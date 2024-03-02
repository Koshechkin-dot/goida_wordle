public class ScoreChanged : IEvent
{
    public int Score { get; private set; }
    public ScoreChanged(int score)
    {
        Score = score;
    }
}
