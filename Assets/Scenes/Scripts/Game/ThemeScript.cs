using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Themescript : MonoBehaviour
{
    public Button ThemeSelect;
    public Image Theme, DarkTheme, Letters, DarkLetters;

    private void Start()
    {
        if(PlayerPrefs.GetInt("Theme")==1)
        {
            Theme.gameObject.SetActive(false);
            DarkTheme.gameObject.SetActive(true);
            Letters.gameObject.SetActive(false);
            DarkLetters.gameObject.SetActive(true);
        }
        else
        {
            Theme.gameObject.SetActive(true);
            DarkTheme.gameObject.SetActive(false);
            Letters.gameObject.SetActive(true);
            DarkLetters.gameObject.SetActive(false);
        }
    }

    public void ThemeSwitch()
    {
        if(Theme.gameObject.activeInHierarchy)
        {
            PlayerPrefs.SetInt("Theme", 1);
            Theme.gameObject.SetActive(false);
            DarkTheme.gameObject.SetActive(true);
            Letters.gameObject.SetActive(false);
            DarkLetters.gameObject.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("Theme", 0);
            Theme.gameObject.SetActive(true);
            DarkTheme.gameObject.SetActive(false);
            Letters.gameObject.SetActive(true);
            DarkLetters.gameObject.SetActive(false);
        }
    }
}
