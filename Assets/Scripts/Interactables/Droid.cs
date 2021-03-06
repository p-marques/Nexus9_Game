﻿using UnityEngine;

public class Droid : InteractableWithRequirements, IHijackable
{
    private const float RANGE = 30f;
    private const string INTERACTION_TEXT = "Hijack";

    public float Range => RANGE;

    public string InteractionText => INTERACTION_TEXT;

    public Transform Transform => transform;

    public void Interact(Player player)
    {
        if (!CheckControlSystem())
        {
            onActionBlocked.Raise();
            return;
        }

        player.CurrentHijack = this;
    }
}
