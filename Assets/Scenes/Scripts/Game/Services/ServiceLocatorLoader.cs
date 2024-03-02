using UnityEngine;

public class ServiceLocatorLoader : MonoBehaviour
{
    void Awake()
    {
        ServiceLocator.Instance.Register(new EventBus());
        ServiceLocator.Instance.Register(new WordBaseLoader());
    }
}
