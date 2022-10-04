using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dog : MonoBehaviour
{
    NavMeshAgent agent;
    bool state; // false --> idle, true --> flee
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state)
        {
            if (Vector3.Distance(transform.position, target.position) <= 1)
            {
                state = false;
            }
            else
            {
                agent.SetDestination(target.position);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bee" && state == false)
        {
            state = true;
            target.position = new Vector3(Random.Range(-5, 5), 1, Random.Range(-5, 5));
        }
    }
}
