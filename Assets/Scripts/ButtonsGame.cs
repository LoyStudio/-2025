using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class ButtonsGame : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ShowRewarded();

    [DllImport("__Internal")]
    private static extern void ShowFullscreen();

    public static bool isPause = true;

    [SerializeField] private GameObject screen;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject mainMusic;
    [SerializeField] private AudioSource sound;
    public static bool countdownFinished = false;
    [SerializeField] private GameObject[] characters;
    [SerializeField] private AudioSource[] sources;

    [SerializeField] private GameObject obj;

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

    public void StartButton()
    {
        sound.Play();
        MuteAllCarsAudio();
        ShowFullscreen();
    }
    private void StartGame()
    {
        UnmuteAllCarsAudio();
        isPause = false;
        countdownFinished = false;
        Loading();
    }

    public void Ads()
    {
        sound.Play();
        MuteAllCarsAudio();
        ShowRewarded();
    }

    private void AddCoins()
    {
        UnmuteAllCarsAudio();
        mainMusic.SetActive(true);
        MainCoins.coins += 500;
        PlayerPrefs.SetInt("coins", MainCoins.coins);
    }

    public void Settings(GameObject panel)
    {
        sound.Play();
        panel.SetActive(true);
    }

    public void CloseSettings(GameObject panel)
    {
        sound.Play();
        panel.SetActive(false);
        PlayerPrefs.SetFloat("volumeM", SoundsControl.volumeMusic);
        PlayerPrefs.SetFloat("volumeS", SoundsControl.volumeSounds);
    }

    public void TgChannel()
    {
        sound.Play();
        Application.OpenURL("https://t.me/LoyStudio");
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
    public void MuteAllCarsAudio()
    {
        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");

        // 2. Перебираем все найденные объекты
        foreach (GameObject car in cars)
        {
            // 3. Получаем все компоненты AudioSource на этом объекте (и его дочерних)
            AudioSource[] audioSources = car.GetComponentsInChildren<AudioSource>();

            // 4. Отключаем звук для каждого AudioSource
            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.mute = true;
                Debug.Log($"Отключен звук на объекте: {car.name} -> AudioSource: {audioSource.name}");
            }
        }

        for (int i = 0; i < sources.Length; i++)
        {
            sources[i].enabled = false;
        }

        Debug.Log($"Звук отключен у всех {cars.Length} объектов с тегом '{"Car"}'.");
    }

    // (Опционально) Метод для включения звука обратно
    public void UnmuteAllCarsAudio()
    {
        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");

        foreach (GameObject car in cars)
        {
            AudioSource[] audioSources = car.GetComponentsInChildren<AudioSource>();
            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.mute = false;
            }
        }

        for (int i = 0; i < sources.Length; i++)
        {
            sources[i].enabled = true;
        }
    }
}
