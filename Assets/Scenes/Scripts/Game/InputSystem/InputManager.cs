using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{   
    private IGameInput gameInput;
    private readonly List<KeyCode> ALLOWED_KEYS = new List<KeyCode>()
    {
        KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U, KeyCode.I, KeyCode.O, KeyCode.P,
        KeyCode.LeftBracket, KeyCode.RightBracket, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.G, KeyCode.H,
        KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.Semicolon, KeyCode.Quote, KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V,
        KeyCode.B, KeyCode.N, KeyCode.M, KeyCode.Comma, KeyCode.Period, KeyCode.BackQuote
    };
    private readonly Dictionary<KeyCode, char> KEYS_ON_RUSSIAN = new Dictionary<KeyCode, char>()
    {
        { KeyCode.Q, 'Й' }, { KeyCode.W, 'Ц' }, { KeyCode.E, 'У' }, { KeyCode.R, 'К' }, { KeyCode.T, 'Е' },
        { KeyCode.Y, 'Н' }, { KeyCode.U, 'Г' }, { KeyCode.I, 'Ш' }, { KeyCode.O, 'Щ' }, { KeyCode.P, 'З' },
        { KeyCode.LeftBracket, 'Х' }, { KeyCode.RightBracket, 'Ъ' }, { KeyCode.A, 'Ф' }, { KeyCode.S, 'Ы' },
        { KeyCode.D, 'В' }, { KeyCode.F, 'А' }, { KeyCode.G, 'П' }, { KeyCode.H, 'Р' }, { KeyCode.J, 'О' },
        { KeyCode.K, 'Л' }, { KeyCode.L, 'Д' }, { KeyCode.Semicolon, 'Ж' }, { KeyCode.Quote, 'Э' },
        { KeyCode.Z, 'Я' }, { KeyCode.X, 'Ч' }, { KeyCode.C, 'С' }, { KeyCode.V, 'М' }, { KeyCode.B, 'И' },
        { KeyCode.N, 'Т' }, { KeyCode.M, 'Ь' }, { KeyCode.Comma, 'Б' }, { KeyCode.Period, 'Ю' }, { KeyCode.BackQuote, 'Ё' },
    };
    private bool IsEnabled = true;
    private EventBus bus;

    public void Inject(IGameInput gameInput)
    {
        this.gameInput = gameInput;
    }

    void Update()
    {
        if(IsEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                gameInput.RemoveLetter();
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                gameInput.SubmitWord();
            }
            else
            {
                foreach (KeyCode keyCode in ALLOWED_KEYS)
                {
                    if (Input.GetKeyDown(keyCode))
                    {
                        gameInput.AddLetter(KEYS_ON_RUSSIAN[keyCode]);
                    }
                }
            }
        }
    }


    private void Start()
    {
        bus = ServiceLocator.Instance.Get<EventBus>();
        bus.Subscribe<IMEvent>(EnableSwitcher);
    }
    private void OnDestroy()
    {
        bus.Unsubscribe<IMEvent>(EnableSwitcher);
    }

    private void EnableSwitcher(IMEvent @event)
    {
        IsEnabled = @event.IsEnabled;
    }
    
}
