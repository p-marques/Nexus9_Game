using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Terminals/New Terminal", order = 1)]
public class TerminalData : ScriptableObject
{
    [SerializeField]
    private TerminalUser[] users;

    public TerminalUser[] Users => users;
}
