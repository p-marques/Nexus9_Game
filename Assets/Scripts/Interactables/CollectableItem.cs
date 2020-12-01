using UnityEngine;

public class CollectableItem : MonoBehaviour, IInteractable
{
    private const float RANGE = 2f;

    [SerializeField]
    private Item item;

    [SerializeField]
    private string interactionText;

    public string InteractionText => interactionText;

    public float Range => RANGE;

    private void Awake()
    {
        if (!item)
        {
            Debug.Log("CollectableItem's item to collect is EMPTY!");
        }
    }

    public void Interact()
    {
        Debug.Log($"Interact");
    }
}
