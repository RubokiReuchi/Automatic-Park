using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/Spotted?")]
[Help("Checks if hunter saw his prey.")]
public class Spotted : ConditionBase
{
    [InParam("Hunter")] public GameObject hunter;
    [OutParam("Prey")] public GameObject prey;

    bool ret = false;

    public override bool Check()
    {
        if (hunter.tag == "Thief")
        {
            ReciveSS t = hunter.GetComponent<ReciveSS>();
            if (t.spotted)
            {
                ret = true;
                prey = t.oldman;
            }
        }

        return ret;
    }
}