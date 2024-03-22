using System;
using UnityEngine;

public class GameManager : MonoBehaviour, IService
{
    //this is only for debug
    [SerializeField] public string word;

    private GameInteractor gameInteractor;
    private GameInteractorDaily gameInteractorDaily;
    void Start()
    {
        ServiceLocator.Instance.Register(this);
        GameConfig config = ServiceLocator.Instance.Get<GameConfigBuilder>().GetConfig();
        ServiceLocator.Instance.Get<WordBaseLoader>().LoadBase(config.Columns);
        switch (ServiceLocator.Instance.Get<DailyOrNot>().Value)
        {
            case true:
                gameInteractorDaily = new GameInteractorDaily();
                gameInteractorDaily.Inject(config.Submitter);
                GetComponentInChildren<InputManager>().Inject(gameInteractorDaily);
                GetComponentInChildren<VirtualKeyboard>().Inject(gameInteractorDaily);
                DateTime currentDate = DateTime.Now;
                int seed = (currentDate.Year * 10000) + (currentDate.Month * 100) + currentDate.Day;
                gameInteractorDaily.StartGame(config.Rows, config.Columns, seed);
                break;
            case false:
                gameInteractor = new GameInteractor();
                gameInteractor.Inject(config.Submitter);
                GetComponentInChildren<InputManager>().Inject(gameInteractor);
                GetComponentInChildren<VirtualKeyboard>().Inject(gameInteractor);
                gameInteractor.StartGame(config.Rows, config.Columns);
                break;

        }

        
    }

    private void OnDestroy()
    {
        gameInteractor.OnDestroy();
        ServiceLocator.Instance.Unregister(this);
    }
}
