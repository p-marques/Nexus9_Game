using UnityEngine;

public class PlayerNormalState : IPlayerState
{
    private const float FORCED_GRAVITY = 20f;

    private readonly Player playerRef;
    private readonly NexusEvent onToggleInventoryUIShow;
    private float inputForward;
    private float inputStrafe;

    private Vector3 GroundDetectionPoint =>
        CurrentControlledCharacter.transform.position;

    public Camera CurrentControlledCamera { get; protected set; }
    public CharacterController CurrentControlledCharacter { get; protected set; }
    public virtual bool CanPickUpItem => true;

    private bool IsGrounded
    {
        get
        {
            Collider[] collisions = Physics.OverlapSphere(GroundDetectionPoint, playerRef.GroundDetectionRadius, playerRef.GroundDetectionLayer);

            if (collisions.Length > 0)
                return true;
            else
                return false;
        }
    }

    public PlayerNormalState(Player player, NexusEvent toggleInventoryUIEvent)
    {
        playerRef = player;

        onToggleInventoryUIShow = toggleInventoryUIEvent;
    }

    public virtual void OnEnter() 
    {
        CurrentControlledCamera = playerRef.GetComponentInChildren<Camera>();
        CurrentControlledCharacter = playerRef.GetComponent<CharacterController>();
        CurrentControlledCamera.GetComponent<AudioListener>().enabled = true;
    }

    public virtual void PhysicsTick()
    {
        if (!CurrentControlledCharacter) return;

        Transform transform = CurrentControlledCharacter.transform;

        Vector3 moveDirection = transform.right * inputStrafe + transform.forward * inputForward;

        moveDirection *= playerRef.MoveSpeed * Time.fixedDeltaTime;

        if (!IsGrounded)
        {
            moveDirection.y -= FORCED_GRAVITY;
        }

        //CurrentControlledCharacter.Move(moveDirection);
    }

    public virtual void Tick()
    {
        inputForward = Input.GetAxis("Forward");
        inputStrafe = Input.GetAxis("Strafe");

        playerRef.Animator.SetFloat("MovementForward", inputForward);
        playerRef.Animator.SetFloat("MovementStrafe", inputStrafe);

        if (Input.GetKeyDown(KeyCode.G))
        {
            playerRef.Animator.SetTrigger("HijackToggle");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!onToggleInventoryUIShow) return;

            onToggleInventoryUIShow.Raise();
        }
    }

    public virtual void OnExit() 
    {
        
    }
}
