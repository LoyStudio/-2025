using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyChunks : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Road"))
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Car"))
        {
            Destroy(other.gameObject);
        }
    }
}
