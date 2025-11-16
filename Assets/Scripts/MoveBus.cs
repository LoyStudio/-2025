using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveBus : MonoBehaviour
{
    private float speed = 7f;
    private void Start()
    {
        GetComponent<AudioSource>().pitch = Random.Range(1f, 1.3f);
        GetComponent<AudioSource>().volume = SoundsControl.volumeSounds;
    }

    private void Update()
    {
        GetComponent<AudioSource>().volume = SoundsControl.volumeSounds;
        transform.Translate(new Vector3(speed * Time.deltaTime, 0f, 0f));
    }
}
