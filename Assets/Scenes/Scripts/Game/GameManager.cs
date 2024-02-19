using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GridManager GridManager;
    //this is only for debug
    [SerializeField] public string word;

    private GameInteractor gameInteractor;
    void Start()
    {
        gameInteractor = new GameInteractor(GridManager, this, "Words/word_storage");
        GetComponentInChildren<InputManager>().Inject(gameInteractor);
        gameInteractor.Inject(new MemoryChallengeSubmitter());
        gameInteractor.StartGame(6,6);
    }
}
