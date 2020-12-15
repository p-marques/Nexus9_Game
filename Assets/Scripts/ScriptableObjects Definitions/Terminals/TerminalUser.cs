using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Terminals/Terminal User", order = 2)]
public class TerminalUser : ScriptableObject
{
    [SerializeField]
    [ReadOnly]
    private bool isUnlocked = false;

    [SerializeField]
    private string userName;

    [SerializeField]
    private string password;

    [SerializeField]
    private TerminalCommunication[] communications;

    public string Id => userName.Replace(" ", "").ToLower();
    public TerminalCommunication[] Communications => communications;

    public bool IsUnlocked => isUnlocked;

    private void Awake()
    {
        isUnlocked = false;
    }

    public bool Unlock(string inPassword)
    {
        if (isUnlocked)
            Debug.LogWarning($"{userName} was already unlocked and yet Unlock() was called.");
        else
            isUnlocked = password == inPassword;

        return isUnlocked;
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

    public void Reset()
    {
        isUnlocked = false;
    }
}
