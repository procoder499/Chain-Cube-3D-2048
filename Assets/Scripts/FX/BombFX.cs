using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem Explosion;
    public static BombFX instance;
    private void Awake()
    {
        instance = this;
    }

    public void PlayCubeExplosionFX(Vector3 position)
    {
        ParticleSystem ps = Instantiate(Explosion, position, Quaternion.identity);
        ps.Play();
    }
}
