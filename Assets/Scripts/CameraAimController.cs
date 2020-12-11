using System.Collections;
using System.Collections.Generic;
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
    
    private Transform parentTransform;
    private AudioListener audioListener;
    private bool canMove;

    private float inputMouseX;
    private float inputMouseY;

    public bool CanMove
    {
        get => canMove;
        set
        {
            if (!audioListener)
                audioListener = GetComponent<AudioListener>();

            audioListener.enabled = value;
            canMove = value;
        }
    }

    private void Awake()
    {
        parentTransform = gameObject.transform.parent;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        if (!CanMove)
            return;

        HandleCameraPitch();

        HandleParentRotation();
    }

    private void Update()
    {
        inputMouseX = Input.GetAxis("Mouse X");
        inputMouseY = Input.GetAxis("Mouse Y");
    }

    private void HandleCameraPitch()
    {
        Vector3 cameraRotation = transform.localRotation.eulerAngles;
        
        cameraRotation.x -= inputMouseY * mouseSensitivity * Time.fixedDeltaTime;

        if (cameraRotation.x > 180f)
        {
            cameraRotation.x = Mathf.Max(cameraRotation.x, 360f - maxLookUpRotation);
        }
        else
        {
            cameraRotation.x = Mathf.Min(cameraRotation.x, maxLookDownRotation);
        }

        transform.localRotation = Quaternion.Euler(cameraRotation.x, 0f, 0f);
    }

    private void HandleParentRotation()
    {
        parentTransform.Rotate(Vector3.up * inputMouseX * Time.fixedDeltaTime * mouseSensitivity);
    }
}
