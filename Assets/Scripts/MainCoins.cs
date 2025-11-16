using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainCoins : MonoBehaviour
{
    public static int coins;
    public static int scores;
    public TMP_Text[] textCoins;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey("coins"))
        {
            coins = PlayerPrefs.GetInt("coins");
        }
        else
        {
            coins = 0;
        }
    }

    private void Update()
    {
        textCoins[0].text = coins.ToString();
        textCoins[1].text = coins.ToString();
    }
}
