using UnityEngine;
using TMPro;

public class Scores : MonoBehaviour
{
    public static int scores;
    public static int coins = 0;
    [SerializeField] private TMP_Text textScore;
    [SerializeField] private TMP_Text textCoins;
    public static bool isOnUpScores;

    private void Start()
    {
        coins = 0;
        scores = 0;
        isOnUpScores = true;
    }

    private void Update()
    {
        textCoins.text = coins.ToString();
        if ((ButtonsGame.isPause == false && isOnUpScores == true) && Timer.countdownFinished == true)
        {
            scores += 1;
            if (scores < 10)
            {
                textScore.text = "00000" + scores.ToString("F0");
            }
            else if (scores < 100 && scores > 10)
            {
                textScore.text = "0000" + scores.ToString("F0");
            }
            else if (scores < 1000 && scores > 100)
            {
                textScore.text = "000" + scores.ToString("F0");
            }
            else if (scores < 10000 && scores > 1000)
            {
                textScore.text = "00" + scores.ToString("F0");
            }
            else if (scores < 100000 && scores > 10000)
            {
                textScore.text = "0" + scores.ToString("F0");
            }
            else if (scores > 100000)
            {
                textScore.text = scores.ToString("F0");
            }
        }
    }
}
