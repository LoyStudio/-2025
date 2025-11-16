using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject[] characters;
    [SerializeField] private GameObject[] buyButtons;
    [SerializeField] private TMP_Text[] textsButton;
    public static int currnetCharacter = 0;
    private int buyedButton1 = 0;
    private int buyedButton2 = 0;
    public static bool[] isSelect = new bool[3];
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioClip BuyClip;

    private void Start()
    {
        for (int i = 0; i < isSelect.Length; i++)
        {
            isSelect[i] = false;
        }
        isSelect[0] = true;
        if (PlayerPrefs.HasKey("buy1"))
        {
            buyedButton1 = PlayerPrefs.GetInt("buy1");
        }
        else
        {
            buyedButton1 = 0;
        }
        if (PlayerPrefs.HasKey("buy2"))
        {
            buyedButton2 = PlayerPrefs.GetInt("buy2");
        }
        else
        {
            buyedButton2 = 0;
        }
        if (buyedButton1 == 1)
        {
            Destroy(buyButtons[0].gameObject);
        }
        if (buyedButton2 == 1)
        {
            Destroy(buyButtons[1].gameObject);
        }
        textsButton[currnetCharacter].text = "Выбрано";
    }

    public void Sahur(TMP_Text text)
    {
        source.PlayOneShot(clip);
        isSelect[0] = true;
        for (int i = 0; i < textsButton.Length; i++)
        {
            textsButton[i].text = "Выбрать";
        }
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].SetActive(false);
        }
        text.text = "Выбрано";
        currnetCharacter = 0;
        characters[currnetCharacter].SetActive(true);
        PlayerPrefs.SetInt("current", currnetCharacter);
    }


    public void Patrick(TMP_Text text)
    {
        source.PlayOneShot(clip);
        isSelect[0] = false;
        isSelect[1] = true;
        for (int i = 0; i < textsButton.Length; i++)
        {
            textsButton[i].text = "Выбрать";
        }
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].SetActive(false);
        }
        text.text = "Выбрано";
        currnetCharacter = 1;
        characters[currnetCharacter].SetActive(true);
        PlayerPrefs.SetInt("current", currnetCharacter);
    }
    public void HuggyWuggy(TMP_Text text)
    {
        source.PlayOneShot(clip);
        isSelect[0] = false;
        isSelect[2] = true;
        for (int i = 0; i < textsButton.Length; i++)
        {
            textsButton[i].text = "Выбрать";
        }
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].SetActive(false);
        }
        text.text = "Выбрано";
        currnetCharacter = 2;
        characters[currnetCharacter].SetActive(true);
        PlayerPrefs.SetInt("current", currnetCharacter);
    }

    public void BuyPatrick()
    {
        if (MainCoins.coins >= 3000)
        {
            source.PlayOneShot(BuyClip);
            buyedButton1 = 1;
            MainCoins.coins -= 3000;
            PlayerPrefs.SetInt("buy1", buyedButton1);
            PlayerPrefs.SetInt("coins", MainCoins.coins);
            Destroy(buyButtons[0].gameObject);
        }
    }

    public void BuyHuggyWuggy()
    {
        if (MainCoins.coins >= 7000) 
        {
            source.PlayOneShot(BuyClip);
            buyedButton2 = 1;
            MainCoins.coins -= 7000;
            PlayerPrefs.SetInt("buy2", buyedButton2);
            PlayerPrefs.SetInt("coins", MainCoins.coins);
            Destroy(buyButtons[1].gameObject);
        }
    }

}
