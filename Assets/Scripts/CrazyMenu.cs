using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CrazyMenu : MonoBehaviour
{
    [SerializeField] private AudioSource audio;

    public static bool isPause = true;

    [SerializeField] private GameObject screen;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject mainMusic;
    public static bool countdownFinished = false;
    [SerializeField] private GameObject[] characters;

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
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].SetActive(false);
        }
        characters[Shop.currnetCharacter].SetActive(true);
    }
    public void StartGame()
    {
        audio.Play();
        isPause = false;
        countdownFinished = false;
        Loading();
    }

    public void BackMenu()
    {
        audio.Play();
        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
        audio.Play();
        SceneManager.LoadScene("Menu");
    }

    public void Settings(GameObject panel)
    {
        audio.Play();
        panel.SetActive(true);
    }

    public void CloseSettings(GameObject panel)
    {
        audio.Play();
        panel.SetActive(false);
        PlayerPrefs.SetFloat("volumeM", SoundsControl.volumeMusic);
        PlayerPrefs.SetFloat("volumeS", SoundsControl.volumeSounds);
    }

    public void Loading()
    {
        screen.SetActive(true);
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(1);
        loadAsync.allowSceneActivation = false;

        while (!loadAsync.isDone)
        {
            slider.value = loadAsync.progress;

            if (loadAsync.progress >= 0.9f && !loadAsync.allowSceneActivation)
            {
                yield return new WaitForSeconds(2.2f);
                loadAsync.allowSceneActivation = true;
            }
            yield return null;
        }

    }
}
