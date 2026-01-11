using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    [SerializeField] private AudioClip hurtClip;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioClip winClip;
    [SerializeField] private AudioClip loseClip;

    public void HurtSoundVFX()
    {
        AudioManager.Instance.PlaySFX(hurtClip);
    }

    public void HitSoundVFX()
    {
        AudioManager.Instance.PlaySFX(hitClip);
    }

    public void WinSoundVFX()
    {
        AudioManager.Instance.PlaySFX(winClip);
    }

    public void LoseSoundVFX()
    {
        AudioManager.Instance.PlaySFX(loseClip);
    }
}
