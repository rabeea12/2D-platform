using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 checkpointPos; // Store the checkpoint position
    Rigidbody2D playerRb;
    CameraController cameraController;
    public ParticleController particleController;
    public float fallThreshold = -10f;
    MovementController movementController;
    private bool isPlayerDead = false;
    [SerializeField] private LoseHandler loseHandler; // Reference to LoseHandler


    private void Awake()
    {
        
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        movementController = GetComponent<MovementController>();
        playerRb = GetComponent<Rigidbody2D>();
        if (particleController == null)
        {
            particleController = GameObject.FindFirstObjectByType<ParticleController>();
        }
        if (loseHandler == null)
        {
            Debug.LogError("LoseHandler is not assigned in the Inspector! (GameController)");
        }
    }
    private void Start()
    {
        checkpointPos = transform.position; // Store the initial position of the object
        //playerRotation = transform.rotation; // Store the initial rotation of the object
    }
    private void Update()
    {
        if (!isPlayerDead && transform.position.y < fallThreshold)
        {
            Die();
        }
    }
   
    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos; // Update the checkpoint position when the player reaches a new checkpoint
    }
    public void Die()
    {
        isPlayerDead = true; // منع استدعاء Die() مرة أخرى
        //cameraController.anim.Play("White Screen");
        particleController.PlayParticle(ParticleController.Particles.die, (Vector2)transform.position);
        loseHandler?.ShowLoseScreen(); // Call ShowLoseScreen in LoseHandler

        StartCoroutine(Respawn(0.5f)); // Call the Respawn method after a delay of 0.5 seconds
    }
    
    IEnumerator Respawn(float duration)
    {
        playerRb.simulated = false;
        playerRb.linearVelocity = new Vector2(0, 0);
        transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(duration);
        transform.position = checkpointPos;
        transform.localScale = new Vector3(1, 1, 1);
        playerRb.simulated = true;
        isPlayerDead = false; // السماح بالموت مرة أخرى إذا سقط اللاعب

    }

    public void RespawnPlayer()
    {
        if (!isPlayerDead)
        {
            StartCoroutine(Respawn(0.5f));
        }
    }
}
