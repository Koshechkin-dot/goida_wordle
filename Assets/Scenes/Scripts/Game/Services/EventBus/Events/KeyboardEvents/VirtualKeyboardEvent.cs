public class VirtualKeyboardEvent : IEvent
{
    public KeyState State;
    public string Key;

    public VirtualKeyboardEvent(KeyState state, string key)
    {
        State = state;
        Key = key;
    }
}
