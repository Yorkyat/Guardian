﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Image loadingBar;
    public GameObject loadingImage;
    public AudioSource backgroundMusic;
    public Slider musicVolSlider;
    public Slider sfxVolSlider;
    public GameObject continueGameButton;
    public Text highestLevelText;
    public Text highestScoreText;
    public Text currentLevelText;
    public Text currentScoreText;
    public GameObject highestStatsGroup;
    public GameObject currentStatsGroup;

    private AsyncOperation async;

    void Start()
    {
        if (highestLevelText != null && highestScoreText != null && GameManager.manager.playerData.highestLevel != 0)
        {
            highestLevelText.text = GameManager.manager.playerData.highestLevel.ToString();
            highestScoreText.text = GameManager.manager.playerData.highestScore.ToString();
            highestStatsGroup.SetActive(true);
        }

        if (currentLevelText != null && currentScoreText != null && GameManager.manager.playerData.currentLevel != 0)
        {
            currentLevelText.text = GameManager.manager.playerData.currentLevel.ToString();
            currentScoreText.text = GameManager.manager.playerData.currentScore.ToString();
            currentStatsGroup.SetActive(true);
        }

        if (continueGameButton != null)
        {
            if (GameManager.manager.playerData.currentLevel != 0)
            {
                continueGameButton.SetActive(true);
            }
        }

        if (musicVolSlider != null)
        {
            musicVolSlider.normalizedValue = GameManager.manager.playerData.musicVol;
        }

        if (sfxVolSlider != null)
        {
            sfxVolSlider.normalizedValue = GameManager.manager.playerData.sfxVol;
        }
    }

    public void NewGame()
    {
        GameManager.manager.ResetLevelData();
        GameManager.manager.playerData.currentLevel = 1;

        loadingImage.SetActive(true);
        StartCoroutine(LoadLevelWithBar(SceneManager.GetActiveScene().buildIndex + 1));

        GameManager.manager.SavePlayerData();
    }

    public void ContinueGame()
    {
        loadingImage.SetActive(true);
        StartCoroutine(LoadLevelWithBar(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadNextLevel()
    {
        GameManager.manager.playerData.currentLevel++;

        loadingImage.SetActive(true);
        StartCoroutine(LoadLevelWithBar(SceneManager.GetActiveScene().buildIndex));

        GameManager.manager.SavePlayerData();
    }

    IEnumerator LoadLevelWithBar(int buildIndex)
    {
        if (backgroundMusic.isPlaying)
        {
            backgroundMusic.Stop();
        }

        async = SceneManager.LoadSceneAsync(buildIndex);
        while (!async.isDone)
        {
            loadingBar.fillAmount = async.progress;
            yield return null;
        }
    }

    public void RestartLevel()
    {
        loadingImage.SetActive(true);
        StartCoroutine(LoadLevelWithBar(SceneManager.GetActiveScene().buildIndex));
        Time.timeScale = 1;
    }

    public void BackToMainMenu()
    {
        loadingImage.SetActive(true);
        StartCoroutine(LoadLevelWithBar(0));
        Time.timeScale = 1;
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
}
