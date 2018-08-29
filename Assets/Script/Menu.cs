using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Image loadingBar;
    public GameObject loadingImage;

    private AsyncOperation async;

    public void LoadNextLevel()
    {
        loadingImage.SetActive(true);
        StartCoroutine(LoadLevelWithBar(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevelWithBar(int level)
    {
        async = SceneManager.LoadSceneAsync(level);
        while (!async.isDone)
        {
            loadingBar.fillAmount = async.progress;
            yield return null;
        }
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
