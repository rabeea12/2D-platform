using UnityEngine;

public class InstantDeath : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Find the GameController and call the Die function
            GameController gameController = other.GetComponent<GameController>();
            if (gameController != null)
            {
                gameController.Die();
            }
            else
            {
                Debug.LogError("Player has no GameController!");
                // Destroy(other.gameObject); // Destroy the player if there is no GameController
            }
        }
    }
}