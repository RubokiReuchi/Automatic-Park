using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum state_machine
{
    FOLLOW,
    FLEE,
    IDLE,
    RETURN,
}

public class Dog : MonoBehaviour
{
    public GameObject runner;
    [SerializeField] state_machine state;
    NavMeshAgent agent;
    public GameObject[] points;
    int point_selected;
    public float max_speed;
    public float acceleration;
    public float time_idle;

    bool in_water;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        state = state_machine.FOLLOW;
        in_water = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case state_machine.FOLLOW:
                agent.speed = max_speed;
                agent.SetDestination(runner.transform.position);
                agent.acceleration = acceleration;
                break;
            case state_machine.FLEE:
                agent.SetDestination(points[point_selected].transform.position);
                if (in_water) agent.speed = max_speed;
                else agent.speed = max_speed * 2;
                agent.acceleration = acceleration * 2;

                if (Vector3.Distance(this.transform.position, points[point_selected].transform.position) <= 1.0f)
                {
                    state = state_machine.IDLE;
                    StartCoroutine(Rest());
                }
                break;
            case state_machine.IDLE:
                agent.speed = 0.0f;
                transform.position = new Vector3(points[point_selected].transform.position.x, points[point_selected].transform.position.y, points[point_selected].transform.position.z);
                break;
            case state_machine.RETURN:
                agent.SetDestination(runner.transform.position);
                if (in_water) agent.speed = max_speed;
                else agent.speed = max_speed * 2;
                agent.acceleration = acceleration * 2;

                if (Vector3.Distance(this.transform.position, runner.transform.position) <= 2)
                {
                    state = state_machine.FOLLOW;
                }
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bee") && state == state_machine.FOLLOW)
        {
            state = state_machine.FLEE;
            point_selected = Random.Range(0, points.Length);
        }
        if (other.CompareTag("Water"))
        {
            in_water = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            in_water = false;
        }
    }

    IEnumerator Rest()
    {
        yield return new WaitForSeconds(time_idle);
        state = state_machine.RETURN;
    }
}