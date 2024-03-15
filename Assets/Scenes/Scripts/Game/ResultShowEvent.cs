public class ResultShowEvent : IEvent
{
    public string Score {  get; set; }
    public string Timer { get; set; }
    public string SecretWord { get; set; }

    public ResultShowEvent(string score, string timer, string secretWord)
    {
        SecretWord = secretWord;
        Score = score;
        Timer = timer;
    }
}
