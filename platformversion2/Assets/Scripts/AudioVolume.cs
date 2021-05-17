using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public void SetMusicLevel(float slidervalue) {
        mixer.SetFloat("MusicVol", Mathf.Log10(slidervalue)*20);
    }

    public void SetTalkLevel(float slidervalue)
    {
        mixer.SetFloat("TalkVol", Mathf.Log10(slidervalue) * 20);
    }
    public void SetMasterLevel(float slidervalue)
    {
        mixer.SetFloat("MasterVol", Mathf.Log10(slidervalue) * 20);
    }

}
