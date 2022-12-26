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
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
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
        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.z);
        
    }

    public float forceMultiplier = 20;
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];
        rBody.AddForce(controlSignal * forceMultiplier);

        // Rewards
        float distanceToTarget = Vector3.Distance(this.transform.localPosition, Target.localPosition);

        // Reached target
        if (distanceToTarget < 1.42f)
        {
            SetReward(1f);
            EndEpisode();
        }
        else if (distanceToTarget < 3f)
        {
            SetReward(0.5f);
            EndEpisode();
        }
        else if (distanceToTarget < 4f)
        {
            SetReward(0.25f);
            EndEpisode();
        }




        if(hits.hitting == true)
        {
            SetReward(-0.5f);
            Debug.Log("Hit!");
        }
        else if(hits.cubo == true)
        {
            SetReward(0.5f);
            Debug.Log("Cubo!");
        }


        // Fell off platform
        if (this.transform.localPosition.y < 8.4)
        {
            SetReward(-0.5f);
            EndEpisode();
        }



        if (checker.isCurrentlyColliding == true)
        {
            Debug.Log("Colliding!");
            SetReward(-0.05f);
            EndEpisode();
        }
        

    }

}
