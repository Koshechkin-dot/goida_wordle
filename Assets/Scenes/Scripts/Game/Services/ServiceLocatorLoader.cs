using Unity.VisualScripting;
using UnityEngine;

public class ServiceLocatorLoader : MonoBehaviour
{
    void Awake()
    {
        if(!ServiceLocator.IsLoaded)
        {
            ServiceLocator.Instance.Register(GameObject.Find("Timer").GetComponent<Timer>());
            ServiceLocator.Instance.Register(new EventBus());
            ServiceLocator.Instance.Register(new WordBaseLoader());
            ServiceLocator.Instance.Register(new GameConfigBuilder());
            ServiceLocator.Instance.Register(new GameObject().AddComponent<AudioSource>().AddComponent<AudioManager>());
        }   
    }
}
