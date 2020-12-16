using UnityEngine;

public class PlayerHijackState : PlayerNormalState
{
    private readonly Player playerRef;

    public override bool CanPickUpItem => false;

    public PlayerHijackState(Player player, NexusEvent toggleInventoryUIEvent) 
        : base(player, toggleInventoryUIEvent)
    {
        playerRef = player;
    }

    public override void OnEnter()
    {
        CurrentControlledCharacter = playerRef.CurrentHijack
            .Transform.GetComponent<CharacterController>();

        if (!CurrentControlledCharacter)
            Debug.LogWarning("Player hijacked IHijackable doesn't have a CharacterController!");

        CurrentControlledCamera = playerRef.CurrentHijack
            .Transform.GetComponentInChildren<Camera>(true);

        CurrentControlledCamera.GetComponent<AudioListener>().enabled = true;

        CurrentControlledCamera.gameObject.SetActive(true);
    }

    public override void Tick()
    {
        base.Tick();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            playerRef.CurrentHijack = null;
        }
    }

    public override void OnExit()
    {
        // Only disable hijack camera if player is not stuck in interaction
        if (playerRef.CurrentInteraction == null)
        {
            CurrentControlledCamera.gameObject.SetActive(false);
        }
    }
}
