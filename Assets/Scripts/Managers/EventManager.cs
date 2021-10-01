using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void EventReceiver(params object[] parameters);

   static Dictionary<EventType, EventReceiver> _events = new Dictionary<EventType, EventReceiver>();

   



    public static void Subscribe (EventType eventType,EventReceiver listener)
    {
        if (!_events.ContainsKey(eventType))
            _events.Add(eventType, listener);
        else
            _events[eventType] += listener;
       
    }

    public static void Unsubscribe(EventType evenType,EventReceiver listener)
    {
        if (_events.ContainsKey(evenType))
        {
            _events[evenType] -= listener;

            if (_events[evenType] == null)
                _events.Remove(evenType);
        }

    }

    public static void Trigger (EventType eventype,params object[] parameters)
    {
        if (_events.ContainsKey(eventype))
            _events[eventype](parameters);
    }

    public static void Clear()
    {
        _events.Clear();
    }


    public enum EventType
    {

    }
}
