using System;
using System.Collections.Generic;

public class EventBus : IService
{
    private readonly Dictionary<string, List<object>> callbacks = new();

    public void Subscribe<Event>(Action<Event> callback) where Event : IEvent
    {
        string key = typeof(Event).Name;
        if(callbacks.ContainsKey(key))
            callbacks[key].Add(callback);
        else
            callbacks.Add(key, new List<object>() { callback });
    }

    public void Unsubscribe<Event>(Action<Event> callback) where Event : IEvent
    {
        string key = typeof(Event).Name;
        if(!callbacks.ContainsKey(key))
            UnityEngine.Debug.LogError($"EVENT BUS ERROR\n trying to unsubscribe for not subscribed event callback {callback}");
        else
            callbacks[key].Remove(callback);
    }
    
    public void Invoke<Event>(Event @event) where Event : IEvent
    {
        string key = typeof(Event).Name;
        if(callbacks.ContainsKey(key))
        {
            foreach(Action<Event> callback in callbacks[key])
                callback?.Invoke(@event);
        }
    }
}
