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

    public void SetTrack (int trackNum)
    {
        BGM.Stop();
        BGM.clip = songs[trackNum];
        BGM.Play();
    }
}
