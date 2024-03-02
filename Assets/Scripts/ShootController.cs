using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script triggers the Particle System when the player presses the Fire button.
/// </summary>

// Help on the new input system from: https://www.youtube.com/watch?v=SyA4PPiXorI

public class ShootController : MonoBehaviour
{
    public float CooldownTime = 0.5f; // Cooldown time in seconds
    private float Cooldown = 0.0f; // Current cooldown time
    private bool CooldownActive = false; // Is the cooldown active?
    private DefaultPlayerActions playerActions;
    public PlayerMovement playerMovement;
    public SpriteRenderer spriteRenderer; // Reference to the Sprite Renderer

    public ParticleSystem particleSystem; // Reference to the Particle System

    void Awake()
    {
        playerActions = new DefaultPlayerActions();
        playerActions.Player.Fire.performed += _ => Fire();
    }

    void Update()
    {
        if (CooldownActive)
        {
        IncrementCooldown();
        }
    }

    void OnEnable()
    {
        playerActions.Enable();
    }

    void OnDisable()
    {
        playerActions.Disable();
    }

    void Fire()
    {
        if (CooldownActive == false)
        {
            CooldownActive = true;
            Cooldown = 0.0f;
            particleSystem?.Play();
            playerMovement.Shoot();
        }

    }

    void IncrementCooldown()
    {
        ChangeColor();
        Cooldown += Time.deltaTime;
        if (Cooldown > CooldownTime)
        {
            Cooldown = 0.0f;
            CooldownActive = false;
        }
    }

    // This method gradually changes the color of the player sprite from white to red based on the cooldown time
    void ChangeColor()
    {
        // Change the color of the sprite
        spriteRenderer.color = Color.Lerp(Color.white, Color.red, Cooldown / CooldownTime);
    }


}
