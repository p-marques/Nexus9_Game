using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Terminals/Terminal Communication", order = 3)]
public class TerminalCommunication : ScriptableObject
{
    [SerializeField] private string _from;

    [ResizableTextArea]
    [SerializeField] private string _text;

    public string From => _from;

    public string Text => _text;
}
