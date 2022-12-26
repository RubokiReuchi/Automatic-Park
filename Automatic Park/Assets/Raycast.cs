using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{

    public bool hitting;
    public bool cubo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       

        Ray ray = new Ray(this.transform.position, new Vector3(1, 0, 0));
        Ray ray2 = new Ray(this.transform.position, new Vector3(0.5f, 0, 0.5f));
        Ray ray3 = new Ray(this.transform.position, new Vector3(0, 0, 1));
        Ray ray4 = new Ray(this.transform.position, new Vector3(-0.5f, 0, 0.5f));
        Ray ray5 = new Ray(this.transform.position, new Vector3(-1, 0, 0));
        Ray ray6 = new Ray(this.transform.position, new Vector3(-0.5f, 0, -0.5f));
        Ray ray7 = new Ray(this.transform.position, new Vector3(0, 0, -1));
        Ray ray8 = new Ray(this.transform.position, new Vector3(0.5f, 0, -0.5f));
       
        Debug.DrawRay(transform.position, new Vector3(1, 0, 0) * 2.5f, Color.yellow);
        Debug.DrawRay(transform.position, new Vector3(0.5f, 0, 0.5f) * 2.5f, Color.yellow);
        Debug.DrawRay(transform.position, new Vector3(0, 0, 1) * 2.5f, Color.yellow);
        Debug.DrawRay(transform.position, new Vector3(-0.5f, 0, 0.5f) * 2.5f, Color.yellow);
        Debug.DrawRay(transform.position, new Vector3(-1, 0, 0) * 2.5f, Color.yellow);
        Debug.DrawRay(transform.position, new Vector3(-0.5f, 0, -0.5f) * 2.5f, Color.yellow);
        Debug.DrawRay(transform.position, new Vector3(0, 0, -1) * 2.5f, Color.yellow);
        Debug.DrawRay(transform.position, new Vector3(0.5f, 0, -0.5f) * 2.5f, Color.yellow);

        RaycastHit hitData;
        
        if(Physics.Raycast(ray, out hitData, 2.5f) || Physics.Raycast(ray2, out hitData, 2.5f) || Physics.Raycast(ray3, out hitData, 2.5f) || Physics.Raycast(ray4, out hitData, 2.5f) || Physics.Raycast(ray5, out hitData, 2.5f) || Physics.Raycast(ray6, out hitData, 2.5f) || Physics.Raycast(ray7, out hitData, 2.5f) || Physics.Raycast(ray8, out hitData, 2.5f))
        {
            string tag = hitData.collider.tag;
            if (tag == "Mal")
            {
                hitting = true;
                
            }
            else
            {
                hitting = false;
            }


            if (tag == "Bien")
            {
                cubo = true;

            }
            else
            {
                cubo = false;
            }
        }
    }
}
