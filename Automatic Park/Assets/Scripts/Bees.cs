using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Bees : MonoBehaviour
{
    public SphereCollider col;
    Vector3 acual_point;
    public state_machine state;
    public GameObject runner;

    // Start is called before the first frame update
    void Start()
    {
        acual_point = RandomPointInBounds(col.bounds);
        state = state_machine.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case state_machine.FOLLOW:
                acual_point = new Vector3(runner.transform.position.x, runner.transform.position.y + 2, runner.transform.position.z);
                this.transform.position = Vector3.MoveTowards(this.transform.position, acual_point, 2.5f * Time.deltaTime);
                break;
            case state_machine.IDLE:
                this.transform.position = Vector3.MoveTowards(this.transform.position, acual_point, Time.deltaTime);

                if (Vector3.Distance(this.transform.position, acual_point) <= 0.1f)
                {
                    acual_point = RandomPointInBounds(col.bounds);
                }
                break;
            case state_machine.RETURN:
                
                this.transform.position = Vector3.MoveTowards(this.transform.position, acual_point, 2 * Time.deltaTime);

                if (Vector3.Distance(this.transform.position, acual_point) <= 1.0f)
                {
                    state = state_machine.IDLE;
                }
                break;
            default:
                break;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && state == state_machine.IDLE)
        {
            state = state_machine.FOLLOW;
            runner = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && state == state_machine.FOLLOW)
        {
            state = state_machine.RETURN;
            acual_point = RandomPointInBounds(col.bounds);
        }
    }

    Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(10, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
