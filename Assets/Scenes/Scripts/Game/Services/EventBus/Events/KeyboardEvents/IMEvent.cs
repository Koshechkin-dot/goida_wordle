public class IMEvent : IEvent
{
    public bool IsEnabled { get; private set; }
    public IMEvent(bool isEnabled)
    {
        IsEnabled = isEnabled;
    }
}
