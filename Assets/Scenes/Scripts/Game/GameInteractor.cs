using UnityEngine;
using System;

public class GameInteractor : IGameInput
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

    //берем ссылки на нужные сервисы
    public GameInteractor()
    {
        eventBus = ServiceLocator.Instance.Get<EventBus>();
        gameManager = ServiceLocator.Instance.Get<GameManager>();
        gridManager = ServiceLocator.Instance.Get<GridManager>();
        wordBase = ServiceLocator.Instance.Get<WordBaseLoader>().GetBase();
        timer = ServiceLocator.Instance.Get<Timer>();
        eventBus.Subscribe<NextWordEvent>(Restart);
    }
    public void OnDestroy()
    {
        eventBus.Unsubscribe<NextWordEvent>(Restart);
    }

    //прокидываем логику сабмиттера
    public void Inject(IWordSubmitter wordSubmitter) => this.wordSubmitter = wordSubmitter;

    public void Restart(NextWordEvent @event)
    {
        SecretWord = wordBase.GetRandomWord();
        gameManager.word = SecretWord; //удалить на релизе
        rowPointer = 0;
        tilePointer = 0;

        if (gridManager.Generated)
            gridManager.ClearGrid();
        else
            gridManager.GenerateGrid(rows, columns);

        currentRow = gridManager.GetRow(rowPointer);
        timer.StartTimer();
    }

    public void StartGame(int rows, int columns)
    {
        if(rows <= 0 || columns <= 0)
        {
            Debug.LogError($"TRY TO GENERATE GRID WITH ROWS = {rows}, COLUMNS = {columns}");
            throw new ArgumentException("BAD GRID GENERATION");
        }

        this.rows = rows;
        this.columns = columns;
        SecretWord = wordBase.GetRandomWord();
        gameManager.word = SecretWord; //удалить на релизе
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
                if (wordSubmitter.SubmitWord(currentRow, SecretWord)) //если верно
                {
                    float seconds = timer.StopTimer();
                    int result = (int)(100 * (rows - rowPointer + 1) / seconds * columns);
                    eventBus.Invoke(new ScoreChanged(result));
                    eventBus.Invoke(new ResultShowEvent(result.ToString(), 
                                                        seconds.ToString() + " сек", 
                                                        "Вы победили!\nЗагаданное слово: " + SecretWord));
                }
                else if (rowPointer + 1 >= rows) //если попытки кончились
                {
                    float seconds = timer.StopTimer();
                    eventBus.Invoke(new ScoreClear());
                    eventBus.Invoke(new ResultShowEvent("0", 
                                                        seconds.ToString() + " сек", 
                                                        "Вы проиграли!\nЗагаданное слово: " + SecretWord));
                }
                else GoNextTry(); //следующая попытка
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
