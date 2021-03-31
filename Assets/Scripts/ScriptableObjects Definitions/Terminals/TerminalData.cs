using UnityEngine;

[CreateAssetMenu(menuName = "Game/Terminals/New Terminal", order = 1)]
public class TerminalData : ScriptableObject
{
    [SerializeField] private TerminalUser[] _users;

    [SerializeField] private TerminalCS[] _controlSystems;

    public TerminalUser[] Users => _users;
    public TerminalCS[] ControlSystems => _controlSystems;
}
