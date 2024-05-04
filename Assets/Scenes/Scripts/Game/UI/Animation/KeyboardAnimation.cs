using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyboardAnimation : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI textMesh;
    private Color curr_c;

    public void OnKeyPDown()
    {
        curr_c = image.color;
        image.color = Color.black;
        textMesh.color = Color.white;
    }

    public void OnKeyPUp()
    {
        LeanTween.value(image.gameObject, image.color, curr_c, 0.5f).setOnUpdate((Color val) =>
        {
            image.color = val;
        });

        LeanTween.value(textMesh.gameObject, textMesh.color, Color.black, 0.5f).setOnUpdate((Color val) =>
        {
            textMesh.color = val;
        });
    }
}