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

    [SerializeField]
    public AudioMixerGroup masterMixer;
    [SerializeField]
    private AudioMixerGroup musicMixer;
    [SerializeField]
    private AudioMixerGroup sfxMixer;

    private AudioSource[] audioSources;

    //Plays a given sound effect clip
    public void PlaySFX(SFXAsset sfx)
    {
        SFXSource.pitch = Random.Range(sfx.minPitch, sfx.maxPitch);
        SFXSource.volume = sfx.volume;
        SFXSource.clip = sfx.audioClip;
        if(sfx.alts.Length > 0)
        {
            int chosenClipIndex = Random.Range(0, sfx.alts.Length + 1);
            if (chosenClipIndex != sfx.alts.Length)
            {
                SFXSource.clip = sfx.alts[chosenClipIndex];
            }
        }
        SFXSource.PlayOneShot(SFXSource.clip);
    }
}
