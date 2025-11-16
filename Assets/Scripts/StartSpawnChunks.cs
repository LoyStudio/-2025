using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSpawnChunks : MonoBehaviour
{
    public GameObject[] chunkPrefab;
    int countChunksStart = 0;

    private void Start()
    {
        Transform oldStartChunk = gameObject.transform;
        GameObject newStartChunk;
        while (countChunksStart < 3)
        {
            int rand = Random.Range(0, chunkPrefab.Length);
            newStartChunk = Instantiate(chunkPrefab[rand], oldStartChunk.position + new Vector3(34.949f, 0f, 0f), Quaternion.identity);
            oldStartChunk = newStartChunk.transform;
            countChunksStart++;
        }
    }
}
