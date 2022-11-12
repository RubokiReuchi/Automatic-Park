using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/Spotted?")]
[Help("Checks if hunter saw his prey.")]
public class Spotted : ConditionBase
{
    [OutParam("Prey")] public GameObject prey;

    bool ret = false;
    public void Steal(GameObject oldman)
    {
        ret = true;
        prey = oldman;
    }

    public void Hide()
    {
        ret = true;
    }

    public void GoToBench(GameObject bench)
    {
        ret = true;
        prey = bench;
    }

    public override bool Check()
    {
        return ret;
    }
}