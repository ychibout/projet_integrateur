using UnityEngine;
using System.Collections;

//rend plus facile la gestion des sons
// à ajouter dans les scripts : SoundEffectHelper.Instance.MakeExplosionSound();

public class SoundEffectsHelper : MonoBehaviour
{


  public static SoundEffectsHelper Instance;

  public AudioClip explosion;
  public AudioClip got_hit;
  public AudioClip laser_shot;
  public AudioClip looping;
  public AudioClip missile_launch;

  public void Awake()
  {
    if (Instance != null)
    {
      Debug.LogError("Multiple instances of SoundEffectsHelper!");
    }
    Instance = this;
  }

  public void MakeExplosionSound()
  {
    MakeSound(explosion);
  }

  public void MakeGotHitSound()
  {
    MakeSound(got_hit);
  }

  public void MakeLaserShotSound()
  {
    MakeSound(laser_shot);
  }

  public void MakeLoopingSound()
  {
    MakeSound(looping);
  }

  public void MakeMissileLaunchSound()
  {
    MakeSound(missile_launch);
  }

  private void MakeSound(AudioClip originalClip)
  {
    AudioSource.PlayClipAtPoint(originalClip, transform.position);
  }
}

