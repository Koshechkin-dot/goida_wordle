using UnityEngine;

public class VirtualKeyboard : MonoBehaviour
{
    private IGameInput gameInput;

    public void Inject(IGameInput gameInput)
    {
        this.gameInput = gameInput;
    }

    public void OnLetterButtonClick(string letter) => gameInput.AddLetter(letter.ToCharArray()[0]);
    public void OnSubmitButtonClick() => gameInput.SubmitWord();
    public void OnReturnButtonClick() => gameInput.RemoveLetter();
}
