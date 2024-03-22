using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int ScoreCount;
    private TextMeshProUGUI text;
    private EventBus eventBus;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();

        eventBus = ServiceLocator.Instance.Get<EventBus>();
        eventBus.Subscribe<ScoreChanged>(UpdateScore);
        eventBus.Subscribe<ScoreClear>(ScoreClear);

        ScoreCount = PlayerPrefs.GetInt("Score");
        text.text = ScoreCount.ToString();
    }

    private void OnDestroy()
    {
        eventBus.Unsubscribe<ScoreChanged>(UpdateScore);
        eventBus.Unsubscribe<ScoreClear>(ScoreClear);

        PlayerPrefs.Save();
    }

    public void UpdateScore(ScoreChanged score)
    {
        ScoreCount += score.Score;
        ScoreCount = Mathf.Clamp(ScoreCount, 0, int.MaxValue);
        text.text = ScoreCount.ToString();

        PlayerPrefs.SetInt("Score", ScoreCount);
        PlayerPrefs.Save();
    }

    public void ScoreClear(ScoreClear score)
    {
        ScoreCount = 0;
        text.text = ScoreCount.ToString();

        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.Save();
    }
}
