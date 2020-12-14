using UnityEngine;

public interface IHijackable : IInteractable
{
    Transform Transform { get; }
}
