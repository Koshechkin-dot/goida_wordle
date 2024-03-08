using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour, IService
{
    //this is only for debug
    [SerializeField] public string word;

    private GameInteractor gameInteractor;
    void Start()
    {
        int rows = 6;
        int cols = 6;
        ServiceLocator.Instance.Register(this);
        ServiceLocator.Instance.Get<WordBaseLoader>().LoadBase(cols);


        gameInteractor = new GameInteractor();
        GetComponentInChildren<InputManager>().Inject(gameInteractor);
        GetComponentInChildren<VirtualKeyboard>().Inject(gameInteractor);
        gameInteractor.Inject(new NormalSubmitter());

        gameInteractor.SetGridDimensions(rows, cols).StartGame();
    }
}
