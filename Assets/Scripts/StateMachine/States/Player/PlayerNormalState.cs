using UnityEngine;

public class PlayerNormalState : IPlayerState
{
    private const float FORCED_GRAVITY = 20f;

    private readonly Player playerRef;
    private float inputForward;
    private float inputStrafe;

    private Vector3 GroundDetectionPoint =>
        CurrentControlledCharacter.transform.position - new Vector3(0, 1f, 0);

    public Camera CurrentControlledCamera { get; set; }
    public CharacterController CurrentControlledCharacter { get; set; }

    private bool IsGrounded
    {
        get
        {
            Collider[] collisions = Physics.OverlapSphere(GroundDetectionPoint, playerRef.GroundDetectionRadius);

            if (collisions.Length > 0)
                return true;
            else
                return false;
        }
    }

    public PlayerNormalState(Player player)
    {
        playerRef = player;
    }

    public virtual void OnEnter() 
    {
        CurrentControlledCamera = playerRef.GetComponentInChildren<Camera>();
        CurrentControlledCharacter = playerRef.GetComponent<CharacterController>();
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

        CurrentControlledCharacter.Move(moveDirection);
    }

    public virtual void Tick()
    {
        inputForward = Input.GetAxis("Forward");
        inputStrafe = Input.GetAxis("Strafe");
    }

    public virtual void OnExit() 
    {
        AudioListener audioListener = CurrentControlledCamera.GetComponent<AudioListener>();

        if (audioListener) audioListener.enabled = false;
    }
}
