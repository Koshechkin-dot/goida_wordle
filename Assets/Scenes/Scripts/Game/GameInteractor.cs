using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameInteractor : IGameInput
{
    private int rows;
    private int columns;
    private int rowPointer;
    private int tilePointer;
    private Row currentRow;

    private List<string> words;
    private string SecretWord;

    private GameManager gameManager;
    private GridManager gridManager;
    private IWordSubmitter wordSubmitter;
    
    public GameInteractor(GridManager grid, GameManager manager, string wordsBase)
    {
        gridManager = grid;
        gameManager = manager;
        TextAsset textAsset = (TextAsset)Resources.Load(wordsBase);
        words = textAsset.text.Split("\r\n").ToList();
    }

    public void Inject(IWordSubmitter wordSubmitter) => this.wordSubmitter = wordSubmitter;

    public void StartGame(int rows, int columns)
    {
        GenerateWord();
        rowPointer = 0;
        tilePointer = 0;
        this.rows = rows;
        this.columns = columns;
        gridManager.GenerateGrid(rows, columns);
        currentRow = gridManager.GetRow(rowPointer);
    }
    protected void GenerateWord()
    {
        SecretWord = "–¿¡Œ“¿";
    }
    public void AddLetter(char letter)
    {
        if (tilePointer < columns)
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
        currentRow = gridManager.GetRow(rowPointer);
        SecretWord = words[Random.Range(0, words.Count)];
        gridManager.ClearGrid();
        gameManager.word = SecretWord;
    }
    private void GoNextTry()
    {
        rowPointer++;
        currentRow = gridManager.GetRow(rowPointer);
        tilePointer = 0;
    }
    public void SubmitWord()
    {
        if (tilePointer == columns)
        {
            if (wordSubmitter.SubmitWord(currentRow, SecretWord))
            {
                //win
            }
            else if (rowPointer + 1 >= rows)
            {
                //lose
                return;
            }
            else GoNextTry();
        }
    }
}
