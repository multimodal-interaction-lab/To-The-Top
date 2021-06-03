﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    private static AudioManager _instance;
    public static AudioManager Instance => _instance ? _instance : FindObjectOfType<AudioManager>();

    //Sound source for bg music
    public AudioSource musicSource;
    //Sound source for client side UI sound effects
    public AudioSource SFXSource;


    public bool playMusicOnAwake;

    public MusicAsset[] playlist;

    private bool isMutedMusic;
    private bool isMutedSFX;
    private float musicVol;
    private float sfxVol;
    //How much button adjusts volume (in db)
    private const float VOL_STEP = 5f;
    //Bounds for music and sfx
    private const float MIN_VOL_BOUND = -70f;
    private const float MAX_VOL_BOUND = 20f;


    private List<MusicAsset> currentPlaylist;
    private int currentPlaylistIndex;

    private bool musicStarted;
    private bool playingMusic;
    #region PlayerPrefKeys
    //Checks if default prefs need to be set (default value is 0, indicates need to setup)
    public const string SETUP_AUDIO_INT = "audioSetup";
    public const string MUTE_MUSIC_INT = "muteMusic";
    public const string MUTE_SFX_INT = "muteSFX";
    public const string VOL_MUSIC_INT = "volMusic";
    public const string VOL_SFX_INT = "volSFX";
    #endregion
    public const int DEFAULT_MUSIC_VOL = 0;
    public const int DEFAULT_SFX_VOL = 0;

    private bool MusicDone
    {
        get
        {
            return musicStarted && musicSource.time <= 0f;
        }
    }

    [SerializeField]
    public AudioMixerGroup masterMixer;
    [SerializeField]
    private AudioMixerGroup musicMixer;
    [SerializeField]
    private AudioMixerGroup sfxMixer;

    private AudioSource[] audioSources;

    public void Awake()
    {
        if(PlayerPrefs.GetInt(SETUP_AUDIO_INT) == 0)
        {
            SetDefaultSettings();
        }
        RetrievePrefs();
        playingMusic = false;
        musicStarted = false;
        if (playMusicOnAwake)
        {
            GenerateCurrentPlaylist();
            PlayNextInPlaylist();
        }
    }

    public void Update()
    {
        if (playingMusic)
        {
            musicStarted |= musicSource.time > 0f;
            if (MusicDone)
            {
                PlayNextInPlaylist();
            }
        }
    }
    void SetDefaultSettings()
    {
        PlayerPrefs.SetInt(VOL_SFX_INT, DEFAULT_SFX_VOL);
        PlayerPrefs.SetInt(VOL_MUSIC_INT, DEFAULT_MUSIC_VOL);
        PlayerPrefs.SetInt(SETUP_AUDIO_INT, 1);
    }
    void RetrievePrefs()
    {
        musicVol = PlayerPrefs.GetInt(VOL_MUSIC_INT);
        sfxVol = PlayerPrefs.GetInt(VOL_SFX_INT);
        isMutedMusic = PlayerPrefs.GetInt(MUTE_MUSIC_INT) == 0 ? false : true;
        isMutedSFX = PlayerPrefs.GetInt(MUTE_SFX_INT) == 0 ? false : true;
        UpdateMixers();
    }
    void UpdateMixers()
    {
        musicMixer.audioMixer.SetFloat("MusicVolume", isMutedMusic ? -80f : musicVol);
        sfxMixer.audioMixer.SetFloat("SFXVolume", isMutedSFX ? -80f : sfxVol);
    }
    public void DecrementMusic()
    {
        musicVol = Mathf.Max(musicVol - VOL_STEP, MIN_VOL_BOUND);
        PlayerPrefs.SetInt(VOL_MUSIC_INT, (int)musicVol);
        UpdateMixers();
    }
    public void IncrementMusic()
    {
        musicVol = Mathf.Min(musicVol + VOL_STEP, MAX_VOL_BOUND);
        PlayerPrefs.SetInt(VOL_MUSIC_INT, (int)musicVol);
        UpdateMixers();
    }
    public void DecrementSFX()
    {
        sfxVol = Mathf.Max(sfxVol - VOL_STEP, MIN_VOL_BOUND);
        PlayerPrefs.SetInt(VOL_SFX_INT, (int)sfxVol);
        UpdateMixers();
    }
    public void IncrementSFX()
    {
        sfxVol = Mathf.Min(sfxVol + VOL_STEP, MAX_VOL_BOUND);
        PlayerPrefs.SetInt(VOL_SFX_INT, (int)sfxVol);
        UpdateMixers();
    }
    public void ToggleMuteMusic()
    {
        isMutedMusic = !isMutedMusic;
        PlayerPrefs.SetInt(MUTE_MUSIC_INT, isMutedMusic ? 1 : 0);
        UpdateMixers();
    }
    public void ToggleMuteSFX()
    {
        isMutedSFX = !isMutedSFX;
        PlayerPrefs.SetInt(MUTE_SFX_INT, isMutedSFX ? 1 : 0);
        UpdateMixers();
    }
    //Plays a given music clip
    public void PlayMusic(MusicAsset music)
    {
        musicSource.volume = music.volume;
        musicSource.clip = music.audioClip;
        musicSource.Play();
    }
    //Plays a given sound effect clip
    public void PlaySFX(SFXAsset sfx)
    {
        PlaySFX(sfx, SFXSource);
    }
    //Plays a given sound effect clip at a specific source
    public void PlaySFX(SFXAsset sfx, AudioSource src)
    {

        src.pitch = Random.Range(sfx.minPitch, sfx.maxPitch);
        src.volume = sfx.volume;
        src.clip = sfx.audioClip;
        if (sfx.alts.Length > 0)
        {
            int chosenClipIndex = Random.Range(0, sfx.alts.Length + 1);
            if (chosenClipIndex != sfx.alts.Length)
            {
                src.clip = sfx.alts[chosenClipIndex];
            }
        }
        src.PlayOneShot(src.clip);
    }
    //Shuffles songs to play
    public void GenerateCurrentPlaylist()
    {
        currentPlaylistIndex = 0;
        var tempPlaylist = new List<MusicAsset>();
        foreach (MusicAsset track in playlist)
        {
            tempPlaylist.Add(track);
        }
        currentPlaylist = new List<MusicAsset>();
        while(tempPlaylist.Count > 0)
        {
            int randIndex = Random.Range(0, tempPlaylist.Count);
            currentPlaylist.Add(tempPlaylist[randIndex]);
            tempPlaylist.RemoveAt(randIndex);
        }
    }
    //Plays next song in playlist, or shuffles new playlist if all songs played
    public void PlayNextInPlaylist()
    {
        playingMusic = true;
        if(currentPlaylistIndex >= currentPlaylist.Count)
        {
            GenerateCurrentPlaylist();
        }
        PlayMusic(currentPlaylist[currentPlaylistIndex]);
        currentPlaylistIndex++;
    }
}
