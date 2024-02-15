using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private SpriteRenderer sprite;
    public void ChangeColor(TileColors colors)
    {
        switch (colors)
        {
            case TileColors.None:
                sprite.color = new Color(1, 0, 0, 0.5f);
                break;
            case TileColors.Valid:
                sprite.color = new Color(0, 1, 0, 0.5f);
                break;
            case TileColors.Exist:
                sprite.color = new Color(0.5f, 0.5f, 0, 0.5f);
                break;
        }
    }

    public void ChangeText(string ch) => text.text = ch;

    public string GetLetter() => text.text;
    
}
