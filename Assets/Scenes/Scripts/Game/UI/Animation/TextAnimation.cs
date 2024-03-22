using UnityEngine;
using TMPro;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    private string previousText = string.Empty;

    private void Update()
    {
        if (textMeshPro.text != previousText)
        {
            AnimateText();
            previousText = textMeshPro.text;
        }
    }

    private void AnimateText()
    {
        LeanTween.value(gameObject, UpdateTextAnimation, 48f, 72f, 0.5f)
            .setEase(LeanTweenType.easeOutElastic);
    }

    private void UpdateTextAnimation(float value)
    {
        textMeshPro.fontSize = value;
    }
}
