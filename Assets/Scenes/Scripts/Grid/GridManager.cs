using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject gridHolder;
    private GridLayoutGroup layoutGroup;
    private Tile[,] tiles;

    private void Awake()
    {
        layoutGroup = gridHolder.GetComponent<GridLayoutGroup>();
    }

    public void GenerateGrid(int rows, int cols)
    {
        tiles = new Tile[rows, cols];
        layoutGroup.constraintCount = cols;
        GameObject reference = (GameObject)Instantiate(Resources.Load("Tile"));       
        for(int row = 0; row < rows; row++)        
            for(int col = 0; col < cols; col++)
            {
                var tile = Instantiate(reference);
                tiles[row,col] = tile.GetComponent<Tile>();
                tile.transform.SetParent(gridHolder.transform);
            }              
        Destroy(reference);
    }
    public void ClearGrid()
    {
        for (int row = 0; row < tiles.GetLength(0); row++)
            for (int col = 0; col < tiles.GetLength(1); col++)
            {
                tiles[row, col].ChangeColor(TileColors.None);
                tiles[row, col].ChangeText(string.Empty);
            }
    }
    public Tile GetTile(int row, int col) => tiles[row, col];
    public Tile[,] GetTiles() => tiles;

    public Tile[] GetTilesInRow(int row)
    {
        Tile[] til = new Tile[tiles.GetLength(1)];
        for(int col = 0; col < tiles.GetLength(1); col++)
        {
            til[col] = tiles[row, col];
        }
        return til;
    }

    public string GetWordInRow(int row)
    {
        StringBuilder stringBuilder = new StringBuilder();
        for(int col = 0; col < tiles.GetLength(1); col++)
        {
            stringBuilder.Append(tiles[row, col].GetLetter());
        }
        return stringBuilder.ToString();
    }
}
