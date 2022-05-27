using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PowerupSettings
{
    public int speedLevel;
    public int oxygenLevel;
    public int firingLevel;
    public bool cameraPeak;
    public PowerupSettings(int speedLevel=1,int oxygenLevel=1, int firingLevel=1,bool cameraPeak = false)
    {
        this.speedLevel = speedLevel;
        this.oxygenLevel = oxygenLevel;
        this.firingLevel = firingLevel;
        this.cameraPeak = cameraPeak;
    }
}
