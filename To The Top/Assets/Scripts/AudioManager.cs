using System.Collections;
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

    private List<MusicAsset> currentPlaylist;
    private int currentPlaylistIndex;

    private bool musicStarted;
    private bool playingMusic;
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
