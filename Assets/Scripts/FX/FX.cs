using System.Collections;
using UnityEngine;

public class FX : MonoBehaviour
{
    [SerializeField] private ParticleSystem cubeExplosionFX;
    [SerializeField] private ParticleSystem blastVFX;

    // Singleton class
    public static FX Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayCubeBlastVFX(Vector3 position)
    {
        ParticleSystem ps_1 = Instantiate(blastVFX, position, Quaternion.identity);
        ps_1.Play();
        StartCoroutine(RemoveParticle(ps_1));
    }

    IEnumerator RemoveParticle(ParticleSystem fx)
    {
        // Wait until the particle system and all its sub-emitters are no longer alive
        while (fx.IsAlive(true))
        {
            yield return null;
        }
        Destroy(fx.gameObject);
    }

    public void PlayCubeExplosionFX(Vector3 position)
    {
        ParticleSystem ps = Instantiate(cubeExplosionFX, position, Quaternion.identity);
        ps.Play();
    }
}