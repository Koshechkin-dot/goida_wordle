using UnityEngine;

public class ServiceLocatorLoader : MonoBehaviour
{
    void Awake()
    {
        ServiceLocator.Instance.Register(GameObject.Find("Timer").GetComponent<Timer>());
        ServiceLocator.Instance.Register(new EventBus());
        ServiceLocator.Instance.Register(new WordBaseLoader());
    }
}
