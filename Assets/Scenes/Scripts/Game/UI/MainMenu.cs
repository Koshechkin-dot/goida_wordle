using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Button startB;
    private Button quitB;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        startB = root.Q<Button>("Start");
        quitB = root.Q<Button>("Quit");

        startB.clicked += onStartBClick;
        quitB.clicked += onQuitBClick;
    }

    void onStartBClick()
    {
        SceneManager.LoadScene("TransitionScene");
    }
    void onQuitBClick()
    {
        Application.Quit();
    }
}
