using System;
using TMPro;
using UnityEngine;

public class StreakTimer : MonoBehaviour
{
    private TextMeshProUGUI textContainer;
    private DateTime nextDay;
    void Start()
    {
        DateTime now = DateTime.Now;
        nextDay = new DateTime(now.Year, now.Month, now.Day + 1);
        textContainer = GetComponent<TextMeshProUGUI>();
        string lastDate = PlayerPrefs.GetString("Date");
        if (lastDate != DateTime.Now.Date.ToString())
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
    void Update()
    {
        textContainer.text = nextDay.Subtract(DateTime.Now).ToString(@"hh\:mm\:ss");
    }
}
