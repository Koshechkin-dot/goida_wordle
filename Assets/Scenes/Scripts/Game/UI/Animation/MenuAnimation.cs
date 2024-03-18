using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuAnimation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Title;
    [SerializeField] private Image StartB;
    [SerializeField] private Image ExitB;
    [SerializeField] private Image[] SelectB;

    public void OnTitlePDown()
    {
        LeanTween.scale(Title.rectTransform, new Vector2(1.3f, 1.3f), 0.3f)
            .setEase(LeanTweenType.easeOutQuad);
    }

    public void OnTitlePUp()
    {
        LeanTween.scale(Title.rectTransform, new Vector2(1.0f, 1.0f), 0.3f)
            .setEase(LeanTweenType.easeOutQuad);
    }

    public void OnStartButtonPEnter()
    {
        LeanTween.scale(StartB.rectTransform, new Vector2(1.1f, 1.1f), 0.3f)
            .setEase(LeanTweenType.easeOutQuad);
    }

    public void OnStartButtonPExit()
    {
        LeanTween.scale(StartB.rectTransform, new Vector2(1.0f, 1.0f), 0.3f)
            .setEase(LeanTweenType.easeOutQuad);
    }

    public void OnExitButtonPEnter()
    {
        LeanTween.scale(ExitB.rectTransform, new Vector2(1.1f, 1.1f), 0.3f)
            .setEase(LeanTweenType.easeOutQuad);
    }

    public void OnExitButtonPExit()
    {
        LeanTween.scale(ExitB.rectTransform, new Vector2(1.0f, 1.0f), 0.3f)
            .setEase(LeanTweenType.easeOutQuad);
    }

    public void OnSelectButtonPEnter(int i)
    {
        LeanTween.scale(SelectB[i].rectTransform, new Vector2(1.1f, 1.1f), 0.3f)
            .setEase(LeanTweenType.easeOutQuad);
    }

    public void OnSelectButtonPExit(int i)
    {
        LeanTween.scale(SelectB[i].rectTransform, new Vector2(1.0f, 1.0f), 0.3f)
            .setEase(LeanTweenType.easeOutQuad);
    }
}
