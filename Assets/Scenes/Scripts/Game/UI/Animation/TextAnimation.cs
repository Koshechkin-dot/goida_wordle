using UnityEngine;
using TMPro;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private AudioClip clip;
    private AudioManager manager;
    private string previousText = string.Empty;

    private void Start()
    {
        manager = ServiceLocator.Instance.Get<AudioManager>();
    }

    private void Update()
    {
        if (textMeshPro.text != previousText)
        {
            AnimateText();
            manager.PlaySound(clip);
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
