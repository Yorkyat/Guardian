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
    public Menu menuScript;

    public Text enemyCounterText;
    public Text levelText;
    public Text timerText;

    public Text resultLevelText;
    public Text resultMinuteText;
    public Text resultSecondText;
    public Text resultHPText;
    public Text resultLevelScoreText;
    public Text resultTotalScoreText;

    public Toggle hpToggle;
    public Toggle attackToggle;
    public Toggle speedToggle;
    public Text hpBuffText;
    public Text attackBuffText;
    public Text speedBuffText;

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

        hpBuffText.text = "+" + GameManager.manager.playerData.currentHPBuff;
        attackBuffText.text = "+" + GameManager.manager.playerData.currentAttackBuff;
        speedBuffText.text = "+" + GameManager.manager.playerData.currentSpeedBuff;
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

        if(totalScore > GameManager.manager.playerData.highestScore)
        {

        }

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

    public void ChooseHPBuff()
    {
        if (hpToggle.isOn)
        {
            hpBuffText.text = "+" + (GameManager.manager.playerData.currentHPBuff + GameManager.manager.hpBuffVal);
            attackBuffText.text = "+" + GameManager.manager.playerData.currentAttackBuff;
            speedBuffText.text = "+" + GameManager.manager.playerData.currentSpeedBuff;
        }
        else
        {
            hpBuffText.text = "+" + GameManager.manager.playerData.currentHPBuff;
            attackBuffText.text = "+" + GameManager.manager.playerData.currentAttackBuff;
            speedBuffText.text = "+" + GameManager.manager.playerData.currentSpeedBuff;
        }
    }

    public void ChooseAttackBuff()
    {
        if (attackToggle.isOn)
        {
            hpBuffText.text = "+" + GameManager.manager.playerData.currentHPBuff;
            attackBuffText.text = "+" + (GameManager.manager.playerData.currentAttackBuff + GameManager.manager.attackBuffVal);
            speedBuffText.text = "+" + GameManager.manager.playerData.currentSpeedBuff;
        }
        else
        {
            hpBuffText.text = "+" + GameManager.manager.playerData.currentHPBuff;
            attackBuffText.text = "+" + GameManager.manager.playerData.currentAttackBuff;
            speedBuffText.text = "+" + GameManager.manager.playerData.currentSpeedBuff;
        }
    }

    public void ChooseSpeedBuff()
    {
        if (speedToggle.isOn)
        {
            hpBuffText.text = "+" + GameManager.manager.playerData.currentHPBuff;
            attackBuffText.text = "+" + GameManager.manager.playerData.currentAttackBuff;
            speedBuffText.text = "+" + (GameManager.manager.playerData.currentSpeedBuff + GameManager.manager.speedBuffVal);
        }
        else
        {
            hpBuffText.text = "+" + GameManager.manager.playerData.currentHPBuff;
            attackBuffText.text = "+" + GameManager.manager.playerData.currentAttackBuff;
            speedBuffText.text = "+" + GameManager.manager.playerData.currentSpeedBuff;
        }
    }

    private bool CheckBuffSelected()
    {
        if (hpToggle.isOn || attackToggle.isOn || speedToggle.isOn)
        {
            if (hpToggle.isOn)
            {
                GameManager.manager.playerData.currentHPBuff += GameManager.manager.hpBuffVal;
            }
            else if (attackToggle.isOn)
            {
                GameManager.manager.playerData.currentAttackBuff += GameManager.manager.attackBuffVal;
            }
            else if (speedToggle.isOn)
            {
                GameManager.manager.playerData.currentSpeedBuff += GameManager.manager.speedBuffVal;
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    public void NextLevel()
    {
        if (CheckBuffSelected())
        {
            if(totalScore > GameManager.manager.playerData.highestScore)
            {
                GameManager.manager.playerData.highestScore = totalScore;
            }
            if(level > GameManager.manager.playerData.highestLevel)
            {
                GameManager.manager.playerData.highestLevel = level;
            }
            GameManager.manager.playerData.currentScore = totalScore;

            menuScript.LoadNextLevel();
        }
        else
        {
            // Show error "Please choose a buff first"
        }
    }
}
