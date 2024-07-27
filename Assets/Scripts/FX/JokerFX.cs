using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokerFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem MyJokerEffect;
    public static JokerFX instance;
    private void Awake()
    {
        instance = this;
    }

    public void PlayCubeExplosionFX(Vector3 position)
    {
        ParticleSystem ps = Instantiate(MyJokerEffect, position, Quaternion.identity);
        ps.Play();
    }
}
