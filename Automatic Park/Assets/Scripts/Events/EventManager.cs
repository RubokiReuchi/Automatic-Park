using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SENSE
{
    VISION,
    SOUND,
}

public enum TYPE
{
    SPOT,
    LOST,
}

public class PerceptionEvent
{
    public PerceptionEvent(GameObject go, SENSE sense, TYPE type)
    {
        this.go = go;
        this.sense = sense;
        this.type = type;
    }

    public GameObject go;
    public SENSE sense;
    public TYPE type;
}

public class EventManager : MonoBehaviour
{
    [HideInInspector] public List<PerceptionEvent> events = new List<PerceptionEvent>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (PerceptionEvent ev in events)
        {
            if (ev.sense == SENSE.VISION && ev.type == TYPE.SPOT)
            {
                ev.go.SendMessage("Follow_Thief");
            }
            else if (ev.sense == SENSE.VISION && ev.type == TYPE.LOST)
            {
                
            }
            else if (ev.sense == SENSE.SOUND && ev.type == TYPE.SPOT)
            {
                
            }
            else if (ev.sense == SENSE.SOUND && ev.type == TYPE.LOST)
            {
                
            }
        }
    }
}
