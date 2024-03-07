using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

public class Row : MonoBehaviour
{
    public List<Tile> tiles { get; private set; }

    private Animator animator;

    private void Awake()
    {
    }

    public Tile this[int index]
    {
        get => tiles[index];
    }

    public IEnumerator<Tile> GetEnumerator()
    {
        return tiles.GetEnumerator();
    }

    public void SetTiles(IEnumerable<Tile> tiles)
    {
        this.tiles = tiles.ToList();
    }

    public void WrongWordAnimation()
    {
        //animator.Play("Word doesn't exist");
    }

    public string GetWord()
    {
        StringBuilder sb = new StringBuilder();
        foreach (Tile tile in tiles) sb.Append(tile.Letter);
        return sb.ToString();   
    }

}
