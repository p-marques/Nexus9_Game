using UnityEngine;

public interface IPlayerState : IState
{
    CharacterController CurrentControlledCharacter { get; set; }
    Camera CurrentControlledCamera { get; set; }
}
