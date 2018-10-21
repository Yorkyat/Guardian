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
        mainMix.SetFloat("musicVol", Mathf.Log(GameManager.manager.playerData.musicVol) * 20);
        mainMix.SetFloat("sfxVol", Mathf.Log(GameManager.manager.playerData.sfxVol) * 20);
    }

    public void SetMusicVol(float musicVol)
    {
        GameManager.manager.playerData.musicVol = musicVol;
        mainMix.SetFloat("musicVol", Mathf.Log(musicVol) * 20);
        GameManager.manager.Save("musicVol", musicVol);
    }

    public void SetSfxVol(float sfxVol)
    {
        GameManager.manager.playerData.sfxVol = sfxVol;
        mainMix.SetFloat("sfxVol", Mathf.Log(sfxVol) * 20);
        GameManager.manager.Save("sfxVol", sfxVol);
    }
}
