using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReciveSS1 : MonoBehaviour
{
    [HideInInspector] public bool spotted;

    public void Scm()
    {
        spotted = false;
    }

    public void Scream()
    {
        spotted = true;
    }
}
