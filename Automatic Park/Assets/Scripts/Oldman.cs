using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum OLDMAN_STATE
{
    WANDER,
    GO_TO_BENCH,
    SIT,
}

public class Oldman : MonoBehaviour
{
    [SerializeField] OLDMAN_STATE state;
    NavMeshAgent agent;
    public GameObject[] targets;
    GameObject bench;

    int i;

    // Start is called before the first frame update
    void Start()
    {
        state = OLDMAN_STATE.WANDER;
        agent = GetComponent<NavMeshAgent>();
        i = 0;
        agent.SetDestination(targets[i].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case OLDMAN_STATE.WANDER:
                agent.speed = 8f;
                if (Vector3.Distance(transform.position, targets[i].transform.position) <= 1)
                {
                    i++;
                    if (i == targets.Length)
                    {
                        i = 0;
                    }

                    GetComponent<Vision>().event_set = false;
                    agent.SetDestination(targets[i].transform.position);
                }
                break;
            case OLDMAN_STATE.GO_TO_BENCH:
                agent.speed = 1.5f;
                agent.SetDestination(bench.transform.position);
                if (Vector3.Distance(transform.position, bench.transform.position) <= 1)
                {
                    state = OLDMAN_STATE.SIT;
                }
                break;
            case OLDMAN_STATE.SIT:
                agent.speed = 0;
                break;
            default:
                break;
        }
    }

    public void GoToBench(GameObject bench)
    {
        if (Random.Range(0, 100) < 100)
        {
            state = OLDMAN_STATE.GO_TO_BENCH;
            this.bench = bench;
        }
    }
}
