/*
 * File: OutOfBounds.cs
 * -------------------------
 * This file contains the implementation of the out of bounds area.
 *
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using UnityEngine;

/// <summary>
/// This script manages collisions with the out of bounds area.
/// </summary>
public class OutOfBounds : MonoBehaviour
{
    private GameManager gameManager; // Reference to the GameManager
    private PlayerManager playerManager; // Reference to the PlayerManager

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        gameManager = GameManager.instance;
        playerManager = PlayerManager.Instance;
    }

    /// <summary>
    /// OnTriggerEnter2D is called when the Collider2D other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision, likely the player.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerManager?.Die();
            gameManager?.ResetLevel();
        }
    }
}
