using TMPro;
using UnityEngine;

public class DailyStreak : MonoBehaviour
{
    private TextMeshProUGUI textContainer;
    private void Start()
    {
        textContainer = GetComponent<TextMeshProUGUI>();
        textContainer.text = PlayerPrefs.GetInt("DailyStreak").ToString();
    }
}
