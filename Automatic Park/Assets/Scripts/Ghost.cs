using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject[] targets;
    public bool scare;
    public Bees bees;

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

        if (scare)
        {
            if (bees.runner.GetComponent<Runner>().ghost == this.gameObject && bees.state == state_machine.FOLLOW)
            {
                agent.speed = 7.6f;
                agent.angularSpeed = 360;
            }
            else
            {
                agent.speed = 3.8f;
                agent.angularSpeed = 120;
            }
        }
        else
        {
            agent.speed = 3.8f;
            agent.angularSpeed = 120;
        }
    }
}