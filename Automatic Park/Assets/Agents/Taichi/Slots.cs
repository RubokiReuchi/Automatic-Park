using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slots : MonoBehaviour
{
    public GameObject teacherPrefab;
    public int number_of_followers;
    public GameObject followerPrefab;
    public GameObject ghost;

    void Start()
    {
        createRow(1, -2f, teacherPrefab);
        createRow(number_of_followers, -4f, followerPrefab);
    }

    void createRow(int num, float z, GameObject pf)
    {
        float pos = 1 - num;
        for (int i = 0; i < num; ++i)
        {
            Vector3 position = ghost.transform.TransformPoint(new Vector3(pos, 0f, z));
            GameObject temp = (GameObject)Instantiate(pf, position, ghost.transform.rotation);
            temp.AddComponent<Formation>();
            temp.GetComponent<Formation>().pos = new Vector3(pos, 0, z);
            temp.GetComponent<Formation>().target = ghost;
            pos += 2f;
        }
    }
}
