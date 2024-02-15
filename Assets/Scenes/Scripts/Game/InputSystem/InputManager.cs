using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{   
    private GameManager gameManager;
    private List<KeyCode> validKeys = new List<KeyCode>()
    {
        KeyCode.Q,
        KeyCode.W,
        KeyCode.E,
        KeyCode.R,
        KeyCode.T,
        KeyCode.Y,
        KeyCode.U,
        KeyCode.I,
        KeyCode.O,
        KeyCode.P,
        KeyCode.LeftBracket,
        KeyCode.RightBracket,
        KeyCode.A,
        KeyCode.S,
        KeyCode.D,
        KeyCode.F,
        KeyCode.G,
        KeyCode.H,
        KeyCode.J,
        KeyCode.K,
        KeyCode.L,
        KeyCode.Semicolon,
        KeyCode.Quote,
        KeyCode.Z,
        KeyCode.X,
        KeyCode.C,
        KeyCode.V,
        KeyCode.B,
        KeyCode.N,
        KeyCode.M,
        KeyCode.Comma,
        KeyCode.Period,
        KeyCode.BackQuote
    };
    private Dictionary<KeyCode, char> rusLetters = new Dictionary<KeyCode, char>()
    {
        { KeyCode.Q, '�' },
        { KeyCode.W, '�' },
        { KeyCode.E, '�' },
        { KeyCode.R, '�' },
        { KeyCode.T, '�' },
        { KeyCode.Y, '�' },
        { KeyCode.U, '�' },
        { KeyCode.I, '�' },
        { KeyCode.O, '�' },
        { KeyCode.P, '�' },
        { KeyCode.LeftBracket, '�' },
        { KeyCode.RightBracket, '�' },
        { KeyCode.A, '�' },
        { KeyCode.S, '�' },
        { KeyCode.D, '�' },
        { KeyCode.F, '�' },
        { KeyCode.G, '�' },
        { KeyCode.H, '�' },
        { KeyCode.J, '�' },
        { KeyCode.K, '�' },
        { KeyCode.L, '�' },
        { KeyCode.Semicolon, '�' },
        { KeyCode.Quote, '�' },
        { KeyCode.Z, '�' },
        { KeyCode.X, '�' },
        { KeyCode.C, '�' },
        { KeyCode.V, '�' },
        { KeyCode.B, '�' },
        { KeyCode.N, '�' },
        { KeyCode.M, '�' },
        { KeyCode.Comma, '�' },
        { KeyCode.Period, '�' },
        { KeyCode.BackQuote, '�' },
    };

    void Start()
    {
        gameManager = GetComponentInParent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                gameManager.RemoveLetter();
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                gameManager.ApplyWord();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameManager.Restart();
            }
            else
            {
                foreach(KeyCode keyCode in validKeys)
                {
                    if (Input.GetKeyDown(keyCode))
                    {
                        gameManager.AddLetter(rusLetters[keyCode]);
                    }
                }
            }

        }
    }
}
