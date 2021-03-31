using UnityEngine;

public class PlayerNormalState : IPlayerState
{
    private const float FORCED_GRAVITY = 20f;

    private readonly Player _playerRef;
    private readonly NexusEvent _onToggleInventoryUIShow;
    private float _inputForward;
    private float _inputStrafe;

    private Vector3 GroundDetectionPoint =>
        CurrentControlledCharacter.transform.position;

    public Camera CurrentControlledCamera { get; protected set; }
    public CharacterController CurrentControlledCharacter { get; protected set; }
    public virtual bool CanPickUpItem => true;

    private bool IsGrounded
    {
        get
        {
            Collider[] collisions = Physics.OverlapSphere(GroundDetectionPoint, _playerRef.GroundDetectionRadius, _playerRef.GroundDetectionLayer);

            if (collisions.Length > 0)
                return true;
            else
                return false;
        }
    }

    public PlayerNormalState(Player player, NexusEvent toggleInventoryUIEvent)
    {
        _playerRef = player;

        _onToggleInventoryUIShow = toggleInventoryUIEvent;
    }

    public virtual void OnEnter() 
    {
        CurrentControlledCamera = _playerRef.GetComponentInChildren<Camera>();
        CurrentControlledCharacter = _playerRef.GetComponent<CharacterController>();
        CurrentControlledCamera.GetComponent<AudioListener>().enabled = true;
    }

    public virtual void PhysicsTick()
    {
        if (!CurrentControlledCharacter) return;

        Transform transform = CurrentControlledCharacter.transform;

        Vector3 moveDirection = transform.right * _inputStrafe + transform.forward * _inputForward;

        moveDirection *= _playerRef.MoveSpeed * Time.fixedDeltaTime;

        if (!IsGrounded)
        {
            moveDirection.y -= FORCED_GRAVITY;
        }

        CurrentControlledCharacter.Move(moveDirection);
    }

    public virtual void Tick()
    {
        _inputForward = Input.GetAxis("Forward");
        _inputStrafe = Input.GetAxis("Strafe");

        _playerRef.Animator.SetFloat("MovementForward", _inputForward);
        _playerRef.Animator.SetFloat("MovementStrafe", _inputStrafe);

        if (Input.GetKeyDown(KeyCode.G))
        {
            _playerRef.Animator.SetTrigger("HijackToggle");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!_onToggleInventoryUIShow) return;

            _onToggleInventoryUIShow.Raise();
        }
    }

    public virtual void OnExit() 
    {
        
    }
}
