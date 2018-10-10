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
            musicVolSlider.value = GameManager.manager.playerData.musicVol;
        }
        if (sfxVolSlider != null)
        {
            sfxVolSlider.value = GameManager.manager.playerData.sfxVol;
        }
    }

    public void LoadNextLevel()
    {
        loadingImage.SetActive(true);
        StartCoroutine(LoadLevelWithBar(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevelWithBar(int level)
    {
        if (backgroundMusic.isPlaying)
        {
            backgroundMusic.Stop();
        }

        async = SceneManager.LoadSceneAsync(level);
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
