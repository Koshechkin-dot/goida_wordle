using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject SelectMenu;

    public void OnQuitClick() => Application.Quit();

    public void OnStartClick()
    {
        MainMenu.SetActive(false);
        SelectMenu.SetActive(true);
    }

    public void OnEasyButtonClick()
    {
        SceneManager.LoadScene("Game");
    }
}
