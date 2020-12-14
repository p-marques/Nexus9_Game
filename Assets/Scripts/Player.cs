using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 4f;

    [SerializeField]
    [Range(0.01f, 0.1f)]
    private float groundDetectionRadius = 0.05f;

    [SerializeField]
    private Storage inventory;

    [Header("Events")]
    [SerializeField]
    [Tooltip("Event fired entering/exiting hijack")]
    private NexusEvent onHijackChangeEvent;

    private StateMachine<IPlayerState> stateMachine;

    public Storage Inventory => inventory;
    public float MoveSpeed => moveSpeed;
    public float GroundDetectionRadius => groundDetectionRadius;
    public IHijackable CurrentHijack { get; set; }
    public Camera CurrentCamera => stateMachine?.CurrentState.CurrentControlledCamera;

    private void Awake()
    {
        if (!inventory) Debug.LogError("Player doesn't have an inventory!");

        stateMachine = new StateMachine<IPlayerState>();

        PlayerNormalState normalState = new PlayerNormalState(this);
        PlayerHijackState hijackState = new PlayerHijackState(this);

        stateMachine.SetState(normalState);

        stateMachine.AddTransition(normalState, hijackState, () => CurrentHijack != null, onHijackChangeEvent.Raise);
        stateMachine.AddTransition(hijackState, normalState, () => CurrentHijack == null, onHijackChangeEvent.Raise);
    }

    private void FixedUpdate() => stateMachine.PhysicsTick();

    private void Update() => stateMachine.Tick();
}
