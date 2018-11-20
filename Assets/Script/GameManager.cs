using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager manager;
    public PlayerData playerData;

    private const int defaultScore = 0;
    private const int defaultLevel = 0;
    private const int defaultBuff = 0;
    public int hpBuffVal = 10;
    public int attackBuffVal = 2;
    public float speedBuffVal = 0.2f;

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

    public void Save(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    public void Save(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }

    public void Save(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }

    public void ResetLevelData()
    {
        playerData.currentScore = defaultScore;
        playerData.currentLevel = defaultLevel;
        playerData.currentHPBuff = defaultBuff;
        playerData.currentAttackBuff = defaultBuff;
        playerData.currentSpeedBuff = (float)defaultBuff;
    }

    public void SavePlayerData()
    {
        Save("highestScore", playerData.highestScore);
        Save("highestLevel", playerData.highestLevel);
        Save("currentScore", playerData.currentScore);
        Save("currentLevel", playerData.currentLevel);
        Save("currentHPBuff", playerData.currentHPBuff);
        Save("currentAttackBuff", playerData.currentAttackBuff);
        Save("currentSpeedBuff", playerData.currentSpeedBuff);
        Save("musicVol", playerData.musicVol);
        Save("sfxVol", playerData.sfxVol);
    }

    void LoadPlayerData()
    {
        PlayerData loadedPlayerData = new PlayerData();
        loadedPlayerData.highestScore = PlayerPrefs.HasKey("highestScore") ? PlayerPrefs.GetInt("highestScore") : defaultScore;
        loadedPlayerData.highestLevel = PlayerPrefs.HasKey("highestLevel") ? PlayerPrefs.GetInt("highestLevel") : defaultLevel;
        loadedPlayerData.currentScore = PlayerPrefs.HasKey("currentScore") ? PlayerPrefs.GetInt("currentScore") : defaultScore;
        loadedPlayerData.currentLevel = PlayerPrefs.HasKey("currentLevel") ? PlayerPrefs.GetInt("currentLevel") : defaultLevel;
        loadedPlayerData.currentHPBuff = PlayerPrefs.HasKey("currentHPBuff") ? PlayerPrefs.GetInt("currentHPBuff") : defaultBuff;
        loadedPlayerData.currentAttackBuff = PlayerPrefs.HasKey("currentAttackBuff") ? PlayerPrefs.GetInt("currentAttackBuff") : defaultBuff;
        loadedPlayerData.currentSpeedBuff = PlayerPrefs.HasKey("currentSpeedBuff") ? PlayerPrefs.GetFloat("currentSpeedBuff") : (float)defaultBuff;
        loadedPlayerData.musicVol = PlayerPrefs.HasKey("musicVol") ? PlayerPrefs.GetFloat("musicVol") : SoundManager.defaultVol;
        loadedPlayerData.sfxVol = PlayerPrefs.HasKey("sfxVol") ? PlayerPrefs.GetFloat("sfxVol") : SoundManager.defaultVol;
        playerData = loadedPlayerData;
    }
}
