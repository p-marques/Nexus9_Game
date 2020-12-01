using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private const float RANGE = 4f;

    [SerializeField]
    private string interactionText;

    public string InteractionText => interactionText;

    public float Range => RANGE;

    public void Interact()
    {
        Debug.Log($"{InteractionText}");
    }
}
