using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DailyQuest : MonoBehaviour
{
    void Awake()
    {
        string lastDate = PlayerPrefs.GetString("Date");
        if(lastDate != DateTime.Now.Date.ToString())
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void OnClick()
    {
        PlayerPrefs.SetString("Date", DateTime.Now.Date.ToString());
        ServiceLocator.Instance.Get<DailyOrNot>().Value = true;
        ServiceLocator.Instance.Get<GameConfigBuilder>().SetGrid(5, 5).SetSubmitter(new NormalSubmitter());
        SceneManager.LoadScene("Game");
    }

}
