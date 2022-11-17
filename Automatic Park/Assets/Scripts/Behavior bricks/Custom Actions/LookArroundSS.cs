using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LookArroundSS : MonoBehaviour
{
    public int headAngle;
    public float headSpeed;
    [HideInInspector] public int execute = 0; // 0 --> not working, 1 --> start, 2 --> working, 3 --> finished
    NavMeshAgent agent;

    public void StartWork()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 0;
        StartCoroutine("LookArround");
        execute = 2;
    }

    IEnumerator LookArround()
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
                    if (new_y >= ori_y + headAngle) phase = 1;
                    yield return null;
                    break;
                case 1:
                    new_y -= headSpeed;
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, new_y, transform.rotation.eulerAngles.z);
                    if (new_y <= ori_y - headAngle) phase = 2;
                    yield return null;
                    break;
                case 2:
                    new_y += headSpeed;
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, new_y, transform.rotation.eulerAngles.z);
                    if (new_y >= ori_y) phase = 3;
                    yield return null;
                    break;
            }
        }
        execute = 3;
        agent.speed = 3.5f;
    }
}
