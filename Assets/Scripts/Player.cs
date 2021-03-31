using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 4f;

    [Range(0.01f, 0.1f)]
    [SerializeField] private float _groundDetectionRadius = 0.05f;

    [SerializeField] private LayerMask _groundDetectionLayer;

    [SerializeField] private Storage _inventory;

    [Header("Events")]
    [Tooltip("Event fired entering/exiting hijack")]
    [SerializeField] private NexusEvent _onHijackChangeEvent;

    [Tooltip("Event: toggle ON/OFF Inventory UI")]
    [SerializeField] private NexusEvent _inventoryUIToggleEvent;

    private Animator _animator;
    private StateMachine<IPlayerState> _stateMachine;

    public Storage Inventory => _inventory;
    public Animator Animator => _animator;
    public float MoveSpeed => _moveSpeed;
    public float GroundDetectionRadius => _groundDetectionRadius;
    public LayerMask GroundDetectionLayer => _groundDetectionLayer;
    public IInteractable CurrentInteraction { get; set; }
    public IHijackable CurrentHijack { get; set; }
    public Camera CurrentCamera => _stateMachine?.CurrentState.CurrentControlledCamera;
    public bool IsAnalysingItem { get; set; }
    public bool CanPickUpItem => _stateMachine.CurrentState.CanPickUpItem;

    private void Awake()
    {
        if (!_inventory) Debug.LogError("Player doesn't have an inventory!");

        _animator = GetComponentInChildren<Animator>();

        if (!_animator) Debug.LogError("Player doesn't have an Animator!");

        _stateMachine = new StateMachine<IPlayerState>();

        PlayerNormalState normalState = new PlayerNormalState(this, _inventoryUIToggleEvent);
        PlayerHijackState hijackState = new PlayerHijackState(this, _inventoryUIToggleEvent);
        PlayerInteractionState interactionState = new PlayerInteractionState(this);

        _stateMachine.SetState(normalState);

        _stateMachine.AddTransition(normalState, hijackState, () => CurrentHijack != null, OnNormalHijackStateSwap);
        _stateMachine.AddTransition(hijackState, normalState, () => CurrentHijack == null, OnNormalHijackStateSwap);

        _stateMachine.AddTransition(normalState, interactionState, () => CurrentInteraction != null || IsAnalysingItem);
        _stateMachine.AddTransition(interactionState, normalState, () => CurrentInteraction == null && CurrentHijack == null && !IsAnalysingItem);

        _stateMachine.AddTransition(hijackState, interactionState, () => CurrentInteraction != null || IsAnalysingItem);
        _stateMachine.AddTransition(interactionState, hijackState, () => CurrentInteraction == null && CurrentHijack != null && !IsAnalysingItem);
    }

    private void FixedUpdate() => _stateMachine.PhysicsTick();

    private void Update() => _stateMachine.Tick();

    private void OnNormalHijackStateSwap()
    {
        _onHijackChangeEvent.Raise();
        CurrentCamera.GetComponent<AudioListener>().enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 point = transform.position - new Vector3(0, 1f, 0);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, GroundDetectionRadius);
    }
}
