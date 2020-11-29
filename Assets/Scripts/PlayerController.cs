using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float GRAVITY = 100f;

    [SerializeField]
    private float playerSpeed = 4f;

    private CharacterController characterController;
    private float inputForward;
    private float inputStrafe;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = transform.right * inputStrafe + transform.forward * inputForward;

        if (!characterController.isGrounded)
        {
            moveDirection.y -= GRAVITY;
        }

        characterController.Move(moveDirection * Time.deltaTime * playerSpeed);
    }

    private void Update()
    {
        inputForward = Input.GetAxis("Forward");
        inputStrafe = Input.GetAxis("Strafe");
    }
}
