using Unity.VisualScripting;
using UnityEngine;

public class ServiceLocatorLoader : MonoBehaviour
{
    void Awake()
    {
        if(!ServiceLocator.IsLoaded)
        {
            ServiceLocator.Instance.Register(new EventBus());
            ServiceLocator.Instance.Register(new WordBaseLoader());
            ServiceLocator.Instance.Register(new GameConfigBuilder());
        }   
    }
}
