using UnityEngine;
using NaughtyAttributes;

public class Terminal : InteractableWithRequirements, ITerminal
{
    private const float RANGE = 3f;
    private const string INTERACTION_TEXT = "Terminal";

    [SerializeField]
    private TerminalData terminalData;

    [Header("Events")]
    [Tooltip("Event raised when interactions begins")]
    [SerializeField]
    private NexusEvent<ITerminal> onInteractionStartEvent;

    public float Range => RANGE;

    public string InteractionText => INTERACTION_TEXT;

    public TerminalData TerminalData => terminalData;

    private void Awake()
    {
        if (!terminalData)
        {
            Debug.LogError("Terminal doens't have terminal data.");

            return;
        }

        for (int i = 0; i < terminalData.ControlSystems.Length; i++)
        {
            terminalData.ControlSystems[i].Reset();
        }

        for (int i = 0; i < terminalData.Users.Length; i++)
        {
            terminalData.Users[i].Reset();
        }
    }

    public void Interact(Player player)
    {
        if (hasControlSystem)
        {
            if (controlSystem && !controlSystem.IsEngaged)
            {
                onActionBlocked.Raise();

                return;
            }
        }

        player.CurrentInteraction = this;

        onInteractionStartEvent.Raise(this);
    }
}
