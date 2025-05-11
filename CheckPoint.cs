using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] GameController gameController; // Reference to the GameController script
    public Transform respawnPoint;
    SpriteRenderer spriteRenderer;

    public Sprite passive, active;
    Collider2D coll;
    AudioManager audioManager; // Reference to the AudioManager script
    private bool hasPlayedSound = false;

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // Find the AudioManager in the scene
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasPlayedSound)
        {
            audioManager.PlaySFX(audioManager.checkpoint); // Play the checkpoint sound effect
            gameController.UpdateCheckpoint(respawnPoint.position); // Update the checkpoint position in the GameController
            spriteRenderer.sprite = active; // Change the sprite to active
            coll.enabled = false; // Disable the collider to prevent re-triggering
            hasPlayedSound = true;
        }
    }
}
