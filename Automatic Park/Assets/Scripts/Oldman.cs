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
    int waiting = 0;

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
                agent.speed = 1.5f;
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
        if (waiting == 0) StartCoroutine("Wait");
        if (waiting == 1)
        {
            waiting = 2;
            if (Random.Range(0, 100) < 10)
            {
                state = OLDMAN_STATE.GO_TO_BENCH;
                this.bench = bench;
                GetComponent<Vision>().event_set = true;
            }
        }
    }

    IEnumerator Wait()
    {
        waiting = 1;
        yield return new WaitForSeconds(0.5f);
        waiting = 0;
    }
}
