using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/Spotted?")]
[Help("Checks if hunter saw his prey.")]
public class Spotted : ConditionBase
{
    bool ret = false;
    public void Steal()
    {
        ret = true;
    }

    public void Hide()
    {
        ret = true;
    }

    public void GoToBench()
    {
        ret = true;
    }

    public override bool Check()
    {
        return ret;
    }
}