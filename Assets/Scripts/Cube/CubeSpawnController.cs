using UnityEngine;
using System.Collections.Generic;

public class CubeSpawnController : MonoBehaviour
{
    // Singleton class
    public static CubeSpawnController Instance;

    Queue<Cube> cubesQueue = new Queue<Cube>();
    [SerializeField] private int cubesQueueCapacity = 20;
    [SerializeField] private bool autoQueueGrow = true;

    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject jokerPrefab;
    [SerializeField] private Color[] cubeColors;
    [HideInInspector] public int maxCubeNumber;

    [SerializeField] private GameObject mainCube;
    // in our case it's 4096 (2^12)

    private int maxPower = 12;

    private Vector3 defaultSpawnPosition;

    private void Awake()
    {
        Instance = this;

        defaultSpawnPosition = transform.position;
        maxCubeNumber = (int)Mathf.Pow(2, maxPower);

        InitializeCubesQueue();
    }
    private void Start()
    {
        ControlMovement.instance.mainCube = SpawnRandom();
        ControlMovement.instance.SpawnMainCube();
        ControlMovement.instance.mainCube.isMainCube = true;

        SaveManager.instance.isButtonPressed = false;
    }

    private void InitializeCubesQueue()
    {
        for (int i = 0; i < cubesQueueCapacity; i++)
            AddCubeToQueue();
    }

    private void AddCubeToQueue()
    {
        Cube cube = Instantiate(cubePrefab, defaultSpawnPosition, Quaternion.identity, transform)
                                .GetComponent<Cube>();
        cube.gameObject.SetActive(false);
        cubesQueue.Enqueue(cube);
    }
    public Cube Spawn(int number, Vector3 position)
    {
        if (cubesQueue.Count == 0)
        {
            if (autoQueueGrow)
            {
                cubesQueueCapacity++;
                AddCubeToQueue();

            }
            else
            {
                Debug.LogError("[Cubes Queue] : no more cubes available in the pool");
                return null;
            }
        }
        Cube cube = cubesQueue.Dequeue();
        cube.transform.position = position;
        cube.SetNumber(number);
        cube.SetColor(GetColor(number)); 
        cube.gameObject.SetActive(true);
        cube.StartImmunity(0.5f);
        return cube;
    }

    public Cube SpawnBomb()
    {
        Cube bomb = Instantiate(bombPrefab, defaultSpawnPosition, Quaternion.identity, transform)
                                .GetComponent<Cube>();
        return bomb;
    }
    public Cube SpawnJoker()
    {
        Cube joker = Instantiate(jokerPrefab, defaultSpawnPosition, Quaternion.identity, transform)
                                .GetComponent<Cube>();
        return joker;
    }
    public Cube SpawnRandom()
    {
        return Spawn(GenerateRandomNumber(), defaultSpawnPosition);
    }
    public void DestroyCube(Cube cube)
    {
        cube.CubeRigidbody.velocity = Vector3.zero;
        cube.CubeRigidbody.angularVelocity = Vector3.zero;
        cube.transform.rotation = Quaternion.identity;
        cube.gameObject.SetActive(false);
        cubesQueue.Enqueue(cube);
    }

    public int GenerateRandomNumber()
    {
        if (SaveManager.instance.newMaxScore == 1024)
        {
            return (int)Mathf.Pow(2, Random.Range(2, 7));
        }
        else if (SaveManager.instance.newMaxScore == 2048)
        {
            return (int)Mathf.Pow(2, Random.Range(3, 8));
        }
        else  return (int)Mathf.Pow(2, Random.Range(1, 6));
    }

    public Color GetColor(int number)
    {
        return cubeColors[(int)(Mathf.Log(number) / Mathf.Log(2)) - 1];
    }
}
