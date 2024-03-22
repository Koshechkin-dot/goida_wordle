using UnityEngine;

public class FXButton : MonoBehaviour
{
    private AudioManager manager;
    private GameObject instance;
    void Awake()
    {
        if(instance == null)
        {
            instance = gameObject;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        manager = ServiceLocator.Instance.Get<AudioManager>();
    }
    public void Switch()
    {
        manager.MuteSwitch();
    }
}
