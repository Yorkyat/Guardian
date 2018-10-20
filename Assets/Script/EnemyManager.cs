using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public MainCharacterHealth mainCharacterHealth;
    public GameObject[] enemyArray;
    public float spawnTime = 3.0f;
    public Transform[] spawnPoints;
    public int noOfEnemy = 0;

    private List<GameObject> curEnemyList;
    private List<int> enemyNumList;

    void Awake()
    {
        curEnemyList = new List<GameObject>();
        enemyNumList = new List<int>();
        LevelSetUp();
    }

    void Start()
    {
        InvokeRepeating("Spawn", 1.0f, spawnTime);
    }

    void Spawn()
    {
        if (mainCharacterHealth.currentHealth <= 0 || curEnemyList.Count == 0)
        {
            return;
        }

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        int enemyIndex = Random.Range(0, curEnemyList.Count);
        enemyNumList[enemyIndex]--;

        Instantiate(curEnemyList[enemyIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

        if (enemyNumList[enemyIndex] == 0)
        {
            enemyNumList.RemoveAt(enemyIndex);
            curEnemyList.RemoveAt(enemyIndex);
        }
    }

    void LevelSetUp()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                // Level 1
                noOfEnemy = 10;
                curEnemyList.Add(enemyArray[0]);
                curEnemyList[0].GetComponent<EnemyAttack>().attackDamge = 5;
                curEnemyList[0].GetComponent<EnemyHealth>().startingHealth = 30;
                enemyNumList.Add(10);
                break;
        }



    }
}
