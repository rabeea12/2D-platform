using UnityEngine;
using System.Collections;   
using System.Collections.Generic;

public class ParticleController : MonoBehaviour
{
    [Header("------Particle System-----")]
    [SerializeField] ParticleSystem movementParticle; 

    [Range(0, 10)]
    [SerializeField] int occurAfterVelocity;

    [Range(0,1f)]
    [SerializeField] float dustFormationPeriod;
    [SerializeField] Rigidbody2D playerRb;
    float counter;
    bool isOnGround;

    [Header("------Particles ----")]
    [SerializeField] ParticleSystem fallParticle;
    [SerializeField] ParticleSystem touchParticle;
    [SerializeField] ParticleSystem dieParticle;

    AudioManager audioManager; // Reference to the AudioManager script
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // Find the AudioManager in the scene
    }
    
    
    private void Start()
    {
        //touchParticle.transform.parent = null; // Set the parent of the touch particle system to null to avoid inheriting the player's position 
        //dieParticle.transform.parent = null; // Set the parent of the die particle system to null to avoid inheriting the player's position
    }
    private void Update()
    {
        counter += Time.deltaTime;

        if (isOnGround && Mathf.Abs(playerRb.linearVelocity.x) > occurAfterVelocity)
        {
            if (counter > dustFormationPeriod)
            {
                movementParticle.Play(); // Play the particle system
                counter = 0; // Reset the counter
            }
            
        }  
    }
    public void PlayParticle(Particles particle, Vector2 pos=default(Vector2))
    {
        switch (particle)
        {
            case Particles.touch:
                audioManager.PlaySFX(audioManager.wallTouch); // Play the portal in sound effect
                touchParticle.transform.position = pos; // Set the position of the particle system
                touchParticle.Play(); // Play the particle system
                break;
            case Particles.fall:
                audioManager.PlaySFX(audioManager.wallTouch); // Play the portal in sound effect
                fallParticle.Play(); // Play the particle system
                break;
            case Particles.die:
                audioManager.PlaySFX(audioManager.death); // Play the death sound effect
                dieParticle.transform.position = pos; // Set the position of the particle system
                dieParticle.Play(); // Play the particle system
                isOnGround = false; // Player is not on the ground
                break;
            default:
                break;
            
        }
    }
        public enum Particles
    {
        touch,
        fall,
        die
    }    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            fallParticle.Play(); // Play the particle system
            audioManager.PlaySFX(audioManager.wallTouch); // Same sound as wall touch
            isOnGround = true; // Player is on the ground
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isOnGround = false; // Player is not on the ground
        }
    }
}
