using UnityEngine;

public interface IPlayerState : IState
{
    CharacterController CurrentControlledCharacter { get; }
    Camera CurrentControlledCamera { get; }

    bool CanPickUpItem { get; }
}
