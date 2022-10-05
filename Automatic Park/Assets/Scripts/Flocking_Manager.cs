using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking_Manager : MonoBehaviour
{
    public GameObject bee_prefab;
    public int bee_count;
    [HideInInspector] public GameObject[] bees;
    public Vector3 Movement_Limit;
    public bool bounded;
    public bool randomize;
    public bool follow_lider;
    public GameObject lider;

    [Header("Bee settings")]
    [Range(0.0f, 5.0f)] public float min_speed;
    [Range(0.0f, 5.0f)] public float max_speed;
    [Range(0.0f, 10.0f)] public float neighbour_distance;
    [Range(0.0f, 5.0f)] public float rotation_speed;


    // Start is called before the first frame update
    void Start()
    {
        bees = new GameObject[bee_count];

        for (int i = 0; i < bee_count; ++i)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(1, 3), Random.Range(1, 3), Random.Range(1, 3));
            Vector3 randomize = new Vector3(Random.Range(1, 3), Random.Range(1, 3), Random.Range(1, 3));
            bees[i] = (GameObject)Instantiate(bee_prefab, pos, Quaternion.LookRotation(randomize));
            bees[i].GetComponent<Flock>().myManager = this;
            bees[i].transform.parent = gameObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
