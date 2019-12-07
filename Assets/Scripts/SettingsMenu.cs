﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer mainMixer;

    public void SetVolume (float volume)
    {
        mainMixer.SetFloat("mainVolume", volume);
    }

    public void SetTrack (float trackNum)
    {
        // set the corresponding track;
    }

}
