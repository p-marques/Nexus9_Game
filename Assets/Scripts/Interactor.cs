using UnityEngine;

public class Interactor : MonoBehaviour
{
    private const float RAYCAST_DISTANCE = 30f;

    [SerializeField]
    private IInteractableVariable currentInteractable;

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

        currentInteractable.Value = hitInteractable;
    }
}
