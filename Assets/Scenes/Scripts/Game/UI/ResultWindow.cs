using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Score;
    [SerializeField] private TextMeshProUGUI Timer;
    [SerializeField] private TextMeshProUGUI SecretWord;
    [SerializeField] private GameObject nextWordButton;
    [SerializeField] private Image ResultW;
    [SerializeField] private Image BackB;
    [SerializeField] private Image NextWordB;
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
        nextWordButton.SetActive(!state.ShowNextButton);
        OnObjectSetActive(true);
        Score.text = state.Score;
        Timer.text = state.Timer;
        SecretWord.text = state.SecretWord;
    }
    public void OnObjectSetActive(bool state)
    {
        if (state)
        {
            LeanTween.scale(ResultW.rectTransform, new Vector3(1.2f, 1.2f, 1.0f), 1.0f)
                .setEase(LeanTweenType.easeOutQuad);
        }
        else
        {
            LeanTween.scale(ResultW.rectTransform, new Vector3(1.0f, 1.0f, 1.0f), 1.0f)
                .setEase(LeanTweenType.easeOutQuad);
        }
    }
    public void OnNextWordButtonClick()
    {
        OnObjectSetActive(false);
        gameObject.SetActive(false);
        bus.Invoke(new VirtualKeyboardClear());
        bus.Invoke(new NextWordEvent());
        bus.Invoke(new IMEvent(true));
        
    }
    public void OnBackButtonClick()
    {
        OnObjectSetActive(false);
        gameObject.SetActive(false);
        SceneManager.LoadScene("Menu");
    }

    public void OnBackButtonPEnter()
    {
        LeanTween.scale(BackB.rectTransform, new Vector3(1.1f, 1.1f, 1.0f), 0.3f)
            .setEase(LeanTweenType.easeOutQuad);
    }
    public void OnBackButtonPExit()
    {
        LeanTween.scale(BackB.rectTransform, new Vector3(1.0f, 1.0f, 1.0f), 0.3f)
            .setEase(LeanTweenType.easeOutQuad);
    }
    public void OnNextWordButtonPEnter()
    {
        LeanTween.scale(NextWordB.rectTransform, new Vector3(1.1f, 1.1f, 1.0f), 0.3f)
            .setEase(LeanTweenType.easeOutQuad);
    }
    public void OnNextWordButtonPExit()
    {
        LeanTween.scale(NextWordB.rectTransform, new Vector3(1.0f, 1.0f, 1.0f), 0.3f)
            .setEase(LeanTweenType.easeOutQuad);
    }
}
