using UnityEngine;

public class SpawnChanks : MonoBehaviour
{
    public GameObject[] chunkPrefab;
    private int countSpawnChunks = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TestController.countForSpeed++;
            GameObject newChunk;
            GameObject oldChunk = gameObject;
            while (countSpawnChunks < 3)
            {
                if (countSpawnChunks == 0)
                {
                    int rand = Random.Range(0, chunkPrefab.Length);
                    newChunk = Instantiate(chunkPrefab[rand], oldChunk.transform.position + new Vector3(34.949f * 3, 0f, 0f), Quaternion.identity);
                    oldChunk = newChunk;
                    countSpawnChunks++;
                }
                else
                {
                    int rand = Random.Range(0, chunkPrefab.Length);
                    newChunk = Instantiate(chunkPrefab[rand], oldChunk.transform.position + new Vector3(34.949f, 0f, 0f), Quaternion.identity);
                    Destroy(newChunk.GetComponent<SpawnChanks>());
                    oldChunk = newChunk;
                    countSpawnChunks++;
                }
            }
        }
    }
}
