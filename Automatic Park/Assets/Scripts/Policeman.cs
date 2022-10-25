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
    public int headAngle;
    public float headSpeed;
    public GameObject thief;

    int i;

    // Start is called before the first frame update
    void Start()
    {
        state = POLICEMAN_STATE.WANDER;
        agent = GetComponent<NavMeshAgent>();
        i = 0;
        agent.SetDestination(targets[i].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case POLICEMAN_STATE.WANDER:
                agent.speed = 3.5f;
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
                agent.SetDestination(thief.transform.position);
                agent.speed = 7;
                if (thief.GetComponent<Thief>().state == THIEF_STATE.CHANGE)
                {
                    state = POLICEMAN_STATE.WANDER;
                    agent.SetDestination(targets[i].transform.position);
                    agent.speed = 3.5f;
                }
                break;
            default:
                break;
        }
    }

    public void ListenForHelp()
    {
        state = POLICEMAN_STATE.CATCH;
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
        state = POLICEMAN_STATE.WANDER;
        agent.SetDestination(targets[i].transform.position);
    }
}
