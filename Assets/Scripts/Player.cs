using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 4f;

    [SerializeField]
    [Range(0.01f, 0.1f)]
    private float groundDetectionRadius = 0.05f;

    [SerializeField]
    private LayerMask groundDetectionLayer;

    [SerializeField]
    private Storage inventory;

    [Header("Events")]
    [SerializeField]
    [Tooltip("Event fired entering/exiting hijack")]
    private NexusEvent onHijackChangeEvent;

    [SerializeField]
    [Tooltip("Event: toggle ON/OFF Inventory UI")]
    private NexusEvent inventoryUIToggleEvent;

    private Animator animator;
    private StateMachine<IPlayerState> stateMachine;

    public Storage Inventory => inventory;
    public Animator Animator => animator;
    public float MoveSpeed => moveSpeed;
    public float GroundDetectionRadius => groundDetectionRadius;
    public LayerMask GroundDetectionLayer => groundDetectionLayer;
    public IInteractable CurrentInteraction { get; set; }
    public IHijackable CurrentHijack { get; set; }
    public Camera CurrentCamera => stateMachine?.CurrentState.CurrentControlledCamera;
    public bool IsAnalysingItem { get; set; }
    public bool CanPickUpItem => stateMachine.CurrentState.CanPickUpItem;

    private void Awake()
    {
        if (!inventory) Debug.LogError("Player doesn't have an inventory!");

        animator = GetComponentInChildren<Animator>();

        if (!animator) Debug.LogError("Player doesn't have an Animator!");

        stateMachine = new StateMachine<IPlayerState>();

        PlayerNormalState normalState = new PlayerNormalState(this, inventoryUIToggleEvent);
        PlayerHijackState hijackState = new PlayerHijackState(this, inventoryUIToggleEvent);
        PlayerInteractionState interactionState = new PlayerInteractionState(this);

        stateMachine.SetState(normalState);

        stateMachine.AddTransition(normalState, hijackState, () => CurrentHijack != null, OnNormalHijackStateSwap);
        stateMachine.AddTransition(hijackState, normalState, () => CurrentHijack == null, OnNormalHijackStateSwap);

        stateMachine.AddTransition(normalState, interactionState, () => CurrentInteraction != null || IsAnalysingItem);
        stateMachine.AddTransition(interactionState, normalState, () => CurrentInteraction == null && CurrentHijack == null && !IsAnalysingItem);

        stateMachine.AddTransition(hijackState, interactionState, () => CurrentInteraction != null || IsAnalysingItem);
        stateMachine.AddTransition(interactionState, hijackState, () => CurrentInteraction == null && CurrentHijack != null && !IsAnalysingItem);
    }

    private void FixedUpdate() => stateMachine.PhysicsTick();

    private void Update() => stateMachine.Tick();

    private void OnNormalHijackStateSwap()
    {
        onHijackChangeEvent.Raise();
        CurrentCamera.GetComponent<AudioListener>().enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 point = transform.position - new Vector3(0, 1f, 0);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, GroundDetectionRadius);
    }
}
