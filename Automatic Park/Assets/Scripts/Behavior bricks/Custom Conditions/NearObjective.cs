using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/Near Objective?")]
[Help("Checks if hunter is close to his prey.")]
public class NearObjective : ConditionBase
{
    [InParam("Hunter")] public GameObject hunter;
    [InParam("Prey")] public GameObject prey;

    public override bool Check()
    {
        bool ret = (Vector3.Distance(hunter.transform.position, prey.transform.position) < 1.0f);
        return ret;
    }
}