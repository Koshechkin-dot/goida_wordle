public class ResultShowEvent : IEvent
{
    public string Score {  get; set; }
    public string Timer { get; set; }
    public string SecretWord { get; set; }
    public bool ShowNextButton { get; set; }

    public ResultShowEvent(string score, string timer, string secretWord, bool showNextButton)
    {
        SecretWord = secretWord;
        Score = score;
        Timer = timer;
        ShowNextButton = showNextButton;
    }
}
