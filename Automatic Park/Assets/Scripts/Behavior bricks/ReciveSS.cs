using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReciveSS : MonoBehaviour
{
    [HideInInspector] public bool spotted;
    [HideInInspector] public GameObject oldman;

    public void Steal(GameObject oldman)
    {
        spotted = true;
        this.oldman = oldman;
    }
}