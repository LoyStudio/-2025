using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    private bool[] isCompleteStudies = new bool[3];
    private void Start()
    {
        for (int i = 0; i < isCompleteStudies.Length; i++)
        {
            isCompleteStudies[i] = false;
        }
    }
    private void Update()
    {
        if (TestController.countForSpeed == 10 && isCompleteStudies[0] == false)
        {
            isCompleteStudies[0] = true;
            TestController.speed += 0.5f;
            TestController.NowSpeed = TestController.speed;

        }
        if (TestController.countForSpeed == 20 && isCompleteStudies[1] == false)
        {
            isCompleteStudies[1] = true;
            TestController.speed += 0.5f;
            TestController.NowSpeed = TestController.speed;
        }
    }
}
