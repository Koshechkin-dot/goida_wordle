using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInteractorDaily : IGameInput
{
    private int rows;
    private int columns;
    private int rowPointer;
    private int tilePointer;
    private Row currentRow;
    private string SecretWord;

    #region services
    private EventBus eventBus;
    private GameManager gameManager;
    private GridManager gridManager;
    private WordBase wordBase;
    private Timer timer;
    private IWordSubmitter wordSubmitter;
    #endregion

    //����� ������ �� ������ �������
    public GameInteractorDaily()
    {
        eventBus = ServiceLocator.Instance.Get<EventBus>();
        gameManager = ServiceLocator.Instance.Get<GameManager>();
        gridManager = ServiceLocator.Instance.Get<GridManager>();
        wordBase = ServiceLocator.Instance.Get<WordBaseLoader>().GetBase();
        timer = ServiceLocator.Instance.Get<Timer>();
    }

    //����������� ������ ����������
    public void Inject(IWordSubmitter wordSubmitter) => this.wordSubmitter = wordSubmitter;
    public void StartGame(int rows, int columns, int seed)
    {
        if (rows <= 0 || columns <= 0)
        {
            Debug.LogError($"TRY TO GENERATE GRID WITH ROWS = {rows}, COLUMNS = {columns}");
            throw new ArgumentException("BAD GRID GENERATION");
        }

        this.rows = rows;
        this.columns = columns;
        SecretWord = wordBase.GetSeeded(seed);
        gameManager.word = SecretWord; //������� �� ������
        rowPointer = 0;
        tilePointer = 0;

        if (gridManager.Generated)
            gridManager.ClearGrid();
        else
            gridManager.GenerateGrid(rows, columns);

        currentRow = gridManager.GetRow(rowPointer);
        timer.StartTimer();
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
    public void SubmitWord()
    {
        if (tilePointer == columns)
        {
            string userWord = currentRow.GetWord();
            if (wordBase.Validate(userWord))
            {
                if (wordSubmitter.SubmitWord(currentRow, SecretWord)) //���� �����
                {
                    eventBus.Invoke(new IMEvent(false));
                    float seconds = timer.StopTimer();
                    int result = (int)(100 * (rows - rowPointer + 1) / seconds * columns);
                    eventBus.Invoke(new ScoreChanged(result));
                    eventBus.Invoke(new ResultShowDailyEvent(result.ToString(),
                                                        seconds.ToString() + " ���",
                                                        "�� ��������!\n���������� �����: " + SecretWord));
                }
                else if (rowPointer + 1 >= rows) //���� ������� ���������
                {
                    eventBus.Invoke(new IMEvent(false));
                    float seconds = timer.StopTimer();
                    int result = (int)(100 * (rows - rowPointer + 1) / seconds * columns) * -1;
                    eventBus.Invoke(new ScoreChanged(result));
                    eventBus.Invoke(new ResultShowDailyEvent(result.ToString(),
                                                        seconds.ToString() + " ���",
                                                        "�� ���������!\n���������� �����: " + SecretWord));
                }
                else GoNextTry(); //��������� �������
            }
            else currentRow.WrongWordAnimation();
        }
    }

    private void GoNextTry()
    {
        rowPointer++;
        currentRow = gridManager.GetRow(rowPointer);
        tilePointer = 0;
    }
}
