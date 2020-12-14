using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droid : MonoBehaviour, IHijackable
{
    private const float RANGE = 30f;
    private const string INTERACTION_TEXT = "Hijack";

    public float Range => RANGE;

    public string InteractionText => INTERACTION_TEXT;

    public Transform Transform => transform;

    public void Interact(Player player)
    {
        Debug.Log($"HIJACK!");

        player.CurrentHijack = this;
    }
}
