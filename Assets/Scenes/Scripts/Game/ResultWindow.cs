using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Score;
    [SerializeField] private TextMeshProUGUI Timer;
    [SerializeField] private TextMeshProUGUI SecretWord;
    private EventBus bus;

    public void Awake()
    {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
        bus = ServiceLocator.Instance.Get<EventBus>();
        bus.Subscribe<ResultShowEvent>(Activate);
    }
    private void OnDestroy()
    {
        bus.Unsubscribe<ResultShowEvent>(Activate);
    }

    public void Activate(ResultShowEvent state)
    {
        gameObject.SetActive(true);
        Score.text = state.Score;
        Timer.text = state.Timer;
        SecretWord.text = state.SecretWord;   
    }

    public void OnBackButtonClick()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("Menu");
    }

    public void OnNextWordButtonClick()
    {
        gameObject.SetActive(false);
        bus.Invoke(new NextWordEvent());
    }
}
