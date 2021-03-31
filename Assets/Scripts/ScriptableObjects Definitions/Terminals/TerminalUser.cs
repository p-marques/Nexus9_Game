using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Terminals/Terminal User", order = 2)]
public class TerminalUser : ScriptableObject
{
    [ReadOnly]
    [SerializeField] private bool _isUnlocked = false;

    [SerializeField] private string _userName;

    [SerializeField] private string _password;

    [SerializeField] private TerminalCommunication[] _communications;

    public string Id => _userName.Replace(" ", "").ToLower();
    public TerminalCommunication[] Communications => _communications;

    public bool IsUnlocked => _isUnlocked;

    private void Awake()
    {
        _isUnlocked = false;
    }

    public bool Unlock(string inPassword)
    {
        if (_isUnlocked)
            Debug.LogWarning($"{_userName} was already unlocked and yet Unlock() was called.");
        else
            _isUnlocked = _password == inPassword;

        return _isUnlocked;
    }

    public override string ToString()
    {
        string value = $"{_userName} ({Id}) ";

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
        _isUnlocked = false;
    }
}
