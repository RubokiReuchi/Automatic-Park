using UnityEngine;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction

[Action("MyActions/Wander")]
[Help("User Wander between some points.")]
public class Wander : BasePrimitiveAction
{
    [InParam("GameObject")] public GameObject go;
    [OutParam("Destination")] public Vector3 destination;

    public override TaskStatus OnUpdate()
    {
        WanderPath path = go.GetComponent<WanderPath>();
        destination = path.GetDestination().position;
        return TaskStatus.COMPLETED;
    }
}