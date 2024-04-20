using UnityEngine;
using System;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

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

    public void Inject(IWordSubmitter wordSubmitter) => this.wordSubmitter = wordSubmitter;

    public void Restart(NextWordEvent @event)
    {
        SecretWord = wordBase.GetRandomWord();
        gameManager.word = SecretWord; //удалить на релизе
        rowPointer = 0;
        tilePointer = 0;

        gridManager.ClearGrid();

        currentRow = gridManager.GetRow(rowPointer);
        eventBus.Invoke(new TimerStart());
    }

    public void StartGame(int rows, int columns)
    {
        if(rows <= 0 || columns <= 0)
        {
            Debug.LogError($"TRY TO GENERATE GRID WITH ROWS = {rows}, COLUMNS = {columns}");
            throw new ArgumentException("BAD GRID GENERATION");
        }
        gridManager.GenerateGrid(rows, columns);
        this.rows = rows;
        this.columns = columns;

        if (ServiceLocator.Instance.Get<GameConfigBuilder>().GetConfig().Daily)
        {
            DateTime currentDate = DateTime.Now;
            int seed = (currentDate.Year * 10000) + (currentDate.Month * 100) + currentDate.Day;
            SecretWord = wordBase.GetSeeded(seed);
        }
        else
        {
            SecretWord = wordBase.GetRandomWord();
        }
        gameManager.word = SecretWord; //удалить на релизе
        rowPointer = 0;
        tilePointer = 0;
            

        currentRow = gridManager.GetRow(rowPointer);
        eventBus.Invoke(new TimerStart());
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
                    GenerateResults(true);
                }
                else if (rowPointer + 1 >= rows) //если попытки кончились
                {
                    GenerateResults(false);
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
    private void GenerateResults(bool win)
    {
        if (win)
        {
            eventBus.Invoke(new IMEvent(false));
            eventBus.Invoke(new TimerStop());
            float seconds = timer.totalSeconds;
            int result = (int)(100 * (rows - rowPointer + 1) / seconds * columns);
            eventBus.Invoke(new ScoreChanged(result));
            eventBus.Invoke(new ResultShowEvent(result.ToString(),
                                                seconds.ToString() + " сек",
                                                "Вы победили!\nЗагаданное слово: " + SecretWord,
                                                ServiceLocator.Instance.Get<GameConfigBuilder>().GetConfig().Daily));
            if(ServiceLocator.Instance.Get<GameConfigBuilder>().GetConfig().Daily)
            {
                int prev = PlayerPrefs.GetInt("DailyStreak");
                PlayerPrefs.SetInt("DailyStreak", prev + 1);
                PlayerPrefs.Save();
            }
        }
        else
        {
            eventBus.Invoke(new IMEvent(false));
            eventBus.Invoke(new TimerStop());
            float seconds = timer.totalSeconds;
            int result = (int)(100 * (rows - rowPointer + 1) / seconds * columns) * -1;
            eventBus.Invoke(new ScoreChanged(result));
            eventBus.Invoke(new ResultShowEvent(result.ToString(),
                                                seconds.ToString() + " сек",
                                                "Вы проиграли!\nЗагаданное слово: " + SecretWord,
                                                ServiceLocator.Instance.Get<GameConfigBuilder>().GetConfig().Daily));
            if (ServiceLocator.Instance.Get<GameConfigBuilder>().GetConfig().Daily)
            {
                PlayerPrefs.SetInt("DailyStreak", 0);
                PlayerPrefs.Save();
            }
        }
    }

    public int GetTryCounter() => rowPointer;
}
