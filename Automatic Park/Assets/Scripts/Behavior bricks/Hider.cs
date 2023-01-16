using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using System.Linq;
using Random = UnityEngine.Random;

public class Hider : MonoBehaviour
{
    public bool found;
    private NavMeshAgent agent;
    public GameObject[] hidingSpots;
    public GameObject target;
    public int index;

    //public float radius = 10.0f;
    //public float offset = 10.0f;

    //private float watchTimer = 0.0f;
    //public bool wandering;
    //public bool watching;
    // Start is called before the first frame update
    void Start()
    {
        hidingSpots = GameObject.FindGameObjectsWithTag("Hide");
        agent = GetComponent<NavMeshAgent>();
        found= false;
        index=Random.Range(0,hidingSpots.Length);
    }

    // Update is called once per frame
    void Update()
    {
        //Hide();
    }

    void Seek(Vector3 target)
    {
        agent.destination = target;
    }

    public void Hide()
    {
        Func<GameObject, float> distance =(hs) => Vector3.Distance(target.transform.position,hs.transform.position);
        GameObject hidingSpot = hidingSpots[index];
        Vector3 dir = hidingSpot.transform.position - target.transform.position;
        Ray backRay = new Ray(hidingSpot.transform.position, -dir.normalized);
        RaycastHit info;
        hidingSpot.GetComponent<Collider>().Raycast(backRay, out info, 50f);
        Seek(info.point + dir.normalized);
    }
}
