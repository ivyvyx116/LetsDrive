using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer mainMixer;
    public AudioSource BGM;
    public AudioClip[] songs;

    private void Start()
    {
        BGM = FindObjectOfType<AudioSource>();
    }

    public void SetVolume (float volume)
    {
        mainMixer.SetFloat("mainVolume", volume);
    }

    public void PlayA ()
    {
        BGM.clip = songs[0];
    }
    public void PlayB()
    {
        BGM.clip = songs[1];
    }
    public void PlayC()
    {
        BGM.clip = songs[2];
    }
}
