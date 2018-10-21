using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStageManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Text enemyCounterText;
    public Text levelText;

    private int curNoOfEnemy;

    private void Start()
    {
        curNoOfEnemy = gameObject.GetComponent<EnemyManager>().noOfEnemy;
        enemyCounterText.text = "Enemies: " + curNoOfEnemy;

        levelText.text = "Level " + GameManager.manager.playerData.currentLevel;
    }

    public void DecreaseNoOfEnemy()
    {
        curNoOfEnemy--;
        enemyCounterText.text = "Enemies: " + curNoOfEnemy;
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
