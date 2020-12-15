using NaughtyAttributes;
using UnityEngine;

public class Toggle : MonoBehaviour, IInteractable
{
    private const float RANGE = 4f;
    private const string INTERACTION_TEXT = "Toggle";

    [SerializeField]
    [ReadOnly]
    private bool isOn;

    [Header("Requirements to interact")]
    [SerializeField]
    private bool playerNeedsItem;

    [SerializeField]
    [ShowIf("playerNeedsItem")]
    [Tooltip("Item that the player needs to have to successfully interact")]
    private Item necessaryItem;

    [SerializeField]
    private bool hasControlSystem;

    [SerializeField]
    [ShowIf("hasControlSystem")]
    [Tooltip("Control system that needs to be engaged before interaction")]
    private TerminalCS controlSystem;

    private new Renderer renderer;

    public float Range => RANGE;

    public string InteractionText => INTERACTION_TEXT;

    public bool IsOn => isOn;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();

        if (!renderer) Debug.LogError("Failed getting the renderer.");
    }

    public void Interact(Player player)
    {
        if (hasControlSystem && !CheckControlSystem())
        {
            Debug.Log("Can't interact. Control system not engaged.");
            return;
        }

        if (playerNeedsItem && !CheckRequiredItem(player.Inventory))
        {
            Debug.Log("Can't interact. Player doesn't have required item.");
            return;
        }

        isOn = true;

        UpdateColor();
    }

    private bool CheckControlSystem()
    {
        if (!controlSystem)
        {
            Debug.LogError($"Control system is required for interaction but none is referenced.");
            return true;
        }

        return controlSystem.IsEngaged;
    }

    private bool CheckRequiredItem(Storage inventory)
    {
        if (!necessaryItem)
        {
            Debug.LogError("Player needs item but no item ref is set.");
            return true;
        }

        return inventory.HasItem(necessaryItem);
    }

    private void UpdateColor()
    {
        if (isOn)
            renderer.material.SetColor("_BaseColor", Color.green);
        else
            renderer.material.SetColor("_BaseColor", Color.red);
    }
}
