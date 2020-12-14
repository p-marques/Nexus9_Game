using UnityEngine;

public class Interactor : MonoBehaviour
{
    private const float RAYCAST_DISTANCE = 30f;

    [SerializeField]
    private IInteractableVariable currentInteractable;
    
    private Player playerRef;

    private void Awake()
    {
        playerRef = GetComponent<Player>();

        if (!playerRef)
            Debug.LogError("Interactor failed to find component Player!");
    }

    private void Update()
    {
        Transform cameraTransform = playerRef.CurrentCamera.transform;
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

        currentInteractable.Value = hitInteractable;

        if (currentInteractable.Value != null && Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable.Value.Interact(playerRef);
        }
    }
}
