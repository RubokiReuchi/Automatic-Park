using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/In Objective Position?")]
[Help("Checks if user in the objective position.")]
public class InObjectivePos : ConditionBase
{
    [InParam("Position")] public Transform spot;
    [InParam("GameObject")] public Transform user;

    public override bool Check()
    {
        return Vector3.Distance(user.transform.position, spot.position) < 0.5f;
    }
}