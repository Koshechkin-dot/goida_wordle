using UnityEngine;
using System.Collections.Generic;

public class WordBase
{
    public List<string> allWords { get; private set; }
    public List<string> mostlyUsing { get; private set; }

    public WordBase(List<string> allWords, List<string> mostlyUsing)
    {
        this.allWords = allWords;
        this.mostlyUsing = mostlyUsing;
    }

    public bool Validate(string word) => (word != null && allWords.Contains(word)) ? true : false;
    public string GetRandomWord() => mostlyUsing[Random.Range(0, allWords.Count)];
}
