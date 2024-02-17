using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GridManager GridManager;
    [SerializeField] private int rows = 6;
    [SerializeField] private int columns = 6;
    [SerializeField] private string secretWord;
    [Header("States")]
    public TileState valid;
    public TileState exist;
    public TileState notExist;

    private List<string> words;

    private int tilePointer;
    private int rowPointer;
    private Row currentRow;
   
    
    void Start()
    {
        TextAsset textAsset = Resources.Load("word_storage") as TextAsset;
        words = textAsset.text.Split("\r\n").ToList();
        secretWord = words[Random.Range(0, words.Count)];
        GridManager.GenerateGrid(rows,columns);
        tilePointer = 0;
        rowPointer = 0;
        currentRow = GridManager.GetRow(rowPointer);
        
    }

    public void AddLetter(char letter)
    {
        if(tilePointer < columns)
        {
            currentRow[tilePointer].SetLetter(letter);
            tilePointer++;
        }
    }

    public void RemoveLetter()
    {
        tilePointer = Mathf.Max(tilePointer - 1, 0);
        currentRow[tilePointer].SetLetter('\0');
    }

    public void Restart()
    {
        rowPointer = 0;
        tilePointer = 0;
        currentRow = GridManager.GetRow(rowPointer);
        secretWord = words[Random.Range(0, words.Count)];
        GridManager.ClearGrid(notExist);
    }

    public void SubmitWord()
    {
        if(tilePointer == columns)
        {
            string userWord = currentRow.GetWord();
            if(secretWord == userWord)
            {
                foreach(Tile tile in currentRow)
                {
                    tile.SetState(valid);
                }
                //won
            }
            else
            {
                for (int i = 0; i < secretWord.Length; i++)
                {
                    if (userWord[i] == secretWord[i])
                        currentRow[i].SetState(valid);
                    else if (secretWord.Contains(userWord[i]))
                        currentRow[i].SetState(exist);
                       
                }
            }

            rowPointer++;
            if(rowPointer == rows)
            {
                //lose
                return;
            }
            else
            {
                currentRow = GridManager.GetRow(rowPointer);
                tilePointer = 0;
            }

        }
    }
}
