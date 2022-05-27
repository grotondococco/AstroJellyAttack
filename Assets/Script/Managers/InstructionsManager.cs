using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsManager : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI IstructionsText = default;
    [SerializeField] Image IstructionsImage=default;
    [SerializeField] GameObject backImage=default;
    [SerializeField] GameObject nextImage=default;
    string[] texts;
    [SerializeField] Sprite[] spriteArray = new Sprite[6];
    [SerializeField] LobbyManager m_LobbyManager = default;
    public int nIstr = 0;
    int totalIstr = 6;
    private void Start()
    {
        nIstr = 0;
        texts = new string[totalIstr];
        texts[0] = "Controls:\n\nMove: WASD\nRUN: Mouse R\n\nlook out: running consume more oxygen!";
        texts[1] = "Controls:\n\nFIRE: Mouse L\n\ndefend youself with gun";
        texts[2] = "Controls:\n\nSAVE JELLY: E\n\nthis is your mission";
        texts[3] = "Controls:\n\nCALL RESCUER: R (only rescuer area)\n\nit's always a B plan";
        texts[4] = "Look out for your Oxygen Level, use Oxygen Areas to take a rest";
        texts[5] = "Please help me save my jellies, but be carefull of monsters!";
        backImage.SetActive(false);
        nextImage.SetActive(false);
    }

    //called
    public void ShowIstructions()
    {
        if (nIstr == 0)
            backImage.SetActive(false);
        else
            backImage.SetActive(true);
        if (nIstr == totalIstr-1) nextImage.SetActive(false);
        else nextImage.SetActive(true);
        IstructionsImage.sprite = spriteArray[nIstr];
        IstructionsText.text = texts[nIstr];
    }

    public void ShowNextIstructions() {
        if (nIstr < totalIstr-1) nIstr++;
        ShowIstructions();
    }
    //called by GUI
    public void ShowPreviousIstructions()
    {
        if (nIstr > 0) nIstr--;
        ShowIstructions();
    }
    //called by gui
    public void ResetIstructions() {
        backImage.SetActive(false);
        nextImage.SetActive(false);
        m_LobbyManager.HideInstructions();
        nIstr = 0;
    }

}
