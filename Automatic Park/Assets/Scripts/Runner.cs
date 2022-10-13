using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Runner : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject ghost;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(ghost.transform.position);

        agent.speed = ghost.GetComponent<Ghost>().agent.speed - 0.3f;
        agent.angularSpeed = ghost.GetComponent<Ghost>().agent.angularSpeed;
    }
}
