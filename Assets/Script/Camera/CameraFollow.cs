using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MainCamera  
{
    bool peak = false;
    bool alternativeControls = false;
    private void Start()
    {
        peak = SaveManager.GetPowerupSettings().cameraPeak;
        alternativeControls = SaveManager.GetControlsOptions().KeyPad;
        //Debug.Log("Camera peak: " + peak);
    }

    protected override void FixedUpdate()
    {
        if (peak)
        {
            if (!alternativeControls)
                CameraPeak();
            else
                CameraAlternativePeak();
        }
        base.FixedUpdate();
    }

    private void CameraPeak()
    {
        bool trigger = false;
        if (Input.GetKey(KeyCode.I))
        {
            if (offset.y < 2f)
                offset.y += 0.06f;
            trigger = true;
        }
        if (Input.GetKey(KeyCode.J))
        {
            if (offset.x > -2f)
                offset.x -= 0.06f;
            trigger = true;
        }
        if (Input.GetKey(KeyCode.K))
        {
            if (offset.y > -2f)
                offset.y -= 0.06f;
            trigger = true;
        }
        if (Input.GetKey(KeyCode.L))
        {
            if (offset.x < 2f)
                offset.x += 0.06f;
            trigger = true;
        }
        if (!trigger) offset.x = offset.y = 0;
    }

    private void CameraAlternativePeak() {
        bool trigger = false;
        if (Input.GetKey(KeyCode.Keypad8))
        {
            if (offset.y < 2f)
                offset.y += 0.06f;
            trigger = true;
        }
        if (Input.GetKey(KeyCode.Keypad4))
        {
            if (offset.x > -2f)
                offset.x -= 0.06f;
            trigger = true;
        }
        if (Input.GetKey(KeyCode.Keypad5))
        {
            if (offset.y > -2f)
                offset.y -= 0.06f;
            trigger = true;
        }
        if (Input.GetKey(KeyCode.Keypad6))
        {
            if (offset.x < 2f)
                offset.x += 0.06f;
            trigger = true;
        }
        if (!trigger) offset.x = offset.y = 0;
    }
    public void changeFocus(Transform target)
    {
        m_Focus_Transform = target;
    }
}
