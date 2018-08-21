using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public MainCharacterHealth mainCharacterHealth;
    public GameObject[] enemyArray;
    public float spawnTime = 3.0f;
    public Transform[] spawnPoints;

    void Start()
    {
        InvokeRepeating("Spawn", 1.0f, spawnTime);
    }

    void Spawn()
    {
        if(mainCharacterHealth.currentHealth <= 0)
        {
            return;
        }

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        int enemyIndex = Random.Range(0, enemyArray.Length);
        Instantiate(enemyArray[enemyIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }

}
