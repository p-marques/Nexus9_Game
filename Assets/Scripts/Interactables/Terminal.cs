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
        player.CurrentInteraction = this;

        terminalVariable.Value = terminalData;
    }
}
