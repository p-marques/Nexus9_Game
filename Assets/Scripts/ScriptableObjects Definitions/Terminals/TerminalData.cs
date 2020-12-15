using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Terminals/New Terminal", order = 1)]
public class TerminalData : ScriptableObject
{
    [SerializeField]
    private TerminalUser[] users;

    [SerializeField]
    private TerminalCS[] controlSystems;

    public TerminalUser[] Users => users;
    public TerminalCS[] ControlSystems => controlSystems;
}
