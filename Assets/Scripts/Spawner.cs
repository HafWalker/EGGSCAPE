using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<Transform> spawnPoints;

    public void SpawnPlayer(PlayerController p) 
    {
        int randomPosition = Random.Range(0, spawnPoints.Count); 
        p.transform.position = spawnPoints[randomPosition].position;
    }
}
