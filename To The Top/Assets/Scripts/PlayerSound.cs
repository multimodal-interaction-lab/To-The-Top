using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    //Plays a given sound to the client player only
    public void PlaySFX(SFXAsset sfx)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(sfx);
        }
    }
}