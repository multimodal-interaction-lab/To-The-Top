using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SFXAsset", menuName = "SFX")]
public class SFXAsset : ScriptableObject
{
    public AudioClip audioClip;
    [Range(0f, 1f)]
    public float volume = 1f;
    //Used if pitch will be varied
    [Range(1, 3)]
    public float maxPitch = 1;
    [Range(.1f, 1)]
    public float minPitch = 1;
    [Tooltip("If there are multiple variations of the same sound effect, put them here.")]
    public AudioClip[] alts;
}
