using TMPro;
using UnityEngine;

public class TimeViewer : MonoBehaviour
{
    private TextMeshProUGUI text;
    private EventBus eventBus;

    private int seconds = 0;
    private int minuts = 0;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();

        eventBus = ServiceLocator.Instance.Get<EventBus>();
        eventBus.Subscribe<TimerTick>(TimerUpdate);
        eventBus.Subscribe<TimerRestart>(TimerRestart);
        UpdateGUI();
    }
    private void OnDestroy()
    {
        eventBus.Unsubscribe<TimerTick>(TimerUpdate);
        eventBus.Unsubscribe<TimerRestart>(TimerRestart);
    }

    private void TimerUpdate(TimerTick tick)
    {
        seconds++;
        if (seconds >= 60)
        {
            minuts++;
            seconds -= 60;
        }
        UpdateGUI();
    }

    private void TimerRestart(TimerRestart stop)
    {
        seconds = 0;
        minuts = 0;
        UpdateGUI();
    }

    private void UpdateGUI() => text.text = $"{minuts}:{seconds}";
}
