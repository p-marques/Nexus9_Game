using UnityEngine;

public class CameraAimController : MonoBehaviour
{
    [SerializeField]
    [Range(10, 300)]
    private float mouseSensitivity = 230f;

    [SerializeField]
    private float maxLookUpRotation = 80f;

    [SerializeField]
    private float maxLookDownRotation = 60f;

    private Player playerRef;
    private float inputMouseX;
    private float inputMouseY;

    private void Awake()
    {
        playerRef = GetComponent<Player>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        if (!playerRef.CurrentCamera) return;

        Transform camTransform = playerRef.CurrentCamera.transform;

        HandleCameraPitch(camTransform);

        HandleParentRotation(camTransform);
    }

    private void Update()
    {
        inputMouseX = Input.GetAxis("Mouse X");
        inputMouseY = Input.GetAxis("Mouse Y");
    }

    private void HandleCameraPitch(Transform camTransform)
    {
        Vector3 cameraRotation = camTransform.localRotation.eulerAngles;
        
        cameraRotation.x -= inputMouseY * mouseSensitivity * Time.fixedDeltaTime;

        if (cameraRotation.x > 180f)
        {
            cameraRotation.x = Mathf.Max(cameraRotation.x, 360f - maxLookUpRotation);
        }
        else
        {
            cameraRotation.x = Mathf.Min(cameraRotation.x, maxLookDownRotation);
        }

        camTransform.localRotation = Quaternion.Euler(cameraRotation.x, 0f, 0f);
    }

    private void HandleParentRotation(Transform camTransform)
    {
        camTransform.parent.Rotate(Vector3.up * inputMouseX * Time.fixedDeltaTime * mouseSensitivity);
    }
}
