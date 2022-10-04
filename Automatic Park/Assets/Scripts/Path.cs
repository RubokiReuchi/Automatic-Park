using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    MeshRenderer render;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<MeshRenderer>();
        render.enabled = false;
    }
}
