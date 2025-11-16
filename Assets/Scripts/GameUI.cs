using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using YG;

public class GameUI : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ShowFullscreenPause();

    [DllImport("__Internal")]
    private static extern void ShowFullscreenExit();


    [DllImport("__Internal")]
    private static extern void ShowFullscreenRestart();

    public GameObject countdownPanel;
    public TMP_Text countdownText;
    public TMP_Text GO;
    public static float countdownTime = 3f;
    private int mainScores = 0;

    [SerializeField] private AudioSource sound;
    public static bool countdownFinished = true;
    [SerializeField] private AudioSource music;

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
        if (PlayerPrefs.HasKey("current"))
        {
            Shop.currnetCharacter = PlayerPrefs.GetInt("current");
        }
        else
        {
            Shop.currnetCharacter = 0;
        }
        if (PlayerPrefs.HasKey("best"))
        {
            mainScores = PlayerPrefs.GetInt("best");
        }
        else
        {
            mainScores = 0;
        }
    }

    public void RestartButton()
    {
        sound.Play();
        music.Stop();
        Restart();
    }

    private void Restart()
    {
        if (PlayerPrefs.HasKey("complete"))
        {
            Instructions.studyIsComplete = PlayerPrefs.GetInt("complete");
        }
        else
        {
            Instructions.studyIsComplete = 0;
        }
        MainCoins.coins += Scores.coins;
        PlayerPrefs.SetInt("coins", MainCoins.coins);
        if (Scores.scores > mainScores)
        {
            mainScores = Scores.scores;
        }
        PlayerPrefs.SetInt("best", mainScores);
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync(currentScene.name);
    }

    public void BackMenuButton()
    {
        sound.Play();
        music.Stop();
        ShowFullscreenExit();
    }

    public void BackMenuButton1()
    {
        sound.Play();
        music.Stop();
        BackMenu();
    }

    private void BackMenu()
    {
        if (Scores.scores > mainScores)
        {
            mainScores = Scores.scores;
        }
        PlayerPrefs.SetInt("best", mainScores);
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("Menu");
    }

    public void PauseButton()
    {
        if (Timer.countdownFinished && ButtonsGame.countdownFinished)
        {
            sound.Play();
            ShowFullscreenPause();
        }
    }
    public void Pause()
    {
        if (Timer.countdownFinished == true && countdownFinished == true)
        {
            ButtonsGame.isPause = true;
            Time.timeScale = 0;
            countdownPanel.SetActive(true);
        }
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
