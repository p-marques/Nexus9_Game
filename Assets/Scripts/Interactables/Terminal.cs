using UnityEngine;

public class Terminal : MonoBehaviour, IInteractable
{
    private const float RANGE = 3f;
    private const string INTERACTION_TEXT = "Terminal";

    [SerializeField]
    private TerminalData terminalData;

    [SerializeField]
    private TerminalVariable terminalVariable;

    public float Range => RANGE;

    public string InteractionText => INTERACTION_TEXT;

    public void Interact(Player player)
    {
        player.CurrentInteraction = this;

        terminalVariable.Value = terminalData;
    }
}
