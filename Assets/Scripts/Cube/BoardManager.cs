using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class BoardManager : MonoBehaviour
{
    public static BoardManager instance;
    [SerializeField] private Transform boardPosition;
    [SerializeField] private GameObject mainBoard;
    [SerializeField] private GameObject mainBoard_1;
    private GameObject mainBoard_2;
    [SerializeField] private float moveDuration = 1f;

    [SerializeField] private GameObject currentCubeSpawner;
    [SerializeField] private GameObject cubeSpawnPrefab;
    [SerializeField] private Transform position;
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        // Update logic if needed
    }

    public void Restart()
    {
        StartCoroutine(RestartCoroutine());
        Destroy(currentCubeSpawner);
        StartCoroutine(SpawnCubeSpawner());
    }

    private IEnumerator RestartCoroutine()
    {
        Vector3 initialMainBoardPosition = mainBoard.transform.position;
        Vector3 initialMainBoard1Position = mainBoard_1.transform.position;
        Vector3 targetMainBoardPosition = initialMainBoardPosition + new Vector3(0, 0, -35f);
        Vector3 targetMainBoard1Position = initialMainBoard1Position + new Vector3(0, 0, -35f);

        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            mainBoard.transform.position = Vector3.Lerp(initialMainBoardPosition, targetMainBoardPosition, elapsedTime / moveDuration);
            mainBoard_1.transform.position = Vector3.Lerp(initialMainBoard1Position, targetMainBoard1Position, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position is set to the target position
        mainBoard.transform.position = targetMainBoardPosition;
        mainBoard_1.transform.position = targetMainBoard1Position;

        // Wait for 3 seconds
        //yield return new WaitForSeconds(1f);

        // Change the position of mainBoard to boardPosition
        mainBoard.transform.position = boardPosition.position;
        mainBoard_2 = mainBoard;
        mainBoard = mainBoard_1;
        mainBoard_1 = mainBoard_2;
    }
    IEnumerator SpawnCubeSpawner()
    {
        yield return new WaitForSeconds(1);
        currentCubeSpawner = Instantiate(cubeSpawnPrefab, position);

    }
}