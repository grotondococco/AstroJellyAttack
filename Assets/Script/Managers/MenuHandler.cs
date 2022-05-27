using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MenuManager
{
    [Header("UI Audio Sliders")]
    [SerializeField] Slider musicSlider = default;
    [SerializeField] Slider effectSlider = default;
    [Header("UI Audio Images")]
    [SerializeField] Image soundOnImage = default;
    [SerializeField] Image soundOffImage = default;
    [Header("UI DropdownResolutions")]
    [SerializeField] TMPro.TMP_Dropdown resolutionsDropdown= default;

    AudioManager m_AudioManager = default;
    SaveManager m_SaveManager = default;
    Resolution[] resolutions;

    private void Start()
    {
        m_SaveManager= GameObject.Find("SaveManager").GetComponent<SaveManager>();
        m_SaveManager.Load();
        m_AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        m_AudioManager.PlayMainMenuTrack();
        if (getAudioManager().GetComponent<AudioManager>().isMuted) MuteAll();
        else UnMuteAll();
        resolutions = Screen.resolutions;
        GetResolutions();
        Gamemanager.ShowCursor();
    }

    public void NewGame() {
        getGameManager().GetComponent<Gamemanager>().NewGame();
    }
    public void LoadGame() {
        getGameManager().GetComponent<Gamemanager>().LoadGame();
    }

    public void ShowCredits() {
        Gamemanager.Credits();
    }

    public void Quit() {
        getGameManager().GetComponent<Gamemanager>().Exit();
    }

    public void PlayMenuButton() {
        getAudioManager().GetComponent<AudioManager>().PlayMenuButton();
    }

    public void ChangeMusicValue(float value) {
        getAudioManager().GetComponent<AudioManager>().ChangeMusicValue(value);
    }
    public void ChangeEffectsValue(float value) {
        getAudioManager().GetComponent<AudioManager>().ChangeEffectsValue(value);
    }

    public void SaveAudioChanges() {
        getAudioManager().GetComponent<AudioManager>().SaveChanges();
        m_SaveManager.Load();
    }


    public GameObject getGameManager()
    {
        return GameObject.Find("GameManager");
    }

    public GameObject getAudioManager()
    {
        return GameObject.Find("AudioManager");
    }

    public GameObject getSaveManager()
    {
        return GameObject.Find("SaveManager");
    }


    public void MuteAll()
    {
        m_AudioManager.Mute();
        soundOnImage.gameObject.SetActive(false);
        soundOffImage.gameObject.SetActive(true);
        musicSlider.interactable = false;
        effectSlider.interactable = false;
    }

    public void UnMuteAll()
    {
        m_AudioManager.UnMute();
        musicSlider.interactable = true;
        effectSlider.interactable = true;
        soundOnImage.gameObject.SetActive(true);
        soundOffImage.gameObject.SetActive(false);
        UpdateSliders();
    }

    private void UpdateSliders()
    {
        m_AudioManager.getMixer().GetFloat("MusicVolume", out float tmp);
        musicSlider.value = tmp;
        m_AudioManager.getMixer().GetFloat("EffectsVolume", out float tmp2);
        effectSlider.value = tmp2;
    }

    private void GetResolutions()
    {
        Screen.fullScreen = true;
        int currentResolutionIndex = 0;
        resolutionsDropdown.ClearOptions();
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string resolutionOption = resolutions[i].width + " x " + resolutions[i].height + " "+ resolutions[i].refreshRate + " Hz";
            options.Add(resolutionOption);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = currentResolutionIndex;
        resolutionsDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen, resolution.refreshRate);
        SaveManager.SaveOptions(new ResolutionOptions(resolution.width, resolution.height,resolution.refreshRate));
    }
}
