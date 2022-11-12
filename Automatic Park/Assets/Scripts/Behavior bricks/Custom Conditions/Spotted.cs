using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/Spotted?")]
[Help("Checks if hunter saw his prey.")]
public class Spotted : ConditionBase
{
    [InParam("User Tag")] public string user;
    [OutParam("Prey")] public GameObject prey;

    bool ret = false;
    public void Steal(GameObject oldman)
    {
        if (user == "Thief")
        {
            ret = true;
            prey = oldman;
        }
    }

    public void Hide()
    {
        if (user == "Thief")
        {
            ret = true;
        }
    }

    public void GoToBench(GameObject bench)
    {
        if (user == "Oldman")
        {
            ret = true;
            prey = bench;
        }
    }

    public override bool Check()
    {
        return ret;
    }
}