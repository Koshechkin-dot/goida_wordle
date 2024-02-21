using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private float time = 1.0f;
    [SerializeField] private string scene;

    void Start()
    {
        StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
        Color originalColor = image.color;
        Color targetColor = originalColor;
        targetColor.a = 0;

        float currentTime = 0;

        while (currentTime < time)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / time;
            image.color = Color.Lerp(originalColor, targetColor, t);
            yield return null;
        }
        SceneManager.LoadScene(scene);
    }
}
