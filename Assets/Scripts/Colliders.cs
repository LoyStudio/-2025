using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colliders : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public float laneChangeSpeed = 15f;
    public LayerMask obstacleLayer;
    [SerializeField] private AudioSource playerAudio;
    [SerializeField] private AudioClip[] audioClips;
    private Animator animator;

    [SerializeField] private GameObject gameOverPanel;
    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & obstacleLayer) != 0)
        {
            playerAudio.PlayOneShot(audioClips[3]);
            Scores.isOnUpScores = false;
            HandleObstacleCollision();
        }
    }

    void HandleLaneMovement()
    {
        Vector3 currentPos = player.transform.position;
        currentPos.z = Mathf.Lerp(currentPos.z, TestController.oldTargetZ, laneChangeSpeed * Time.deltaTime);
        transform.position = currentPos;
    }

    void HandleObstacleCollision()
    {
        HandleLaneMovement();
        ButtonsGame.countdownFinished = false;
        Timer.countdownFinished = false;
        TestController.speed = 0f;

        if (animator != null)
        {
            animator.SetBool("IsDead", true);
        }
        OpenGameOverPanel();
    }

    private void OpenGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
}
