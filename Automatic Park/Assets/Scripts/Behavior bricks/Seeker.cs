using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Seeker : MonoBehaviour
{
    
    NavMeshAgent agent;

    public float radius = 10.0f;
    public float offset = 10.0f;

    private float watchTimer = 0.0f;
    public bool wandering;
    public bool watching;

    private float countTimer = 0.0f;
    private Vector3 countPos = new Vector3(-2.1f, 9.0f, 11.5f);
    private bool hasTocount = true;
    


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Parameters for wander
        wandering = true;
        watching = true;
        agent.velocity = new Vector3(0.0f, 0.0f, 5.0f);

        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasTocount)
            Counting();
        else
        {
            //Debug.Log("Is Wandering");
            Wander();
        }
            
    }

    public void Wander()
    {
        watchTimer += Time.deltaTime;
        

        if (wandering)
        {
            Vector3 localTarget = UnityEngine.Random.insideUnitCircle * radius;
            localTarget += new Vector3(offset, 0, offset);

            Vector3 worldTarget = transform.TransformPoint(localTarget);
            worldTarget.y = 0f;

            agent.destination = worldTarget;

            wandering = false;
            watching = true;

        }

        if (Vector3.Distance(agent.destination, this.transform.position) < 0.1 && !wandering)
        {
            if (watching)
            {
                watchTimer = 0.0f;
                watching = false;

            }
           
            else
            {
                wandering = true;
            }
        }
        else
        {
            watching = true;
        }
    }

    public void Counting()
    {
        agent.destination = countPos;


        if (agent.velocity == new Vector3(0, 0, 0) && countTimer < 10f)
        {
            countTimer += Time.deltaTime;
            //Debug.Log(countTimer);
            

        }
        if (countTimer >= 10f)
        {
           //Debug.Log("Finished Counting");
            hasTocount = false;

        }
            
    }
    


}
