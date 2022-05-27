using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AudioOptions
{
    public float musicLevel;
    public float effectsLevel;
    public AudioOptions(float musicLevel = 0f, float effectsLevel = 0f)
    {
        this.musicLevel = musicLevel;
        this.effectsLevel = effectsLevel;
    }
}
