using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    private AudioSource playerAudio;
    [SerializeField] private AudioClip clip;

    private void Start()
    {
        playerAudio = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Scores.coins++;
            playerAudio.PlayOneShot(clip);
            Destroy(gameObject);
        }
    }
}
