using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float FORCED_GRAVITY = 20f;

    [SerializeField]
    private float moveSpeed = 4f;

    [SerializeField]
    [Range(0.01f, 0.1f)]
    private float groundDetectionRadius = 0.05f;

    private CharacterController characterController;
    private float inputForward;
    private float inputStrafe;

    private Vector3 GroundDetectionPoint => transform.parent.transform.position - new Vector3(0, 1f, 0);

    public bool CanMove { get; set; }

    private bool IsGrounded
    {
        get
        {
            Collider[] collisions = Physics.OverlapSphere(GroundDetectionPoint, groundDetectionRadius);

            if (collisions.Length > 0)
                return true;
            else
                return false;
        }
    }

    private void Awake()
    {
        characterController = GetComponentInParent<CharacterController>();

        if (!characterController)
            Debug.LogError($"A PlayerController couldn't find it's parent CharacterController.");
    }

    private void FixedUpdate()
    {
        if (!characterController || !CanMove)
            return;

        Vector3 moveDirection = transform.right * inputStrafe + transform.forward * inputForward;

        moveDirection *= moveSpeed * Time.fixedDeltaTime;

        if (!IsGrounded)
        {
            moveDirection.y -= FORCED_GRAVITY;
        }

        characterController.Move(moveDirection);
    }

    private void Update()
    {
        inputForward = Input.GetAxis("Forward");
        inputStrafe = Input.GetAxis("Strafe");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;

        Gizmos.DrawSphere(GroundDetectionPoint, groundDetectionRadius);
    }
}
