using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderChange : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI value;

    // Update is called once per frame
    void Update()
    {
        value.text = slider.value.ToString();
    }
}
