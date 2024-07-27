using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;
public class Cube : MonoBehaviour
{
    public static Cube Instance;

    static int staticID = 0;
    [SerializeField] private TMP_Text[] numbersText1;
    [HideInInspector] public int CubeID;
    [HideInInspector] public Color CubeColor;
    public int CubeNumber;
    [HideInInspector] public Rigidbody CubeRigidbody;

    public GameObject cubeNew;
    private MeshRenderer cubeMeshRenderer;
    void Start()
    {

    }
    private void Awake()
    {
        Instance = this;
        CubeID = staticID++;
        cubeMeshRenderer = cubeNew.GetComponent<MeshRenderer>();
        CubeRigidbody = GetComponent<Rigidbody>();
    }

    public void SetColor(Color color)
    {
        CubeColor = color;
        cubeMeshRenderer.material.color = color;
    }
        
    public void SetNumber(int number)
    {
        CubeNumber = number;
        for (int i = 0; i < 6; i++)
        {
            numbersText1[i].text = number.ToString();
        }
    }
    public void PlayFX()
    {
        FX.Instance.PlayCubeBlastVFX(transform.position);
        StartCoroutine(Explosion());
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(0.3f);
        FX.Instance.PlayCubeExplosionFX(transform.position);
    }
}
