using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio instance;
    public AudioSource audioSource;
    public AudioSource bombExplosion;
    public AudioSource hearHit;
    public AudioSource smallSuccess;

    private void Awake()
    {
       instance = this; 
    }
    public void RisingComboHit1()
    {
        audioSource.Play();
    }
    public void BombExplosion()
    {
        bombExplosion.Play();
    }
    public void HearHit()
    {
        hearHit.Play();
    }
    public void SmallSuccess()
    {
        smallSuccess.Play();
    }
}
