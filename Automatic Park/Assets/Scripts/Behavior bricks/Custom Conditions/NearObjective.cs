using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/Near Objective?")]
[Help("Checks if hunter is close to his prey.")]
public class NearObjective : ConditionBase
{
    [InParam("Hunter")] public string hunter_tag;
    [InParam("Prey")] public string prey_tag;

    public override bool Check()
    {
        GameObject hunter = GameObject.Find(hunter_tag);
        GameObject prey = GameObject.Find(prey_tag);
        return Vector3.Distance(hunter.transform.position, prey.transform.position) < 1.0f;
    }
}