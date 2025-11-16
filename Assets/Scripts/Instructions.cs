using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Instructions : MonoBehaviour
{
    public static int studyIsComplete = 0;
    [SerializeField] private GameObject[] instructions;
    private AudioSource playerAudio;
    public AudioClip clip;

    private void Start()
    {
        if (PlayerPrefs.HasKey("complete"))
        {
            studyIsComplete = PlayerPrefs.GetInt("complete");
        }
        else
        {
            studyIsComplete = 0;
        }
        if (studyIsComplete == 0)
        {
            instructions[0].SetActive(true);
        }
        else
        {
            instructions[0].SetActive(false);
            instructions[1].SetActive(false);
        }
        playerAudio = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }

    public void Instr()
    {
        if (Timer.countdownFinished == true && GameUI.countdownFinished == true)
        {
            playerAudio.PlayOneShot(clip);
            ButtonsGame.isPause = true;
            Time.timeScale = 0f;
            instructions[0].SetActive(true);
        }
    }

    public void Arrow()
    {
        playerAudio.PlayOneShot(clip);
        instructions[0].SetActive(false);
        instructions[1].SetActive(true);
    }
}
