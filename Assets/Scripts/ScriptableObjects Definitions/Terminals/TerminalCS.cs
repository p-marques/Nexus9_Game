using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Terminals/Terminal Control System", order = 4)]
public class TerminalCS : ScriptableObject
{
    [SerializeField] private string _controlSystemName;

    [ReadOnly]
    [SerializeField] private bool _isEngaged;

    public string Name => _controlSystemName;

    public bool IsEngaged
    {
        get => _isEngaged;
        set
        {
            _isEngaged = value;
        }
    }

    public void Reset()
    {
        _isEngaged = false;
    }
}
