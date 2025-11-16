using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChunks : MonoBehaviour
{
    public static float speed = 10f;

    void FixedUpdate()
    {
        transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
    }
}
