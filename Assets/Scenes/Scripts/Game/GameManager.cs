using System;
using UnityEngine;

public class GameManager : MonoBehaviour, IService
{
    //this is only for debug
    [SerializeField] public string word;

    private GameInteractor gameInteractor;
    void Start()
    {
        ServiceLocator.Instance.Register(this);
        GameConfig config = ServiceLocator.Instance.Get<GameConfigBuilder>().GetConfig();
        ServiceLocator.Instance.Get<WordBaseLoader>().LoadBase(config.Columns);
   
        gameInteractor = new GameInteractor();
        gameInteractor.Inject(config.Submitter);
        GetComponentInChildren<InputManager>().Inject(gameInteractor);
        GetComponentInChildren<VirtualKeyboard>().Inject(gameInteractor);
        gameInteractor.StartGame(config.Rows, config.Columns);

    }

    private void OnDestroy()
    {
        gameInteractor.OnDestroy();
        ServiceLocator.Instance.Unregister(this);
    }
}
