using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject SelectMenu;
    [SerializeField] private GameObject CustomMenu;
    [SerializeField] private Slider Rows;
    [SerializeField] private Slider Columns;

    public void OnQuitClick() => Application.Quit();

    public void OnStartClick()
    {
        MainMenu.SetActive(false);
        SelectMenu.SetActive(true);
    }

    public void OnEasyButtonClick()
    {
        ServiceLocator.Instance.Get<GameConfigBuilder>().SetGrid(6, 6).SetSubmitter(new NormalSubmitter()).SetDaily(false);
        SceneManager.LoadScene("Game");
    }
    public void OnHardButtonClick()
    {
        ServiceLocator.Instance.Get<GameConfigBuilder>().SetGrid(3, 6).SetSubmitter(new MemoryChallengeSubmitter()).SetDaily(false);
        SceneManager.LoadScene("Game");
    }
    public void OnCustomStartButtonClick()
    {
        ServiceLocator.Instance.Get<GameConfigBuilder>().SetGrid((int)Rows.value, (int)Columns.value).SetSubmitter(new NormalSubmitter()).SetDaily(false);
        SceneManager.LoadScene("Game");
    }
    public void OnCustomButtonClick()
    {
        SelectMenu.SetActive(false);
        CustomMenu.SetActive(true);
    }
    public void OnCustomBackButtonClick()
    {
        SelectMenu.SetActive(true);
        CustomMenu.SetActive(false);
    }
}
