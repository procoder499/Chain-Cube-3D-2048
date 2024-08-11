using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ColorCube : MonoBehaviour
{
    private MeshRenderer cubeMeshRenderer;
    public Color color;
    private void Awake()
    {
        cubeMeshRenderer = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        cubeMeshRenderer.material.color = color;
    }
}
