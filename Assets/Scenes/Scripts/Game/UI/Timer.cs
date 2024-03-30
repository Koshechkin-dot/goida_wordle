using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour, IService
{
    private TextMeshProUGUI text;
    private EventBus eventBus;

    private int seconds;
    private int minutes;
    public int totalSeconds { get; private set; }
    private IEnumerator timer;

    private void Awake()
    {
        ServiceLocator.Instance.Register(this);
        Restart();
        text = GetComponent<TextMeshProUGUI>();
        eventBus = ServiceLocator.Instance.Get<EventBus>();
        eventBus.Subscribe<TimerStart>(StartTimer);
        eventBus.Subscribe<TimerStop>(StopTimer);
        timer = Counter();
    }
    private void OnDestroy()
    {
        ServiceLocator.Instance.Unregister(this);
        eventBus.Unsubscribe<TimerStart>(StartTimer);
        eventBus.Unsubscribe<TimerStop>(StopTimer);
    }
    private void Restart()
    {
        totalSeconds = 0;
        seconds = 0;
        minutes = 0;
    }
    private IEnumerator Counter()
    {
        while (true)
        {
            totalSeconds++;
            seconds++;
            if (seconds >= 60)
            {
                minutes++;
                seconds -= 60;
            }
            yield return new WaitForSeconds(1);
        }
    }

    public void StartTimer(TimerStart @event)
    {
        Restart();
        StartCoroutine(timer);
    }

    public void StopTimer(TimerStop @event)
    {
        StopCoroutine(timer);
    }

    private void LateUpdate()
    {
        text.text = $"{minutes}:{seconds:D2}";
    }
}
