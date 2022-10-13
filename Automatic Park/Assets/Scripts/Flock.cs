using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public Flocking_Manager myManager;
    float speed;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        myManager = GetComponentInParent<Flocking_Manager>();

        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cohesion = Vector3.zero;
        Vector3 align = Vector3.zero;
        Vector3 separation = Vector3.zero;
        int num = 0;
        foreach (GameObject go in myManager.bees)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position, transform.position);
                if (distance <= myManager.neighbour_distance)
                {
                    cohesion += go.transform.position;
                    align += go.GetComponent<Flock>().direction;
                    separation -= (transform.position - go.transform.position) / (distance * distance);
                    num++;
                }
            }
        }
        if (num > 0)
        {
            cohesion = (cohesion / num - transform.position).normalized * speed;
            align /= num;
            speed = Mathf.Clamp(align.magnitude, myManager.min_speed, myManager.max_speed);
            direction = (cohesion + align + separation).normalized * speed;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), myManager.rotation_speed * Time.deltaTime);
        transform.Translate(0.0f, 0.0f, Time.deltaTime * speed);
    }
}
