using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Terminals/Terminal Communication", order = 3)]
public class TerminalCommunication : ScriptableObject
{
    [SerializeField]
    private string from;

    [SerializeField]
    [ResizableTextArea]
    private string text;

    public string From => from;

    public string Text => text;
}
