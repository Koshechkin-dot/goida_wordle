using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DailyQuest : MonoBehaviour
{
    void Start()
    {
        string lastDate = PlayerPrefs.GetString("Date");
        if(lastDate != DateTime.Now.Date.ToString())
        {
            GetComponent<EventTrigger>().enabled = true;
        }
        else
        {
            GetComponent<EventTrigger>().enabled = false;
        }
    }

    public void OnClick()
    {
        PlayerPrefs.SetString("Date", DateTime.Now.Date.ToString());
        ServiceLocator.Instance.Get<GameConfigBuilder>().SetGrid(5, 5).SetSubmitter(new NormalSubmitter()).SetDaily(true);
        SceneManager.LoadScene("Game");
    }

}
