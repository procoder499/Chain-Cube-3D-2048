using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControlMovement : MonoBehaviour
{
    public static ControlMovement instance;
    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private bool isDragging = false;
    private bool isMoving = false;
    private bool hasAppliedForce = false;

    public float moveSpeed = 0.01f; // Điều chỉnh moveSpeed cho phù hợp
    public float force = 8f;
    [SerializeField] public Cube mainCube;
    [SerializeField] private GameObject aimingPrefab;
    [SerializeField] private GameObject aiming;
    public float minDragDistance = 5f;

    private bool canShoot = true; // Cờ để kiểm soát việc bắn
    public float shootCooldown = 0.7f; // Thời gian chờ giữa các lần bắn

    //audio
    public AudioSource boxShot1;
    public AudioSource boxHit1;
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (mainCube == null) return; // Thêm kiểm tra để tránh NullReferenceException

        if (Input.GetMouseButtonDown(0))
        {
            if (!SaveManager.instance.isButtonPressed)
            {
                startTouchPosition = Input.mousePosition;
                isDragging = true;
                hasAppliedForce = false; // Reset flag when starting a new drag
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging && !hasAppliedForce)
        {
            isDragging = false;
            isMoving = false;
            if (canShoot) // Kiểm tra cờ trước khi bắn
            {
                ApplyForwardForce();
            }
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (!SaveManager.instance.isButtonPressed)
                {
                    startTouchPosition = touch.position;
                    isDragging = true;
                    hasAppliedForce = false; // Reset flag when starting a new drag
                }
            }
            else if ((touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) && isDragging && !hasAppliedForce)
            {
                isDragging = false;
                isMoving = false;
                if (canShoot) // Kiểm tra cờ trước khi bắn
                {
                    ApplyForwardForce();
                }
            }
        }

        if (isDragging)
        {
            if (Input.GetMouseButton(0))
            {
                currentTouchPosition = Input.mousePosition;
            }
            else if (Input.touchCount > 0)
            {
                currentTouchPosition = Input.GetTouch(0).position;
            }

            Vector2 direction = currentTouchPosition - startTouchPosition;

            if (direction.magnitude > minDragDistance)
            {
                isMoving = true;

                // Tính toán khoảng cách di chuyển và áp dụng trực tiếp
                float moveAmount = direction.x / Screen.width * 4.5f; // Điều chỉnh giá trị này theo tỷ lệ màn hình
                Vector3 targetPosition = mainCube.transform.position + new Vector3(moveAmount, 0, 0);
                targetPosition.x = Mathf.Clamp(targetPosition.x, -1.78f, 1.75f);

                mainCube.transform.position = targetPosition;

                startTouchPosition = currentTouchPosition;
            }
            else
            {
                isMoving = false;
            }
        }

        if (!isMoving)
        {
            mainCube.CubeRigidbody.velocity = new Vector3(0, mainCube.CubeRigidbody.velocity.y, mainCube.CubeRigidbody.velocity.z);
        }
    }

    private void ApplyForwardForce()
    {
        StartCoroutine(MainCubeCheckCollision(mainCube));
        if (mainCube == null) return; // Thêm kiểm tra để tránh NullReferenceException

        mainCube.CubeRigidbody.velocity = new Vector3(mainCube.CubeRigidbody.velocity.x, mainCube.CubeRigidbody.velocity.y, force);
        mainCube.CubeRigidbody.AddForce(Vector3.forward * force, ForceMode.Impulse);
        Destroy(aiming);
        boxShot1.Play();
        Invoke("BoxHit1", 0.5f);
        Invoke("SpawnNewCube", 0.5f);
        // Đặt cờ canShoot thành false và thiết lập lại sau thời gian chờ
        canShoot = false;
        Invoke("ResetShoot", shootCooldown);
    }
    private void BoxHit1()
    {
        boxHit1.Play();
    }
    private void ResetShoot()
    {
        canShoot = true;
    }

    public void SpawnNewBomb()
    {
        mainCube = CubeSpawnController.Instance.SpawnBomb();
        aiming = Instantiate(aimingPrefab, mainCube.transform.position, mainCube.transform.rotation);
        aiming.transform.SetParent(mainCube.transform);
    }

    private void SpawnNewCube()
    {
        mainCube = CubeSpawnController.Instance.SpawnRandom();
        mainCube.isMainCube = true;
        //Dotween xuat hien cube
        mainCube.transform.localScale = Vector3.zero;

        mainCube.transform.DOScale(1, 0.3f).SetEase(Ease.OutBounce);
        StartCoroutine(aim());
    }

    public void SpawnMainCube()
    {
        StartCoroutine(aim());
    }

    IEnumerator aim()
    {
        yield return new WaitForSeconds(0.1f);
        aiming = Instantiate(aimingPrefab, mainCube.transform.position, mainCube.transform.rotation);
        aiming.transform.SetParent(mainCube.transform);
    }

    IEnumerator MainCubeCheckCollision(Cube cube)
    {
        yield return new WaitForSeconds(1.5f);
        cube.isMainCube = false;
    }
}