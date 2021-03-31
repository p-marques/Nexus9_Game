using UnityEngine;

public class PlayerInteractionState : IPlayerState
{
    private Player _playerRef;

    public CharacterController CurrentControlledCharacter => null;
    public Camera CurrentControlledCamera => null;
    public bool CanPickUpItem => false;

    public PlayerInteractionState(Player player)
    {
        _playerRef = player;
    }

    public void OnEnter()
    {

    }

    public void PhysicsTick()
    {
        
    }

    public void Tick()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _playerRef.CurrentInteraction = null;
        }
    }

    public void OnExit()
    {
        
    }
}
