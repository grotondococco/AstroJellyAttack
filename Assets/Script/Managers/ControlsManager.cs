using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsManager : MonoBehaviour
{
    [SerializeField] Toggle ToggleIJKL = default;
    [SerializeField] Toggle ToggleKeyPad = default;

    private void Start()
    {
        LoadControls();
    }

    private void LoadControls()
    {
        ControlsOptions co=SaveManager.GetControlsOptions();
        ToggleIJKL.isOn = co.IJKL;
        ToggleKeyPad.isOn = co.KeyPad;
        if (ToggleIJKL.isOn) ToggleIJKL.interactable = false;
        if (ToggleKeyPad.isOn) ToggleKeyPad.interactable = false;
    }
    public void ChangeValue()
    {
        if (ToggleIJKL.interactable)
        {
            ToggleIJKL.interactable = false;
            ToggleKeyPad.isOn = false;
            ToggleKeyPad.interactable = true;
        }
        else if (ToggleKeyPad.interactable)
        {
            ToggleKeyPad.interactable = false;
            ToggleIJKL.isOn = false;
            ToggleIJKL.interactable = true;
        }
        ControlsOptions co = new ControlsOptions(ToggleIJKL.isOn, ToggleKeyPad.isOn);
        SaveManager.SaveControlOptions(co);
    }
}
