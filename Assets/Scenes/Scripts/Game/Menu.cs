using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void QuitApplication() => Application.Quit();

    public void StartGame() => SceneManager.LoadScene("Game", LoadSceneMode.Single);
}
