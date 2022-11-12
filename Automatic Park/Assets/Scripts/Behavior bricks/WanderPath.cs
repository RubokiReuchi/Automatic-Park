using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderPath : MonoBehaviour
{
    int i;
    public Transform[] targets;
    Transform destination;

    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        destination = targets[i].transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, destination.position) <= 1)
        {
            i++;
            if (i == targets.Length)
            {
                i = 0;
            }

            destination = targets[i].transform;
        }
    }

    public Transform GetDestination() { return destination; }
}
