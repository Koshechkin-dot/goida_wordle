using UnityEngine;
using TMPro;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    private AudioSource Audio;
    private string previousText = string.Empty;

    private void Start()
    {
        Audio = GetComponentInParent<AudioSource>();
    }

    private void Update()
    {
        if (textMeshPro.text != previousText)
        {
            AnimateText();
            Audio.pitch = Random.Range(0.9f, 1.1f);
            Audio.Play();
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
