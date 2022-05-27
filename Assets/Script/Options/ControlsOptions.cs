using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ControlsOptions
{
    public bool IJKL;
    public bool KeyPad;
    public ControlsOptions(bool IJKL=true, bool KeyPad = false)
    {
        this.IJKL = IJKL;
        this.KeyPad = KeyPad;
    }
}
