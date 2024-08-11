using System.Collections;
using UnityEngine;

public class CubeSpawnStartGame : MonoBehaviour
{
    public Transform[] introPositions_1;
    public Transform[] introPositions_2;
    public Transform[] introPositions_3;
    public Transform[] introPositions_4;
    [SerializeField] private Cube cubePrefab;
    private void Awake()
    {

    }
    private void Start()
    {
        StartCoroutine(CallRandomSpawnCoroutine());
    }
    private IEnumerator CallRandomSpawnCoroutine()
    {
        // Create an array of coroutines
        IEnumerator[] spawnCoroutines = new IEnumerator[]
        {
            SpawnCubes(introPositions_1, timeSkip_1),
            SpawnCubes(introPositions_2, timeSkip_2),
            SpawnCubes(introPositions_3, timeSkip_3),
            SpawnCubes(introPositions_4, timeSkip_4)
        };

        // Select a random coroutine to execute
        System.Random random = new System.Random();
        int randomIndex = random.Next(spawnCoroutines.Length);
        yield return StartCoroutine(spawnCoroutines[randomIndex]);
    }

    private IEnumerator SpawnCubes(Transform[] positions, System.Func<int, IEnumerator> timeSkipFunc)
    {
        for (int i = 0; i < positions.Length; i++)
        {
            yield return StartCoroutine(timeSkipFunc(i));
        }
    }

    private IEnumerator timeSkip_1(int i)
    {
        yield return new WaitForSeconds(0.1f);
        int number = i <= 3 ? 2 : i <= 7 ? 4 : i <= 11 ? 8 : 16;
        Spawn(number, introPositions_1[i].position);
    }

    private IEnumerator timeSkip_2(int i)
    {
        yield return new WaitForSeconds(0.1f);
        Spawn(4, introPositions_2[i].position);
    }

    private IEnumerator timeSkip_3(int i)
    {
        yield return new WaitForSeconds(0.1f);
        int number = i <= 3 ? 2 : i <= 7 ? 4 : 8;
        Spawn(number, introPositions_3[i].position);
    }

    private IEnumerator timeSkip_4(int i)
    {
        yield return new WaitForSeconds(0.1f);
        int number = i == 0 || i == 2 ? 2 : i == 1 || i == 4 ? 4 : 8;
        Spawn(number, introPositions_4[i].position);
    }

    private void Spawn(int number, Vector3 position)
    {
        Cube cube = Instantiate(cubePrefab, position, Quaternion.identity, transform)
                 .GetComponent<Cube>();
        cube.SetNumber(number);
        cube.SetColor(CubeSpawnController.Instance.GetColor(number));

        //PhysicMaterial bounceMaterial = new PhysicMaterial();
        //bounceMaterial.bounciness = 0.2f; // Adjust bounce as needed
        //bounceMaterial.frictionCombine = PhysicMaterialCombine.Minimum;
        //bounceMaterial.bounceCombine = PhysicMaterialCombine.Maximum;

        //cube.GetComponent<Collider>().material = bounceMaterial;

        // Apply force to make the cube fall faster
        Rigidbody rb = cube.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.down * 13f, ForceMode.Impulse); // Adjust the force value as needed

    }
}