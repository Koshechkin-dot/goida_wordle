using UnityEngine;

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
    private IWordSubmitter wordSubmitter;
    #endregion

    //����� ������ �� ������ �������
    public GameInteractor()
    {
        eventBus = ServiceLocator.Instance.Get<EventBus>(); 
        gameManager = ServiceLocator.Instance.Get<GameManager>();
        gridManager = ServiceLocator.Instance.Get<GridManager>();
        wordBase = ServiceLocator.Instance.Get<WordBaseLoader>().GetBase();
    }

    //����������� ������ ����������
    public void Inject(IWordSubmitter wordSubmitter) => this.wordSubmitter = wordSubmitter;

    public void StartGame(int rows, int columns)
    {
        SecretWord = "������"; //�������� �� ������
        gameManager.word = SecretWord;
        rowPointer = 0;
        tilePointer = 0;
        this.rows = rows;
        this.columns = columns;
        gridManager.GenerateGrid(rows, columns);
        currentRow = gridManager.GetRow(rowPointer);
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
        SecretWord = wordBase.GetRandomWord();
        gridManager.ClearGrid();
        gameManager.word = SecretWord; //������� �� ������
    }  
    public void SubmitWord()
    {
        if (tilePointer == columns)
        {
            string userWord = currentRow.GetWord();
            if(wordBase.Validate(userWord))
            {
                if (wordSubmitter.SubmitWord(currentRow, SecretWord)) //���� �����
                {
                    eventBus.Invoke(new ScoreChanged(100));
                    Restart();
                }
                else if (rowPointer + 1 >= rows) //���� ������� ���������
                {
                    eventBus.Invoke(new ScoreClear());
                    Restart();
                }
                else GoNextTry(); //��������� �������
            }
            else
            {
                currentRow.WrongWordAnimation();
            }
        }
    }

    private void GoNextTry()
    {
        rowPointer++;
        currentRow = gridManager.GetRow(rowPointer);
        tilePointer = 0;
    }
}
