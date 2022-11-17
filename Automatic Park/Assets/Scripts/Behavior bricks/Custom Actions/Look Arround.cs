using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

[Action("MyActions/Look Arround")]
[Help("Rotate the vision.")]
public class LookArround : BasePrimitiveAction
{
    [InParam("GameObject")] public GameObject go;

    LookArroundSS look_arround;

    public override void OnStart()
    {
        look_arround = go.GetComponent<LookArroundSS>();
        look_arround.execute = 1;
        look_arround.StartWork();
    }

    public override TaskStatus OnUpdate()
    {
        if (look_arround.execute != 3)
        {
            return TaskStatus.RUNNING;
        }
        else
        {
            look_arround.execute = 0;
            GameObject.Find("Policeman").GetComponent<ReciveSS>().spotted = false;
            return TaskStatus.COMPLETED;
        }
    }
}