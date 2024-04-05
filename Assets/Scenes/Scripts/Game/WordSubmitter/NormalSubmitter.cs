using System.Collections.Generic;
using UnityEngine;

public class NormalSubmitter : IWordSubmitter
{
    private TileState valid;
    private TileState exist;
    private TileState inWrongPlace;
    public NormalSubmitter()
    {
        valid = Resources.Load("TileStates/Valid") as TileState;
        exist = Resources.Load("TileStates/Exist") as TileState;
        inWrongPlace = Resources.Load("TileStates/InvalidPosition") as TileState;
    }

    public bool SubmitWord(Row current, string SecretWord)
    {
        var charsInWord = new Dictionary<char, int>();
        var validRecogniteLetters = new Dictionary<char, int>();
        foreach (char c in SecretWord)
        {
            if (charsInWord.ContainsKey(c)) charsInWord[c]++;
            else charsInWord.Add(c, 1);

            if (!validRecogniteLetters.ContainsKey(c))
                validRecogniteLetters.Add(c, 0);
        }
        string userWord = current.GetWord();
        if (SecretWord == userWord)
        {
            foreach (Tile tile in current)
            {
                tile.SetState(valid);
            }
            return true;
        }
        else
        {
            for (int i = 0; i < SecretWord.Length; i++)
            {
                if (userWord[i] == SecretWord[i])
                {
                    current[i].SetState(valid);
                    validRecogniteLetters[userWord[i]]++;
                }
                else if (SecretWord.Contains(userWord[i]))
                {
                    current[i].SetState(exist);
                }
            }
            for (int i = 0; i < SecretWord.Length; i++)
            {
                if (userWord[i] != SecretWord[i]
                    && SecretWord.Contains(userWord[i])
                    && validRecogniteLetters[userWord[i]] >= charsInWord[userWord[i]])
                {
                    current[i].SetState(inWrongPlace);
                }
            }
            return false;
        }    
    }
}
