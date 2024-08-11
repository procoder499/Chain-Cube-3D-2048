using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject ScorePrefab;
    private bool isBomb = true;
    [SerializeField] 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cube"))
        {
            if (isBomb)
            {
                Audio.instance.BombExplosion();
                Cube otherCube = collision.gameObject.GetComponent<Cube>();
                int currentScore = otherCube.CubeNumber;
                SaveManager.instance.currentScore += currentScore;
                Vector3 contactPoint = collision.contacts[0].point;
                BombFX.instance.PlayCubeExplosionFX(transform.position);
                //Spawn Score
                GameObject scoreText = Instantiate(ScorePrefab, contactPoint + Vector3.up * 1f, Quaternion.identity);
                scoreText.GetComponent<TextMeshPro>().text = "+" + currentScore.ToString();
                scoreText.transform.DOLocalMoveY(4f, 1f).OnComplete(() =>
                {
                    Destroy(scoreText);
                });
                Destroy(collision.gameObject);
                Destroy(transform.gameObject);
                isBomb = false;
            }
        }
    }
}
