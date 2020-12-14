using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionState : IPlayerState
{
    private Player playerRef;

    public CharacterController CurrentControlledCharacter => null;
    public Camera CurrentControlledCamera => null;

    public PlayerInteractionState(Player player)
    {
        playerRef = player;
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
            playerRef.CurrentInteraction = null;
        }
    }

    public void OnExit()
    {
        
    }
}
