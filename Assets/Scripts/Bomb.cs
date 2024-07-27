using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cube"))
        {
            BombFX.instance.PlayCubeExplosionFX(transform.position);
            Destroy(collision.gameObject);
            Destroy(transform.gameObject);
        }
    }
}
