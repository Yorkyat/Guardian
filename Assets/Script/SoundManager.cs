using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer mainMix;
    public const float defaultVol = 1f;

    void Start()
    {
        SetMusicVol(GameManager.manager.playerData.musicVol);
        SetSfxVol(GameManager.manager.playerData.sfxVol);
    }

    public void SetMusicVol(float musicVol)
    {
        mainMix.SetFloat("musicVol", Mathf.Log(musicVol) * 20);
        GameManager.manager.Save("musicVol", musicVol);
    }

    public void SetSfxVol(float sfxVol)
    {
        mainMix.SetFloat("sfxVol", Mathf.Log(sfxVol) * 20);
        GameManager.manager.Save("sfxVol", sfxVol);
    }
}
