using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStageManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject winPanel;
    public MainCharacterHealth mainCharacterHealth;
    public NumberAnimation numberAnimation;

    public Text enemyCounterText;
    public Text levelText;
    public Text timerText;

    public Text resultLevelText;
    public Text resultMinuteText;
    public Text resultSecondText;
    public Text resultHPText;
    public Text resultLevelScoreText;
    public Text resultTotalScoreText;

    private int curNoOfEnemy;
    private float stageStartTime;
    private float stageStopTime;
    private bool win = false;
    private bool gameOver = false;

    private int level;
    private float stageLastedTime;
    private float hpLeftPercent;
    private int levelScore;
    private int totalScore;

    private void Start()
    {
        curNoOfEnemy = gameObject.GetComponent<EnemyManager>().noOfEnemy;
        enemyCounterText.text = "Enemies: " + curNoOfEnemy;

        levelText.text = "Level " + GameManager.manager.playerData.currentLevel;

        stageStartTime = Time.time;
    }

    private void Update()
    {
        if (!gameOver && !win)
        {
            stageStopTime = Time.time;
            int minute = (int)((stageStopTime - stageStartTime) / 60);
            int second = (int)((stageStopTime - stageStartTime) % 60);
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
        win = true;

        level = GameManager.manager.playerData.currentLevel;
        stageLastedTime = stageStopTime - stageStartTime;
        hpLeftPercent = (float)mainCharacterHealth.currentHealth / (float)mainCharacterHealth.startingHealth * 100;
        levelScore = 1000 + 100 * level + (int)(100 * (1 + hpLeftPercent / 100)) - (int)(2 * stageLastedTime);
        totalScore = GameManager.manager.playerData.currentScore + levelScore;

        winPanel.SetActive(true);
        StartCoroutine(ResultCalculate());
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);
    }

    IEnumerator ResultCalculate()
    {
        // Set Level
        resultLevelText.text = level.ToString();

        // Set Time
        numberAnimation.SetTextObject(resultMinuteText);
        yield return StartCoroutine(numberAnimation.SetNum((int)stageLastedTime / 60, "00"));

        numberAnimation.SetTextObject(resultSecondText);
        yield return StartCoroutine(numberAnimation.SetNum((int)stageLastedTime % 60, "00"));

        // Set HP Left
        numberAnimation.SetTextObject(resultHPText);
        yield return StartCoroutine(numberAnimation.SetNum(hpLeftPercent, "0"));

        // Set Level Score
        numberAnimation.SetTextObject(resultLevelScoreText);
        yield return StartCoroutine(numberAnimation.SetNum(levelScore, "0"));

        // Set Total Score
        numberAnimation.SetTextObject(resultTotalScoreText);
        yield return StartCoroutine(numberAnimation.SetNum(totalScore, "0"));
    }
}
