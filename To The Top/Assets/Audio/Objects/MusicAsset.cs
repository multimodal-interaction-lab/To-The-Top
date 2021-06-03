using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MusicAsset", menuName = "Music")]
public class MusicAsset : ScriptableObject
{
    public AudioClip audioClip;
    [Range(0f, 1f)]
    public float volume = 1f;
   
}
