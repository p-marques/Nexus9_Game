public interface IInteractable
{
    float Range { get; }

    string InteractionText { get; }

    void Interact();
}
