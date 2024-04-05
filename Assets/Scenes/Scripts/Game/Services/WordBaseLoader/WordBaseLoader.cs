using System.Linq;
using UnityEngine;

public class WordBaseLoader : IService
{
    private WordBase wordBase;
    public void LoadBase(int numberOfLetters)
    {
        TextAsset all = (TextAsset)Resources.Load($"Words/All_{numberOfLetters}");
        TextAsset mostlyUsing = (TextAsset)Resources.Load($"Words/Common_{numberOfLetters}");

        if(all == null || mostlyUsing == null) 
        {
            Debug.LogError($"Try to load not exist base from {this}");
            return;
        }
        wordBase = new WordBase(
            allWords: all.text.Split("\r\n").ToList(),
            mostlyUsing: mostlyUsing.text.Split("\r\n").ToList()
            );
    }
    public WordBase GetBase()
    {
        if(wordBase == null)
        {
            Debug.LogError($"Try to get not loaded base from {this}");
        }
        return wordBase;
    }
}
