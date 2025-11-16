using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCanvas : MonoBehaviour
{
    void Start()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        GetComponent<Canvas>().worldCamera = camera.GetComponent<Camera>();
    }
}
