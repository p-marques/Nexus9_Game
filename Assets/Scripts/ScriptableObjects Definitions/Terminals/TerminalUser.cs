using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Terminals/Terminal User", order = 2)]
public class TerminalUser : ScriptableObject
{
    [SerializeField]
    private string userName;

    [SerializeField]
    private string password;

    [SerializeField]
    private TerminalCommunication[] communications;

    public string Id => userName.Replace(" ", "").ToLower();
    public TerminalCommunication[] Communications => communications;
    public bool IsUnlocked { get; private set; }

    private void Awake()
    {
        IsUnlocked = false;
    }

    public bool Unlock(string inPassword)
    {
        if (IsUnlocked)
            Debug.LogWarning($"{userName} was already unlocked and yet Unlock() was called.");
        else
            IsUnlocked = password == inPassword;

        return IsUnlocked;
    }

    public override string ToString()
    {
        string value = $"{userName} ({Id}) ";

        if (IsUnlocked)
        {
            value += "[Unlocked]";
        }
        else
        {
            value += "[locked]";
        }

        return value;
    }
}
