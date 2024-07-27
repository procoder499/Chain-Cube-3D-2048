using System.Collections;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;
using Color = UnityEngine.Color;

public class FX : MonoBehaviour
{
    [SerializeField] private ParticleSystem cubeExplosionFX;
    [SerializeField] private ParticleSystem blastVFX;


    //singleton class
    public static FX Instance;

    private void Awake()
    {
        Instance = this;
    }
    public void PlayCubeBlastVFX(Vector3 position)
    {
        ParticleSystem ps_1 = Instantiate(blastVFX, position, Quaternion.identity);
        ps_1.Play();
    }
    public void PlayCubeExplosionFX(Vector3 position)
    {
        ParticleSystem ps = Instantiate(cubeExplosionFX, position, Quaternion.identity);
        ps.Play();
    }
}
