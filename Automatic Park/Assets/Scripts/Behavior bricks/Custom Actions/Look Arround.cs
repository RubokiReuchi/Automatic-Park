using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction

[Action("MyActions/Look Arround")]
[Help("Rotate the vision.")]
public class LookArround : BasePrimitiveAction
{
    [InParam("GameObject")] public GameObject go;
    [InParam("Head Angle")] public int headAngle;
    [InParam("Head Speed")] public float headSpeed;

    bool finished = false;

    public override void OnStart()
    {
        //StartCoroutine
    }

    public override TaskStatus OnUpdate()
    {
        if (!finished)
        {
            return TaskStatus.RUNNING;
        }
        else
        {
            return TaskStatus.COMPLETED;
        }
    }

    IEnumerator OnUpdateAsCoroutine()
    {
        uint phase = 0; // 0 --> first right, 1 --> first left, 2 --> second right, 3 --> end head move
        float ori_y = go.transform.rotation.eulerAngles.y;
        float new_y = go.transform.rotation.eulerAngles.y;
        while (phase != 3)
        {
            switch (phase)
            {
                case 0:
                    new_y += headSpeed;
                    go.transform.rotation = Quaternion.Euler(go.transform.rotation.eulerAngles.x, new_y, go.transform.rotation.eulerAngles.z);
                    if (new_y == ori_y + headAngle) phase = 1;
                    yield return null;
                    break;
                case 1:
                    new_y -= headSpeed;
                    go.transform.rotation = Quaternion.Euler(go.transform.rotation.eulerAngles.x, new_y, go.transform.rotation.eulerAngles.z);
                    if (new_y == ori_y - headAngle) phase = 2;
                    yield return null;
                    break;
                case 2:
                    new_y += headSpeed;
                    go.transform.rotation = Quaternion.Euler(go.transform.rotation.eulerAngles.x, new_y, go.transform.rotation.eulerAngles.z);
                    if (new_y == ori_y) phase = 3;
                    yield return null;
                    break;
            }
        }
        finished= true;
    }
}