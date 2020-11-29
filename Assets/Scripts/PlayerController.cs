﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float FORCEDGRAVITY = 20f;
    private const float GROUNDDETECTIONRADIUS = 0.02f;

    [SerializeField]
    private float playerSpeed = 4f;

    private CharacterController characterController;
    private float inputForward;
    private float inputStrafe;

    private Vector3 GroundDetectionPoint => transform.parent.transform.position - new Vector3(0, 1f, 0);

    private bool IsGrounded
    {
        get
        {
            Collider[] collisions = Physics.OverlapSphere(GroundDetectionPoint, GROUNDDETECTIONRADIUS);

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
        if (!characterController)
            return;

        Vector3 moveDirection = transform.right * inputStrafe + transform.forward * inputForward;

        moveDirection *= playerSpeed * Time.fixedDeltaTime;

        if (!IsGrounded)
        {
            moveDirection.y -= FORCEDGRAVITY;
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

        Gizmos.DrawSphere(GroundDetectionPoint, GROUNDDETECTIONRADIUS);
    }
}
