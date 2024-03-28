using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DailyResultWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Score;
    [SerializeField] private TextMeshProUGUI Timer;
    [SerializeField] private TextMeshProUGUI SecretWord;
    [SerializeField] private Image ResultW;
    [SerializeField] private Image BackB;
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
        OnObjectSetActive(true);
        Score.text = state.Score;
        Timer.text = state.Timer;
        SecretWord.text = state.SecretWord;
    }

    public void OnObjectSetActive(bool state)
    {
        if (state)
        {
            LeanTween.scale(ResultW.rectTransform, new Vector2(1.2f, 1.2f), 1.0f)
                .setEase(LeanTweenType.easeOutQuad);
        }
        else
        {
            LeanTween.scale(ResultW.rectTransform, new Vector2(1.0f, 1.0f), 1.0f)
                .setEase(LeanTweenType.easeOutQuad);
        }
    }

    public void OnBackButtonClick()
    {
        OnObjectSetActive(false);
        gameObject.SetActive(false);
        SceneManager.LoadScene("Menu");
        ServiceLocator.Instance.Get<DailyOrNot>().Value = false;
    }

    public void OnBackButtonPEnter()
    {
        LeanTween.scale(BackB.rectTransform, new Vector2(1.1f, 1.1f), 0.3f)
            .setEase(LeanTweenType.easeOutQuad);
    }
    public void OnBackButtonPExit()
    {
        LeanTween.scale(BackB.rectTransform, new Vector2(1.0f, 1.0f), 0.3f)
            .setEase(LeanTweenType.easeOutQuad);
    }
}
