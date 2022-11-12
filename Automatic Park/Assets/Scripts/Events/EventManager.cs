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
    public PerceptionEvent(GameObject go_user, GameObject go_obj, SENSE sense, TYPE type)
    {
        this.go_user = go_user;
        this.go_obj = go_obj;
        this.sense = sense;
        this.type = type;
    }

    public GameObject go_user; // el que ve
    public GameObject go_obj; // el que es visto
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
                if (ev.go_user.gameObject.GetComponent<Thief>()) ev.go_user.SendMessage("Steal"); // thief
                if (ev.go_user.gameObject.GetComponent<Policeman>()) ev.go_user.SendMessage("Hide"); // thief
                if (ev.go_user.gameObject.GetComponent<Oldman>()) ev.go_user.SendMessage("GoToBench", ev.go_obj); // oldman
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
        events.Clear();
    }
}
