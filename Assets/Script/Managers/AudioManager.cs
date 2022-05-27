using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer mainMixer = default;
    [Header("Mixer Groups")]
    [SerializeField] AudioMixerGroup musicMixer = default;
    [SerializeField] AudioMixerGroup effectsMixer = default;
    public bool isMuted = false;
    private AudioSource musicMenuSource, musicCreditsSource, musicLobbySource, musicLevelSource;
    private AudioSource buttonMenuSource, pauseInSource, pauseOutSource;
    private AudioSource levelEsitBad, levelEsitGood;
    private AudioSource merchantSell, levelCantEnter;
    private AudioSource astrodamS,fire, bullet_expl, jellyS, doorUn, warpRescueSource, warpJellySource, jellySavedAudioSource;
    private AudioSource gmas1, gmas2, gmasdef, fmas1, fmas2, fmasdef, bossas1, bossasdef;

    [Header("Tracks")]
    [SerializeField] AudioClip musicMenuTrack = default;
    [SerializeField] AudioClip musicCreditsTrack = default;
    [SerializeField] AudioClip musicLobbyTrack = default;
    [SerializeField] AudioClip musicLevelTrack = default;

    [Header("MenuSounds")]
    [SerializeField] AudioClip buttonMenuTrack = default;
    [SerializeField] AudioClip pauseIn = default;
    [SerializeField] AudioClip pauseOut = default;

    [Header("LevelEsit")]
    [SerializeField] AudioClip fail = default;
    [SerializeField] AudioClip clear = default;

    [Header("Lobby")]
    [SerializeField] AudioClip sell = default;
    [SerializeField] AudioClip levelCantEnterAC = default;

    [Header("Level")]
    [SerializeField] AudioClip AstroDamage = default;
    [SerializeField] AudioClip BulletClip = default;
    [SerializeField] AudioClip Bullet_Explosion = default;
    [SerializeField] AudioClip jellySave = default;
    [SerializeField] AudioClip warpJellySave = default;
    [SerializeField] AudioClip warpRescue = default;
    [SerializeField] AudioClip doorUnlock = default;
    [SerializeField] AudioClip jellySavedClip = default;

    [Header("Monsters")]
    [SerializeField] AudioClip GreenMonster1 = default;
    [SerializeField] AudioClip GreenMonster2 = default;
    [SerializeField] AudioClip GreenMonsterDefeat = default;
    [SerializeField] AudioClip FlyingMonster1 = default;
    [SerializeField] AudioClip FlyingMonster2 = default;
    [SerializeField] AudioClip FlyingMonsterDefeat = default;
    [SerializeField] AudioClip BossMonster1 = default;
    [SerializeField] AudioClip BossMonsterDefeat = default;

    private List<AudioSource> loopingSoundTrackList = new List<AudioSource>();

    public static AudioManager instance;
    void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
            InitializeSounds();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void InitializeSounds()
    {
        //soundtrack
        musicMenuSource = gameObject.AddComponent<AudioSource>();
        musicMenuSource.loop = true;
        musicMenuSource.clip = musicMenuTrack;
        musicMenuSource.outputAudioMixerGroup = musicMixer;
        loopingSoundTrackList.Add(musicMenuSource);

        musicCreditsSource = gameObject.AddComponent<AudioSource>();
        musicCreditsSource.loop = true;
        musicCreditsSource.clip = musicCreditsTrack;
        musicCreditsSource.outputAudioMixerGroup = musicMixer;
        loopingSoundTrackList.Add(musicCreditsSource);

        musicLobbySource = gameObject.AddComponent<AudioSource>();
        musicLobbySource.loop = true;
        musicLobbySource.clip = musicLobbyTrack;
        musicLobbySource.outputAudioMixerGroup = musicMixer;
        loopingSoundTrackList.Add(musicLobbySource);

        musicLevelSource = gameObject.AddComponent<AudioSource>();
        musicLevelSource.loop = true;
        musicLevelSource.clip = musicLevelTrack;
        musicLevelSource.outputAudioMixerGroup = musicMixer;
        loopingSoundTrackList.Add(musicLevelSource);

        // menu/pauseMenu
        buttonMenuSource = gameObject.AddComponent<AudioSource>();
        buttonMenuSource.clip = buttonMenuTrack;
        buttonMenuSource.outputAudioMixerGroup = effectsMixer;

        pauseInSource = gameObject.AddComponent<AudioSource>();
        pauseInSource.clip = pauseIn;
        pauseInSource.outputAudioMixerGroup = effectsMixer;

        pauseOutSource = gameObject.AddComponent<AudioSource>();
        pauseOutSource.clip = pauseOut;
        pauseOutSource.outputAudioMixerGroup = effectsMixer;

        //level esit
        levelEsitBad = gameObject.AddComponent<AudioSource>();
        levelEsitBad.clip = fail;
        levelEsitBad.outputAudioMixerGroup = effectsMixer;

        levelEsitGood = gameObject.AddComponent<AudioSource>();
        levelEsitGood.clip = clear;
        levelEsitGood.outputAudioMixerGroup = effectsMixer;

        //lobby
        merchantSell = gameObject.AddComponent<AudioSource>();
        merchantSell.clip = sell;
        merchantSell.outputAudioMixerGroup = effectsMixer;

        levelCantEnter = gameObject.AddComponent<AudioSource>();
        levelCantEnter.clip = levelCantEnterAC;
        levelCantEnter.outputAudioMixerGroup = effectsMixer;

        //level
        astrodamS = gameObject.AddComponent<AudioSource>();
        astrodamS.clip = AstroDamage;
        astrodamS.outputAudioMixerGroup = effectsMixer;

        fire = gameObject.AddComponent<AudioSource>();
        fire.clip = BulletClip;
        fire.outputAudioMixerGroup = effectsMixer;

        bullet_expl = gameObject.AddComponent<AudioSource>();
        bullet_expl.clip = Bullet_Explosion;
        bullet_expl.outputAudioMixerGroup = effectsMixer;

        jellyS = gameObject.AddComponent<AudioSource>();
        jellyS.clip = jellySave;
        jellyS.loop = true;
        jellyS.outputAudioMixerGroup = effectsMixer;


        warpRescueSource = gameObject.AddComponent<AudioSource>();
        warpRescueSource.clip = warpRescue;
        warpRescueSource.loop = true;
        warpRescueSource.outputAudioMixerGroup = effectsMixer;

        warpJellySource = gameObject.AddComponent<AudioSource>();
        warpJellySource.clip = warpJellySave;
        warpJellySource.loop = true;
        warpJellySource.outputAudioMixerGroup = effectsMixer;

        doorUn = gameObject.AddComponent<AudioSource>();
        doorUn.clip = doorUnlock;
        doorUn.outputAudioMixerGroup = effectsMixer;

        jellySavedAudioSource = gameObject.AddComponent<AudioSource>();
        jellySavedAudioSource.clip = jellySavedClip;
        jellySavedAudioSource.outputAudioMixerGroup = effectsMixer;
        jellySavedAudioSource.loop = true;

        //monsters
        gmas1 = gameObject.AddComponent<AudioSource>();
        gmas1.clip = GreenMonster1;
        gmas1.outputAudioMixerGroup = effectsMixer;

        gmas2 = gameObject.AddComponent<AudioSource>();
        gmas2.clip = GreenMonster2;
        gmas2.outputAudioMixerGroup = effectsMixer;

        gmasdef = gameObject.AddComponent<AudioSource>();
        gmasdef.clip = GreenMonsterDefeat;
        gmasdef.outputAudioMixerGroup = effectsMixer;

        fmas1 = gameObject.AddComponent<AudioSource>();
        fmas1.clip = FlyingMonster1;
        fmas1.outputAudioMixerGroup = effectsMixer;

        fmas2 = gameObject.AddComponent<AudioSource>();
        fmas2.clip = FlyingMonster2;
        fmas2.outputAudioMixerGroup = effectsMixer;

        fmasdef = gameObject.AddComponent<AudioSource>();
        fmasdef.clip = FlyingMonsterDefeat;
        fmasdef.outputAudioMixerGroup = effectsMixer;

        bossas1 = gameObject.AddComponent<AudioSource>();
        bossas1.clip = BossMonster1;
        bossas1.outputAudioMixerGroup = effectsMixer;

        bossasdef = gameObject.AddComponent<AudioSource>();
        bossasdef.clip = BossMonsterDefeat;
        bossasdef.outputAudioMixerGroup = effectsMixer;

    }

    public void ChangeMusicValue(float value)
    {
        mainMixer.SetFloat("MusicVolume", value);
    }

    public void ChangeEffectsValue(float value)
    {
        mainMixer.SetFloat("EffectsVolume", value);
    }

    public void SaveChanges()
    {
        mainMixer.GetFloat("MusicVolume", out float tmp);
        mainMixer.GetFloat("EffectsVolume", out float tmp2);
        AudioOptions newOptions = new AudioOptions(tmp, tmp2);
        SaveManager.SaveOptions(newOptions);
    }

    public void PlayMainMenuTrack()
    {
        MuteAllLoopingTracks();
        musicMenuSource.Play();
    }

    public void PlayCreditsTrack()
    {
        MuteAllLoopingTracks();
        musicCreditsSource.Play();
    }
    public void PlayLobbyTrack()
    {
        MuteAllLoopingTracks();
        musicLobbySource.Play();
    }
    public void PlayLevelTrack()
    {
        MuteAllLoopingTracks();
        musicLevelSource.Play();
    }

    public void PlayMenuButton()
    {
        buttonMenuSource.Play();
    }

    public void PlayPauseIn()
    {
        pauseInSource.Play();
    }
    public void PlayPauseOut()
    {
        pauseOutSource.Play();
    }
    public void PlayLevelClear()
    {
        levelEsitGood.Play();
    }
    public void PlayLevelFail()
    {
        levelEsitBad.Play();
    }
    public void PlayMerchantSell()
    {
        merchantSell.Play();
    }

    public void PlayLevelCantEnter()
    {
        levelCantEnter.Play();
    }
    public void PlayFire()
    {
        fire.Play();
    }
    public void PlayJellySave()
    {
        jellyS.Play();
    }

    public void StopJellySave()
    {
        jellyS.Stop();
    }

    public void PlayJellySavedSuccess()
    {
        jellySavedAudioSource.Play();
    }

    public void PlayAstroDamage()
    {
        astrodamS.Play();
    }


    public void StopJellySavedSuccess()
    {
        jellySavedAudioSource.Stop();
    }

    public void PlayLaserWarp()
    {
        warpJellySource.Play();
    }

    public void StopLaserWarp()
    {
        warpJellySource.Stop();
    }

    public void PlayRescueWarp()
    {
        warpRescueSource.Play();
    }

    public void StopRescueWarp()
    {
        warpRescueSource.Stop();
    }
    public void PlayDoorUnlock()
    {
        doorUn.Play();
    }

    public void PlayBulletExpl()
    {
        bullet_expl.Play();
    }

    private void PlayGreenMonster1() { gmas1.Play(); }
    private void PlayGreenMonster2() { gmas2.Play(); }

    public void PlayGreenMonster() {
        if (!gmas1.isPlaying && !gmas2.isPlaying)
        {
            if (Random.Range(1, 3) == 1) PlayGreenMonster1();
            else PlayGreenMonster2();
        }
    }
    public void PlayGreenMonsterDefeat() { gmasdef.Play(); }

    private void PlayFlyingMonster1() { fmas1.Play(); }
    private void PlayFlyingMonster2() { fmas2.Play(); }

    public void PlayFlyngMonster()
    {
        if (!fmas1.isPlaying && !fmas2.isPlaying)
        {
            if (Random.Range(1, 3) == 1) PlayFlyingMonster1();
            else PlayFlyingMonster2();
        }
    }

    public void PlayFlyingMonsterDefeat() { fmasdef.Play(); }

    private void PlayBossMonster1() { bossas1.Play(); }

    public void PlayBossMonster()
    {
        PlayBossMonster1();
    }
    public void PlayBossMonsterDefeat() { bossasdef.Play(); }

    private void MuteAllLoopingTracks()
    {
        foreach (AudioSource AS in loopingSoundTrackList)
        {
            AS.Stop();
        }
    }

    public AudioMixer getMixer()
    {
        return mainMixer;
    }
    public void Mute()
    {
        isMuted=true;
        ChangeMusicValue(-80f);
        ChangeEffectsValue(-80f);
    }


    public void UnMute()
    {
        isMuted = false;
        AudioOptions loadedOptions = SaveManager.getAudioOptions();
        ChangeMusicValue(loadedOptions.musicLevel);
        ChangeEffectsValue(loadedOptions.effectsLevel);
    }

}