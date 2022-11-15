using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/Spotted?")]
[Help("Checks if hunter saw his prey.")]
public class Spotted : ConditionBase
{
    [InParam("Hunter")] public GameObject hunter;
    [OutParam("Prey")] public GameObject prey;

    bool ret = true;

    public override bool Check()
    {
        if (hunter.tag == "Thief")
        {
            ThiefSS t = hunter.GetComponent<ThiefSS>();
            if (t.spotted)
            {
                ret = true;
                prey = t.oldman;
            }
        }

        return ret;
    }
}