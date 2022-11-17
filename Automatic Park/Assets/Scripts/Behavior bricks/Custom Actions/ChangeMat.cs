using UnityEngine;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction

[Action("MyActions/ChangeMat")]
[Help("Change material.")]
public class ChangeMat : BasePrimitiveAction
{
    [InParam("Self GO")] public GameObject go;
    [InParam("New Material")] public Material mat;

    public override TaskStatus OnUpdate()
    {
        go.GetComponent<Renderer>().sharedMaterial = mat;
        return TaskStatus.COMPLETED;
    }
}