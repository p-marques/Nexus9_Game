using UnityEngine;

public class PlayerHijackState : PlayerNormalState
{
    private readonly Player _playerRef;

    public override bool CanPickUpItem => false;

    public PlayerHijackState(Player player, NexusEvent toggleInventoryUIEvent) 
        : base(player, toggleInventoryUIEvent)
    {
        _playerRef = player;
    }

    public override void OnEnter()
    {
        CurrentControlledCharacter = _playerRef.CurrentHijack
            .Transform.GetComponent<CharacterController>();

        if (!CurrentControlledCharacter)
            Debug.LogWarning("Player hijacked IHijackable doesn't have a CharacterController!");

        CurrentControlledCamera = _playerRef.CurrentHijack
            .Transform.GetComponentInChildren<Camera>(true);

        CurrentControlledCamera.GetComponent<AudioListener>().enabled = true;

        CurrentControlledCamera.gameObject.SetActive(true);
    }

    public override void Tick()
    {
        base.Tick();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _playerRef.CurrentHijack = null;
        }
    }

    public override void OnExit()
    {
        // Only disable hijack camera if player is not stuck in interaction
        if (_playerRef.CurrentInteraction == null)
        {
            CurrentControlledCamera.gameObject.SetActive(false);
        }
    }
}
