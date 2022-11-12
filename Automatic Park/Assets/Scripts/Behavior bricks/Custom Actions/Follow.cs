using UnityEngine;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction

[Action("MyActions/Follow")]
[Help("GO Follows other GO.")]
public class Follow : BasePrimitiveAction
{
    [InParam("Head")] public GameObject lider;
    [OutParam("Destination")] public Vector3 destination;

    public override TaskStatus OnUpdate()
    {
        destination = lider.transform.position;
        return TaskStatus.COMPLETED;
    }
}