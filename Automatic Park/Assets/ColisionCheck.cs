using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionCheck : MonoBehaviour
{

    public bool isCurrentlyColliding;
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Mal")
        {
            isCurrentlyColliding = true;

        }
    }

    void OnCollisionExit(Collision col)
    {
        isCurrentlyColliding = false;
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
