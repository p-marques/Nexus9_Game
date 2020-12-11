public interface IInteractableItem : IInteractable
{
    Item Item { get; }

    bool HasBeenAnalysed { get; set; }
}
