using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum THIEF_STATE
{
    WANDER,
    HIDE, // disimular
    STEAL,
    RUN,
    STOP,
}

public class Thief : MonoBehaviour
{
    [SerializeField] THIEF_STATE state;
    NavMeshAgent agent;
    public GameObject[] targets;
    public GameObject hide_spot; // after steal
    public Camera cam;
    int head_dir;
    bool hide;
    GameObject victim;
    MeshRenderer render;

    int i;

    // Start is called before the first frame update
    void Start()
    {
        state = THIEF_STATE.WANDER;
        agent = GetComponent<NavMeshAgent>();
        i = 0;
        agent.SetDestination(targets[i].transform.position);

        head_dir = 0;
        hide = false;

        render = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case THIEF_STATE.WANDER:
                if (Vector3.Distance(transform.position, targets[i].transform.position) <= 1)
                {
                    i++;
                    if (i == targets.Length)
                    {
                        i = 0;
                    }

                    agent.SetDestination(targets[i].transform.position);
                }
                break;
            case THIEF_STATE.HIDE:
                agent.speed = 0;
                if (hide)
                {
                    StartCoroutine("MoveCam");
                    hide = false;
                }
                break;
            case THIEF_STATE.STEAL:
                agent.SetDestination(victim.transform.position);
                if (Vector3.Distance(transform.position, victim.transform.position) <= 2)
                {
                    state = THIEF_STATE.RUN;
                    agent.speed *= 2;
                }
                break;
            case THIEF_STATE.RUN:
                agent.SetDestination(hide_spot.transform.position);
                if (Vector3.Distance(transform.position, hide_spot.transform.position) <= 1)
                {
                    state = THIEF_STATE.STOP;
                }
                break;
            case THIEF_STATE.STOP:
                agent.speed = 0;
                render.enabled = false;
                break;
            default:
                break;
        }
    }

    public void Steal(GameObject oldman)
    {
        if (state == THIEF_STATE.WANDER)
        {
            state = THIEF_STATE.STEAL;
            victim = oldman;
        }
    }

    public void Hide()
    {
        state = THIEF_STATE.HIDE;
        if (!hide) hide = true;
    }

    IEnumerator MoveCam()
    {
        StartCoroutine("Wait");
        while (head_dir != 0)
        {
            cam.transform.rotation = new Quaternion(cam.transform.rotation.w, cam.transform.rotation.x, cam.transform.rotation.y + head_dir, cam.transform.rotation.z);
            yield return null;
        }
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
