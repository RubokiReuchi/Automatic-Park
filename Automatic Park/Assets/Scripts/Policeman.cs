using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum POLICEMAN_STATE
{
    WANDER,
    LOOK,
    CATCH,
}

public class Policeman : MonoBehaviour
{
    [SerializeField] POLICEMAN_STATE state;
    NavMeshAgent agent;
    public GameObject[] targets;
    public Camera cam;
    int head_dir;
    GameObject victim;

    int i;

    // Start is called before the first frame update
    void Start()
    {
        state = POLICEMAN_STATE.WANDER;
        agent = GetComponent<NavMeshAgent>();
        i = 0;
        agent.SetDestination(targets[i].transform.position);

        head_dir = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case POLICEMAN_STATE.WANDER:
                if (Vector3.Distance(transform.position, targets[i].transform.position) <= 1)
                {
                    i++;
                    if (i == targets.Length)
                    {
                        i = 0;
                    }

                    state = POLICEMAN_STATE.LOOK;
                    StartCoroutine("MoveCam");
                }
                break;
            case POLICEMAN_STATE.LOOK:
                agent.speed = 0;
                break;
            case POLICEMAN_STATE.CATCH:
                break;
            default:
                break;
        }
    }

    IEnumerator MoveCam()
    {
        StartCoroutine("Wait");
        while (head_dir != 0)
        {
            cam.transform.rotation = new Quaternion(cam.transform.rotation.w, cam.transform.rotation.x, cam.transform.rotation.y + head_dir, cam.transform.rotation.z);
            yield return null;
        }
        state = POLICEMAN_STATE.WANDER;
        agent.SetDestination(targets[i].transform.position);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        head_dir = -1;
        yield return new WaitForSeconds(1 * 2);
        head_dir = 1;
        yield return new WaitForSeconds(1);
        head_dir = 0;
    }
}
