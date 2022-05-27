using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ResolutionOptions
{
    public int width;
    public int height;
    public int refresh;
    public ResolutionOptions(int width, int height, int refresh)
    {
        this.width = width;
        this.height = height;
        this.refresh = refresh;
    }
}
