using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IService
{
    //this is only for debug
    [SerializeField] public string word;
    [SerializeField] private GameObject warningWindow;

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


    public void BackToMenu()
    {
        if (gameInteractor.GetTryCounter() == 0)
            SceneManager.LoadScene("Menu");
        else
        {
            warningWindow.SetActive(true);
        }
    }
    public void BackAfterWarning()
    {
        ServiceLocator.Instance.Get<EventBus>().Invoke(new ScoreMinus());
        SceneManager.LoadScene("Menu");
    }
}
