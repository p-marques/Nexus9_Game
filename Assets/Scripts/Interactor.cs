using UnityEngine;

public class Interactor : MonoBehaviour
{
    private const float RAYCAST_DISTANCE = 30f;

    [SerializeField]
    [Tooltip("Event to raise every time the Interactor's Raycast result changes.")]
    private NexusEvent<IInteractable> eventOnChange;

    private IInteractable currentInteractable;
    private Transform cameraTransform;

    private void Awake()
    {
        cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    private void Update()
    {
        IInteractable hitInteractable = null;

        bool hasHit = Physics.Raycast(cameraTransform.position, cameraTransform.forward, 
            out RaycastHit hitInfo, RAYCAST_DISTANCE);

        if (hasHit)
        {
            hitInteractable = hitInfo.transform.GetComponent<IInteractable>();

            if (hitInteractable != null && hitInfo.distance > hitInteractable.Range)
            {
                hitInteractable = null;
            }
        }

        if (currentInteractable != hitInteractable)
        {
            currentInteractable = hitInteractable;

            eventOnChange.Raise(currentInteractable);
        }
    }
}
