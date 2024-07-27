using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joker : MonoBehaviour
{
    private bool isJoker = true;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cube"))
        {
            Cube otherCube = collision.gameObject.GetComponent<Cube>();
            if(isJoker)
            {
                JokerFX.instance.PlayCubeExplosionFX(transform.position);
                Cube newCube = CubeSpawnController.Instance.Spawn(otherCube.CubeNumber * 2, transform.position + Vector3.up * 1f);
                Cube[] allCubes = FindObjectsOfType<Cube>();
                Vector3 target = Vector3.zero;
                foreach (Cube c in allCubes)
                {
                    if (c.CubeNumber == newCube.CubeNumber && c != newCube)
                    {
                        target = c.transform.position;
                        break;
                    }
                }
                Vector3 directionToTarget = (target - newCube.transform.position).normalized;
                Vector3 direction = new Vector3(0, 2f, 0.5f);
                newCube.CubeRigidbody.AddForce(direction * 4, ForceMode.Impulse);
                newCube.CubeRigidbody.AddForce(directionToTarget, ForceMode.Impulse);
                newCube.CubeRigidbody.AddTorque(new Vector3(
                    Random.Range(-100f, 100f),
                    Random.Range(-100f, 100f),
                    Random.Range(-100f, 100f)
                ), ForceMode.Impulse);
            }
            Destroy(transform.gameObject);
            Destroy(otherCube.gameObject);
            isJoker = false;
        }
    }
}
