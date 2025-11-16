using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Ads : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ShowRewarded();


}
