using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStageManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Text enemyCounterText;
    public Text levelText;
    public Text timerText;

    private int curNoOfEnemy;
    private float stageStartTime;
    private bool gameOver = false;

    private void Start()
    {
        curNoOfEnemy = gameObject.GetComponent<EnemyManager>().noOfEnemy;
        enemyCounterText.text = "Enemies: " + curNoOfEnemy;

        levelText.text = "Level " + GameManager.manager.playerData.currentLevel;

        stageStartTime = Time.time;
    }

    private void Update()
    {
        if (!gameOver)
        {
            int minute = (int)((Time.time - stageStartTime) / 60f);
            int second = (int)((Time.time - stageStartTime) % 60f);
            timerText.text = minute.ToString("00") + ":" + second.ToString("00");
        }
    }

    public void DecreaseNoOfEnemy()
    {
        curNoOfEnemy--;
        enemyCounterText.text = "Enemies: " + curNoOfEnemy;
        if (curNoOfEnemy == 0)
        {
            Win();
        }
    }

    private void Win()
    {

    }

    public void GameOver()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);
    }
}
