using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private Image image;
    private Outline outline;
    public char Letter {  get; private set; }
    public TileState TileState { get; private set; }

    private void Awake()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();     
        image = GetComponent<Image>();
        outline = GetComponent<Outline>();
    }

    public void SetLetter(char ch)
    {
        textMesh.text = ch.ToString();
        Letter = ch;
    }

    public void SetState(TileState state)
    {
        TileState = state;
        image.color = state.TileColor;
        outline.effectColor = state.OutlineColor;
    }  
}
