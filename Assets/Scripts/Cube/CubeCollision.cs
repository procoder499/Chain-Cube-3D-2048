using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
public class CubeCollision : MonoBehaviour
{
    public static CubeCollision instance;
    Cube cube;
    [HideInInspector] public int currentScore = 0;
    public GameObject ScorePrefab;
    public bool isMainCube = false;

    private void Awake()
    {
        instance = this;
        cube = GetComponent<Cube>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("lose") && !cube.isMainCube)
        {
            Debug.Log("lose");
            EventManager.instance.LoseWindowEvent();
            SaveManager.instance.isButtonPressed = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cube"))
        {
            Cube otherCube = collision.gameObject.GetComponent<Cube>();

            // check if contacted with other cube
            if (otherCube != null && cube.CubeID > otherCube.CubeID)
            {
                // check if both cubes have same number
                if (cube.CubeNumber == otherCube.CubeNumber)
                {

                    Vector3 contactPoint = collision.contacts[0].point;

                    // check if cubes number less than max number in CubeSpawner:
                    if (otherCube.CubeNumber < CubeSpawnController.Instance.maxCubeNumber)
                    {
                        if ( cube.isImmune || otherCube.isImmune)
                        {
                            return;
                        }
                        // Spawn a new cube as a result
                        currentScore = cube.CubeNumber * 2;
                        SaveManager.instance.currentScore += currentScore;
                        if(SaveManager.instance.currentScore >= SaveManager.instance.highScore)
                        {
                            SaveManager.instance.highScore = SaveManager.instance.currentScore;
                            SaveManager.instance.Save();
                        }
                      
                        //Spawn Score
                        GameObject scoreText = Instantiate(ScorePrefab, contactPoint + Vector3.up * 1f, Quaternion.identity);
                        scoreText.GetComponent<TextMeshPro>().text = "+" + currentScore.ToString();
                        scoreText.transform.DOLocalMoveY(4f, 1f).OnComplete(() =>
                        {
                            Destroy(scoreText);
                        });
                        //Check newMaxScore
                        if(cube.CubeNumber >= SaveManager.instance.newMaxScore)
                        {
                            SaveManager.instance.newMaxScore = cube.CubeNumber*2;
                            SaveManager.instance.Save();
                        }
                        //Audio
                        Audio.instance.RisingComboHit1();
                        //Spawn new Cube
                        Cube newCube = CubeSpawnController.Instance.Spawn(cube.CubeNumber * 2, contactPoint + Vector3.up * 1f);
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
                        newCube.transform.DOScale(1.3f, 0.3f).OnComplete(() =>
                        {
                            newCube.transform.DOScale(1f, 0.3f);
                        });
                        newCube.PlayFX();

                    }
                    // Destroy the two cubes:
                    CubeSpawnController.Instance.DestroyCube(cube);
                    CubeSpawnController.Instance.DestroyCube(otherCube);

                }
            }
        }
    }

}
