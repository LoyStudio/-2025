using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;

public class TestController : MonoBehaviour
{
    [Header("Movement Settings")]
    public static float speed = 10f;
    public static float NowSpeed;
    public static int countForSpeed = 0;
    public float laneChangeSpeed = 15f;
    public float jumpForce = 5f;

    [Header("Lane Positions")]
    public float leftLaneZ = -1.56f;
    public float middleLaneZ = 0f;
    public float rightLaneZ = 1.56f;

    [Header("Roll Settings")]
    public float rollDuration = 0.8f;
    public float rolledColliderHeight = 0.5f;

    [Header("Collision")]
    public LayerMask obstacleLayer;
    public LayerMask obstacleLayer1;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private Animator animator;
    private BoxCollider playerCollider;

    private Vector3 movement;
    public static float oldTargetZ;
    public static float targetZ;
    private int currentLane = 1;
    private bool isGrounded = true;
    private bool isRolling = false;
    private float rollTimer = 0f;

    public GameObject gameOverPanel;
    [SerializeField] private AudioSource playerAudio;
    [SerializeField] private AudioClip[] audioClips;
    private bool isChangeLane = false;
    private bool isGameOver = false;

    public TMP_Text textScore;

    private float timer = 0f;

    void Start()
    {
        isGameOver = false;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider>();
        speed = 8f;
        playerAudio.volume = SoundsControl.volumeSounds;

        NowSpeed = speed;

        rb.freezeRotation = true;
        targetZ = middleLaneZ;
        oldTargetZ = targetZ;

        if (animator != null)
        {
            animator.SetBool("IsRunning", true);
        }
    }

    void Update()
    {
        playerAudio.volume = SoundsControl.volumeSounds;
        HandleInput();
        if (isChangeLane == true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                isChangeLane = false;
            }
        }
        HandleLaneMovement();
        HandleRoll();
        animator.SetBool("IsJump", !isGrounded);
    }

    private void FixedUpdate()
    {
        movement = Vector3.right * speed * Time.fixedDeltaTime;
        Vector3 newPos = transform.position + movement;
        newPos.z = Mathf.Lerp(transform.position.z, targetZ, 20f * Time.fixedDeltaTime);
        transform.position = newPos;
    }

    void HandleInput()
    {
        // Lane movement
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && isGameOver != true)
        {
            playerAudio.PlayOneShot(audioClips[2]);
            isChangeLane = true;
            timer = 1f;
            MoveToLeftLane();
        }

        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && isGameOver != true)
        {
            playerAudio.PlayOneShot(audioClips[2]);
            isChangeLane = true;
            timer = 1f;
            MoveToRightLane();
        }

        // Jump
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded && !isRolling && isGameOver != true)
        {
            playerAudio.PlayOneShot(audioClips[0]);
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            animator.SetBool("IsJump", true);
            isGrounded = false;
        }

        // Roll
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && isGrounded && !isRolling && isGameOver != true)
        {
            playerAudio.PlayOneShot(audioClips[1]);
            StartRoll();
        }
    }

    void HandleLaneMovement()
    {
        // Smooth lane transition
        Vector3 currentPos = transform.position;
        currentPos.z = Mathf.Lerp(currentPos.z, targetZ, laneChangeSpeed * Time.deltaTime);
        transform.position = currentPos;
    }

    void HandleRoll()
    {
        if (isRolling)
        {
            rollTimer -= Time.deltaTime;
            if (rollTimer <= 0f)
            {
                EndRoll();
            }
        }
    }

    void MoveToLeftLane()
    {
        if (currentLane < 2)
        {
            currentLane++;
            UpdateTargetPosition();
        }
    }

    void MoveToRightLane()
    {
        if (currentLane > 0)
        {
            currentLane--;
            UpdateTargetPosition();
        }
    }

    void UpdateTargetPosition()
    {
        switch (currentLane)
        {
            case 0:
                oldTargetZ = targetZ;
                targetZ = leftLaneZ;
                break;
            case 1:
                oldTargetZ = targetZ;
                targetZ = middleLaneZ;
                break;
            case 2:
                oldTargetZ = targetZ;
                targetZ = rightLaneZ;
                break;
        }
    }

    void StartRoll()
    {
        isRolling = true;
        rollTimer = rollDuration;

        Vector3 newSize = playerCollider.size;
        newSize.y = 0.3f;
        playerCollider.size = newSize;

        if (animator != null)
        {
            animator.SetBool("IsRolling", true);
        }
    }

    void EndRoll()
    {
        isRolling = false;

        // Restore original collider height
        Vector3 newSize = playerCollider.size;
        newSize.y = 0.5f;
        playerCollider.size = newSize;

        if (animator != null)
        {
            animator.SetBool("IsRolling", false);
        }
    }

    // Ground detection
    void OnCollisionEnter(Collision collision)
    {
        if ((groundLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            isGrounded = true;
        }
        if (((1 << collision.gameObject.layer) & obstacleLayer) != 0)
        {
            playerAudio.PlayOneShot(audioClips[3]);
            Scores.isOnUpScores = false;
            if (isChangeLane == true)
            {
                targetZ = oldTargetZ;
            }
            HandleObstacleCollision();
        }
        if (((1 << collision.gameObject.layer) & obstacleLayer1) != 0)
        {
            playerAudio.PlayOneShot(audioClips[3]);
            Scores.isOnUpScores = false;
            HandleObstacleCollision();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if ((groundLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            isGrounded = true;
        }
    }

    void HandleObstacleCollision()
    {
        isGameOver = true;
        MainCoins.coins += Scores.coins;
        PlayerPrefs.SetInt("coins", MainCoins.coins);
        PlayerPrefs.SetFloat("volumeM", SoundsControl.volumeMusic);
        PlayerPrefs.SetFloat("volumeS", SoundsControl.volumeSounds);
        textScore.text = Scores.scores.ToString();
        ButtonsGame.countdownFinished = false;
        Timer.countdownFinished = false;
        speed = 0f;
        rb.velocity = Vector3.zero;

        // Play death animation
        if (animator != null)
        {
            animator.SetBool("IsDead", true);
        }
        Invoke("OpenGameOverPanel", 1f);
    }
    private void OpenGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
}
