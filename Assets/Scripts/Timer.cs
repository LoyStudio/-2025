using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ShowFullscreenClosePanel();

    public GameObject countdownPanel;
    public GameObject[] instructionPanel;
    public TMP_Text countdownText;
    public TMP_Text GO;
    public float countdownTime = 3f;

    [SerializeField] private AudioSource sound;

    public static bool countdownFinished = true;
    private void Start()
    {
        if (PlayerPrefs.HasKey("complete"))
        {
            Instructions.studyIsComplete = PlayerPrefs.GetInt("complete");
        }
        else
        {
            Instructions.studyIsComplete = 0;
        }
        if (Instructions.studyIsComplete == 1)
        {
            countdownFinished = false;
            Time.timeScale = 0f;
            StartCoroutine(CountdownCoroutine());
        }
        else
        {
            countdownFinished = false;
            Time.timeScale = 0f;
        }
    }

    public void CloseButton()
    {
        sound.Play();
        ShowFullscreenClosePanel();
    }

    public void CloseButtonInstr()
    {
        sound.Play();
        Instructions.studyIsComplete = 1;
        PlayerPrefs.SetInt("complete", Instructions.studyIsComplete);
        StartCoroutine(CountdownCoroutine1());
    }

    private void Close()
    {
        RestartCountdown();
        PlayerPrefs.SetFloat("volumeM", SoundsControl.volumeMusic);
        PlayerPrefs.SetFloat("volumeS", SoundsControl.volumeSounds);
    }
    IEnumerator CountdownCoroutine()
    {
        countdownFinished = false;
        countdownPanel.SetActive(false);
        countdownText.gameObject.SetActive(true);
        float timer = countdownTime;

        while (timer > 0)
        {
            countdownText.text = Mathf.Ceil(timer).ToString();

            yield return new WaitForSecondsRealtime(0.1f);

            timer -= 0.1f;
        }

        // Показываем "Старт!"
        countdownText.gameObject.SetActive(false);
        GO.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);

        GO.gameObject.SetActive(false);
        Time.timeScale = 1f;
        ButtonsGame.isPause = false;
        countdownFinished = true;
    }

    IEnumerator CountdownCoroutine1()
    {
        countdownFinished = false;
        instructionPanel[0].SetActive(false);
        instructionPanel[1].SetActive(false);
        countdownText.gameObject.SetActive(true);
        float timer = countdownTime;

        while (timer > 0)
        {
            countdownText.text = Mathf.Ceil(timer).ToString();

            yield return new WaitForSecondsRealtime(0.1f);

            timer -= 0.1f;
        }

        // Показываем "Старт!"
        countdownText.gameObject.SetActive(false);
        GO.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);

        GO.gameObject.SetActive(false);
        Time.timeScale = 1f;
        ButtonsGame.isPause = false;
        countdownFinished = true;
    }

    public void RestartCountdown()
    {
        if (!countdownFinished)
            return;

        Time.timeScale = 0f;
        StartCoroutine(CountdownCoroutine());
    }

    public bool IsCountdownFinished()
    {
        return countdownFinished;
    }
}
