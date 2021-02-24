using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to add sound to a button 
/// </summary>
[RequireComponent(typeof(UIButtonUtility))]
public class SoundButton : ButtonComponent
{
    /// <summary>
    /// Sound to play by the SoundManager
    /// </summary>
    [SerializeField]
    private UISound soundToPlay;

    /// <summary>
    /// Plays the sound.
    /// </summary>
    public override void Release()
    {
        SoundManager.Instance.PlayOneShoot(soundToPlay.ToString());
    }
}
