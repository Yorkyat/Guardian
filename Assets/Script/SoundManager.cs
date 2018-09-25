using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer mainMix;

    public void SetMusicVol(float musicVol)
    {
        mainMix.SetFloat("musicVol", musicVol);
    }

    public void SetSfxVol(float sfxVol)
    {
        mainMix.SetFloat("sfxVol", sfxVol);
    }
}
