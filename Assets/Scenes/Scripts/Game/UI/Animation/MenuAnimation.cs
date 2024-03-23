using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuAnimation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Title;
    [SerializeField] private TextMeshProUGUI InfoField;
    [SerializeField] private Image StartB;
    [SerializeField] private Image ExitB;
    [SerializeField] private Image DailyB;
    [SerializeField] private Image[] SelectB;

    private void SetInfoText(string text) => InfoField.text = text;
    private void Start()
    {
        InfoField.CrossFadeAlpha(0f, 0.2f, true);
    }

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

    public void OnDailyButtonPEnter()
    {
        LeanTween.scale(DailyB.rectTransform, new Vector2(1.1f, 1.1f), 0.3f)
            .setEase(LeanTweenType.easeOutQuad);
        SetInfoText("����� ����� ������ ����, ������� ��� ��������.");
        InfoField.CrossFadeAlpha(1f, 0.2f, true);
    }

    public void OnDailyButtonPExit()
    {
        LeanTween.scale(DailyB.rectTransform, new Vector2(1.0f, 1.0f), 0.3f)
            .setEase(LeanTweenType.easeOutQuad);
        InfoField.CrossFadeAlpha(0f, 0.2f, true);
    }

    public void OnSelectButtonPEnter(int i)
    {
        LeanTween.scale(SelectB[i].rectTransform, new Vector2(1.1f, 1.1f), 0.3f)
            .setEase(LeanTweenType.easeOutQuad);
        switch (i)
        {
            case 0:
                SetInfoText("�������� ����� �� ����� ���� �� ����� �������, ������ ���������� �����, ��������������� ������� ���������, ����� - ��������������, �� � ���� �������, �������, ���� ��������������� ����� ��� ������� � ����������� � ����� ��������.");
                break;
            case 1:
                SetInfoText("����� �� ����� ���� ���������� ������ �� ��� �������, ��� ���� ��� ������������� � ���������� ����� ����� ���������� ���������.");
                break;
            case 2:
                SetInfoText("��������� ���� ����� ����, �� ��� �� ������ ���� � ����� � �� ����� �� ������ ��������� ������� ����������� �����.");
                break;
            default:
                SetInfoText(string.Empty);
                break;
        }
        InfoField.CrossFadeAlpha(1f, 0.2f, true);
    }

    public void OnSelectButtonPExit(int i)
    {
        LeanTween.scale(SelectB[i].rectTransform, new Vector2(1.0f, 1.0f), 0.3f)
            .setEase(LeanTweenType.easeOutQuad);
        InfoField.CrossFadeAlpha(0f, 0.2f, true);
    }

    public void OnSelectButtonPUp()
    {
        SetInfoText(string.Empty);
    }
}
