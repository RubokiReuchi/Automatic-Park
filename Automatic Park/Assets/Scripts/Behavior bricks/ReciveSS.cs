using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReciveSS : MonoBehaviour
{
    [HideInInspector] public bool spotted;
    [HideInInspector] public GameObject prey;

    public void Lost()
    {
        spotted = false;
    }

    public void Steal(GameObject oldman)
    {
        spotted = true;
        prey = oldman;
    }

    public void Hide()
    {
        spotted = true;
    }
}
