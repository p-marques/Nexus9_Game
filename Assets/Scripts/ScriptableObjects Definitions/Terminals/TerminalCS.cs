using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Terminals/Terminal Control System", order = 4)]
public class TerminalCS : ScriptableObject
{
    [SerializeField]
    private string controlSystemName;

    [SerializeField]
    [ReadOnly]
    private bool isEngaged;

    public string Name => controlSystemName;

    public bool IsEngaged
    {
        get => isEngaged;
        set
        {
            isEngaged = value;
        }
    }

    public void Reset()
    {
        isEngaged = false;
    }
}
