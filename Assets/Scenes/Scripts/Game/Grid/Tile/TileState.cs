using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "TileState", menuName = "GameDesign/TileState")]
public class TileState : ScriptableObject
{
    public Color TileColor;
    public Color OutlineColor;
}
