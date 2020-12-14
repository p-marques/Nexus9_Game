using UnityEngine;

public class PlayerHijackState : PlayerNormalState
{
    private readonly Player playerRef;

    public PlayerHijackState(Player player) : base(player)
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
        base.OnExit();

        CurrentControlledCamera.gameObject.SetActive(false);
    }
}
