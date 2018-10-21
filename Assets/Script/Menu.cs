using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Image loadingBar;
    public GameObject loadingImage;
    public AudioSource backgroundMusic;

    private AsyncOperation async;
    public Slider musicVolSlider;
    public Slider sfxVolSlider;

    void Start()
    {
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
        loadingImage.SetActive(true);
        StartCoroutine(LoadLevelWithBar(SceneManager.GetActiveScene().buildIndex + 1));

        GameManager.manager.playerData.currentLevel = 1;
        GameManager.manager.Save("currentLevel", GameManager.manager.playerData.currentLevel);

        if(GameManager.manager.playerData.currentLevel > GameManager.manager.playerData.highestLevel)
        {
            GameManager.manager.playerData.highestLevel = GameManager.manager.playerData.currentLevel;
            GameManager.manager.Save("highestLevel", GameManager.manager.playerData.highestLevel);
        }
    }

    public void LoadNextLevel()
    {
        loadingImage.SetActive(true);
        StartCoroutine(LoadLevelWithBar(SceneManager.GetActiveScene().buildIndex));

        GameManager.manager.playerData.currentLevel++;
        GameManager.manager.Save("currentLevel", GameManager.manager.playerData.currentLevel);

        if (GameManager.manager.playerData.currentLevel > GameManager.manager.playerData.highestLevel)
        {
            GameManager.manager.playerData.highestLevel = GameManager.manager.playerData.currentLevel;
            GameManager.manager.Save("highestLevel", GameManager.manager.playerData.highestLevel);
        }
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
