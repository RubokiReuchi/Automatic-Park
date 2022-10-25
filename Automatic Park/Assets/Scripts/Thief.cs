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
    CHANGE, // chage cloths
    GO_TO_SIT,
    SIT,
}

public class Thief : MonoBehaviour
{
    public THIEF_STATE state;
    NavMeshAgent agent;
    public GameObject[] targets;
    public GameObject hide_spot; // after steal
    public int headAngle;
    public float headSpeed;
    bool hide;
    GameObject victim;
    public GameObject bench;

    Material material_component;
    public Material newMaterial;

    int i;

    // Start is called before the first frame update
    void Start()
    {
        state = THIEF_STATE.WANDER;
        agent = GetComponent<NavMeshAgent>();
        i = 0;
        agent.SetDestination(targets[i].transform.position);
        hide = false;
        material_component = agent.GetComponent<Material>();
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
                    state = THIEF_STATE.CHANGE;
                    material_component = newMaterial;
                }
                break;
            case THIEF_STATE.CHANGE:
                agent.speed = 0;
                StartCoroutine("Wait");
                break;
            case THIEF_STATE.GO_TO_SIT:
                agent.SetDestination(bench.transform.position);
                agent.speed = 2.5f;
                if (Vector3.Distance(transform.position, bench.transform.position) <= 1)
                {
                    state = THIEF_STATE.SIT;
                }
                break;
            case THIEF_STATE.SIT:
                agent.speed = 0;
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
        if (state == THIEF_STATE.STEAL)
        {
            state = THIEF_STATE.HIDE;
            if (!hide) hide = true;
        }
    }

    IEnumerator MoveCam()
    {
        uint phase = 0; // 0 --> first right, 1 --> first left, 2 --> second right, 3 --> end head move
        float ori_y = transform.rotation.eulerAngles.y;
        float new_y = transform.rotation.eulerAngles.y;
        while (phase != 3)
        {
            switch (phase)
            {
                case 0:
                    new_y += headSpeed;
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, new_y, transform.rotation.eulerAngles.z);
                    if (new_y == ori_y + headAngle) phase = 1;
                    yield return null;
                    break;
                case 1:
                    new_y -= headSpeed;
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, new_y, transform.rotation.eulerAngles.z);
                    if (new_y == ori_y - headAngle) phase = 2;
                    yield return null;
                    break;
                case 2:
                    new_y += headSpeed;
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, new_y, transform.rotation.eulerAngles.z);
                    if (new_y == ori_y) phase = 3;
                    yield return null;
                    break;
            }
        }
        state = THIEF_STATE.STEAL;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        state = THIEF_STATE.GO_TO_SIT;
    }
}
