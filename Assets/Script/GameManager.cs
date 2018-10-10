using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager manager;
    public PlayerData playerData;

    private const int defaultScore = 0;
    private const int defaultLevel = 0;

    void Awake()
    {
        if (manager == null)
        {
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else if (manager != this)
        {
            Destroy(gameObject);
        }

        LoadPlayerData();
    }

    void LoadPlayerData()
    {
        PlayerData loadedPlayerData = new PlayerData();
        loadedPlayerData.highestScore = PlayerPrefs.HasKey("highestScore") ? PlayerPrefs.GetInt("highestScore") : defaultScore;
        loadedPlayerData.highestLevel = PlayerPrefs.HasKey("highestLevel") ? PlayerPrefs.GetInt("highestLevel") : defaultLevel;
        loadedPlayerData.currentScore = PlayerPrefs.HasKey("currentScore") ? PlayerPrefs.GetInt("currentScore") : defaultScore;
        loadedPlayerData.currentLevel = PlayerPrefs.HasKey("currentLevel") ? PlayerPrefs.GetInt("currentLevel") : defaultLevel;
        loadedPlayerData.musicVol = PlayerPrefs.HasKey("musicVol") ? PlayerPrefs.GetFloat("musicVol") : SoundManager.defaultVol;
        loadedPlayerData.sfxVol = PlayerPrefs.HasKey("sfxVol") ? PlayerPrefs.GetFloat("sfxVol") : SoundManager.defaultVol;
        playerData = loadedPlayerData;
    }
}
