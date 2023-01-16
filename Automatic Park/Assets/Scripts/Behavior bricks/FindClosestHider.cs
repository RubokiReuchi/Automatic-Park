using UnityEngine;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using Pada1.BBCore.Framework;
using System.Linq;

[Action("Hider/FindClosestHider")]
[Help("Get the Closest Free Hider.")]
public class BBFindClosestCop : BasePrimitiveAction
{
    [OutParam("game object")]
    [Help("Nearest free hider.")]
    public GameObject go;

    public override TaskStatus OnUpdate()
    {
        var l = GameObject.FindGameObjectsWithTag("Hider").Where(x => !x.GetComponent<Hider>().found);
        if (l.Count() == 0)
            return TaskStatus.FAILED;
        go = l.First();
        go.GetComponent<Hider>().found = true;
        return TaskStatus.COMPLETED;
    }
}