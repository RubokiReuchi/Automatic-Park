using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject[] targets;

    int i;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        i = 0;
        agent.SetDestination(targets[i].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, targets[i].transform.position) <= 1)
        {
            i++;
            if (i == targets.Length)
            {
                i = 0;
            }

            agent.SetDestination(targets[i].transform.position);
        }
    }
}
