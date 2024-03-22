using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DailyResultWindow : MonoBehaviour
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
        bus.Subscribe<ResultShowDailyEvent>(Activate);
    }
    private void OnDestroy()
    {
        bus.Unsubscribe<ResultShowDailyEvent>(Activate);
    }

    public void Activate(ResultShowDailyEvent state)
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
        ServiceLocator.Instance.Get<DailyOrNot>().Value = false;
    }
}
