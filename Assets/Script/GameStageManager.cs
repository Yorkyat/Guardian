using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStageManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Text enemyNoText;

    private int curNoOfEnemy;

    private void Start()
    {
        curNoOfEnemy = gameObject.GetComponent<EnemyManager>().noOfEnemy;
        enemyNoText.text = curNoOfEnemy.ToString();
    }

    public void DecreaseNoOfEnemy()
    {
        curNoOfEnemy--;
        enemyNoText.text = curNoOfEnemy.ToString();
        if(curNoOfEnemy == 0)
        {
            Win();
        }
    }

    private void Win()
    {

    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }
}
