using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/Scream?")]
[Help("Checks if hunter saw his prey.")]
public class ScreamListener : ConditionBase
{
    [InParam("Hunter")] public GameObject hunter;

    public override bool Check()
    {
        if (hunter.tag == "Policeman")
        {
            ReciveSS1 t = hunter.GetComponent<ReciveSS1>();
            if (t.spotted)
            {
                return true;
            }
        }

        return false;
    }
}