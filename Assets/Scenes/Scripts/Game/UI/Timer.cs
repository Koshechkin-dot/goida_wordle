using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour, IService
{
    private int TotalSeconds = 0;

    private EventBus eventBus;

    private IEnumerator counter;

    private void Start()
    {
        DontDestroyOnLoad(this);
        eventBus = ServiceLocator.Instance.Get<EventBus>();
        counter = Counter();
    }

    private IEnumerator Counter()
    {
        while (true)
        {
            eventBus.Invoke(new TimerTick());
            TotalSeconds++;
            yield return new WaitForSeconds(1);
        }               
    }

    public void StartTimer()
    {
        TotalSeconds = 0;
        eventBus.Invoke(new TimerRestart());
        StartCoroutine(counter);
    }

    public int StopTimer()
    {
        StopCoroutine(counter);
        return TotalSeconds;
    }
}
