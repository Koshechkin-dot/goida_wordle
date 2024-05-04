using UnityEngine;
using UnityEngine.UI;


public class Key : MonoBehaviour
{
    public GameObject KeyCap;
    public string Symbol;
    public KeyState state;
    private void Start()
    {
        KeyCap = gameObject;
    }
}

public enum KeyState
{
    Green,
    Yellow,
    None
}
