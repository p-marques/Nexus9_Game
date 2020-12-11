using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droid : MonoBehaviour, IHijackable
{
    private const float RANGE = 30f;
    private const string INTERACTION_TEXT = "Hijack";

    private Player playerRef;
    private PlayerController controller;
    private GameObject cameraObj;
    private CameraAimController cameraAim;

    public float Range => RANGE;

    public string InteractionText => INTERACTION_TEXT;

    private void Awake()
    {
        controller = GetComponentInChildren<PlayerController>();
        cameraObj = GetComponentInChildren<Camera>(true).gameObject;
        cameraAim = cameraObj.GetComponent<CameraAimController>();
    }

    private void Update()
    {
        if (playerRef && Input.GetKeyDown(KeyCode.Escape))
        {
            controller.CanMove = false;

            cameraAim.CanMove = false;

            cameraObj.SetActive(false);

            playerRef.SetCanMove(true);

            playerRef = null;
        }
    }

    public void Interact(Player player)
    {
        player.SetCanMove(false);

        controller.CanMove = true;

        cameraAim.CanMove = true;

        cameraObj.SetActive(true);
    }
}
