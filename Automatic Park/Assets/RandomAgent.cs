using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class RandomAgent : Agent
{
    Rigidbody rBody;
    ColisionCheck checker;
    Raycast hits;

    private Vector3 lastUpdatePos = Vector3.zero;
    private Vector3 dist;
    private float currentSpeed;

    bool touchedmal;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        checker = GetComponent<ColisionCheck>();
        hits = GetComponent<Raycast>();
    }

    public Transform Target;

    public override void OnEpisodeBegin()
    {
        // If the Agent fell, zero its momentum
        if (this.transform.localPosition.y < 8.4 || checker.isCurrentlyColliding == true)
        {
            //this.rBody.angularVelocity = Vector3.zero;
            //this.rBody.velocity = Vector3.zero;
            this.transform.localPosition = new Vector3(2.87f, 8.7f, -1.75f);
        }

        Target.localPosition = new Vector3(Random.value * 15 - 4,
                                           8.7f,
                                           Random.value * 15 - 4);
    }

    

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions
        sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        // Agent velocity
        //sensor.AddObservation(rBody.velocity.x);
        //sensor.AddObservation(rBody.velocity.z);

        sensor.AddObservation(this.transform.forward.x);
        sensor.AddObservation(this.transform.forward.z);
    }

    //public float forceMultiplier = 20;
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions, size = 2
        //Vector3 controlSignal = Vector3.zero;
        //controlSignal.x = actionBuffers.ContinuousActions[0];
        //controlSignal.z = actionBuffers.ContinuousActions[1];
        //rBody.AddForce(controlSignal * forceMultiplier);

        switch (actionBuffers.DiscreteActions[0])
        {
            case 0:
                break;
            case 1:
                this.transform.Translate(0, 0, 8f * Time.deltaTime);
                break;
            case 2:
                this.transform.Rotate(0, -200 * Time.deltaTime, 0);
                break;
            case 3:
                this.transform.Rotate(0, 200 * Time.deltaTime, 0);
                break;

            default:
                break;
        }

        // Rewards
        float distanceToTarget = Vector3.Distance(this.transform.localPosition, Target.localPosition);

        // Reached target
        if (distanceToTarget < 1.42f)
        {
            SetReward(1f);
            EndEpisode();
        }


        //Stopped
        dist = transform.position - lastUpdatePos;
        currentSpeed = dist.magnitude / Time.deltaTime;
        lastUpdatePos = transform.position;

        if(currentSpeed == 0.0f)
        {
            AddReward(-0.05f);
            Debug.Log("Parado!");
        }

        //Raycast
        if(hits.hitting == true)
        {
            AddReward(-0.05f);
            Debug.Log("Hit!");
        }


        // Fell off platform
        if (this.transform.localPosition.y < 8.4)
        {
            SetReward(-1f);
            EndEpisode();
        }

        if (checker.isCurrentlyColliding == true)
        {
            Debug.Log("Colliding!");
            AddReward(-0.05f);
            
        }
        

    }



}
