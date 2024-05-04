using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardHolder : MonoBehaviour
{
    [SerializeField] List<Key> keys;
    private EventBus bus;
    private TileState Yellow;
    private TileState Green;


    private void Awake()
    {
        
        Green = Resources.Load("TileStates/Valid") as TileState;
        Yellow = Resources.Load("TileStates/Exist") as TileState;
        bus = ServiceLocator.Instance.Get<EventBus>();
        bus.Subscribe<VirtualKeyboardEvent>(Paint);
        bus.Subscribe<VirtualKeyboardClear>(Clear);
    }
    private void OnDestroy()
    {
        bus.Unsubscribe<VirtualKeyboardEvent>(Paint);
        bus.Unsubscribe<VirtualKeyboardClear>(Clear);
    }

    private void Paint(VirtualKeyboardEvent @event)
    {
        Key temp = keys.Where(key => key.Symbol == @event.Key).Single();
        GameObject keyObject = temp.KeyCap;
        if(temp.state == KeyState.Green)
            return;
        switch(@event.State)
        {
            case KeyState.Green:
                keyObject.GetComponent<Image>().color = Green.TileColor;
                temp.state = KeyState.Green;
                break;
            case KeyState.Yellow:
                keyObject.GetComponent<Image>().color = Yellow.TileColor;
                temp.state = KeyState.Yellow;
                break;
        }   

    }
    private void Clear(VirtualKeyboardClear @event)
    {
        foreach(var key in keys)
        {
            key.KeyCap.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            key.state = KeyState.None;
        }
    }
}
