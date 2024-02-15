using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GridManager GridManager;
    [SerializeField] private int rows = 6;
    [SerializeField] private int columns = 6;
    private int currTry = 0;
    private int currLetter = 0;
    [SerializeField] private string secretWord;
    private Tile[] thisTryTiles;
    
    
    void Start()
    {
        GridManager.GenerateGrid(rows,columns);
        thisTryTiles = GridManager.GetTilesInRow(currTry);
    }

    public void AddLetter(char letter)
    {
        if(currLetter < 5)
        {
            thisTryTiles[currLetter].ChangeText(letter.ToString());
            currLetter++;
        }
        else if(currLetter == 5) 
        {
            thisTryTiles[currLetter].ChangeText(letter.ToString());
        }
    }
    public void RemoveLetter()
    {
        if(currLetter > 0)
        {
            thisTryTiles[currLetter].ChangeText(string.Empty);       
            currLetter--;
        }
        else if(currLetter == 0)
        {
            thisTryTiles[currLetter].ChangeText(string.Empty);
        }
    }
    public void ApplyWord()
    {
        if(currLetter == 5)
        {
            string userWord = GridManager.GetWordInRow(currTry);
            if(userWord == secretWord)
            {
                foreach(Tile tile in thisTryTiles)
                {
                    tile.ChangeColor(TileColors.Valid);
                    //красава
                }
            }
            else
            {
                for(int i = 0; i < secretWord.Length; i++) 
                {
                    if (userWord[i] == secretWord[i])
                    {
                        thisTryTiles[i].ChangeColor(TileColors.Valid);
                    }
                    else if (secretWord.Contains(userWord[i]))
                    {
                        thisTryTiles[i].ChangeColor(TileColors.Exist);
                    }
                }

                currTry++;
                currLetter = 0;

                if (currTry == rows)
                {
                    //лох
                }
                else
                {
                    thisTryTiles = GridManager.GetTilesInRow(currTry);
                }
            }
        }
    }
    public void Restart()
    {
        GridManager.ClearGrid();
        currTry = 0;
        currLetter = 0;
    }
}
