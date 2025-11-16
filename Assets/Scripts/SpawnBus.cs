using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBus : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private GameObject[] vehicles;

    private void Start()
    {
        StartCoroutine(SpawnVehicles());
    }

    IEnumerator SpawnVehicles()
    {
        while (true)
        {
            int randPoint = Random.Range(0, points.Length);
            int randVeh = Random.Range(0, vehicles.Length);
            yield return new WaitForSeconds(3f);
            if (randPoint == 1)
            {
                Instantiate(vehicles[randVeh], points[randPoint].position, Quaternion.Euler(0f,180f,0f));
            }
            else
            {
                Instantiate(vehicles[randVeh], points[randPoint].position, Quaternion.identity);
            }
        }
    }
}
