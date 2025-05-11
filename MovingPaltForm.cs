using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Range(0, 5)]
    public float speed; 
    [Range(0, 2)]
    public float waitDuration ;
    Vector3 targetPos;  
    public GameObject ways; 
    public Transform[] wayPoints; 
     int pointIndex ;    
    int pointCount; 
    int direction = 1 ; 
     int speedMultiplier=1 ; // Speed multiplier for the obstacle

    MovementController movementController;  
    Rigidbody2D rb; 
    Vector3 moveDirection; 
    Rigidbody2D PlayerRb;

    private void Awake()
    {
        movementController = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>();    
        rb = GetComponent<Rigidbody2D>();   
        PlayerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();  

        wayPoints = new Transform[ways.transform.childCount];   
        for (int i = 0; i < ways.gameObject.transform.childCount; i++) 
    
        {
            wayPoints[i] = ways.transform.GetChild(i).gameObject.transform; 
        }
    }
    private void Start()
    {
        pointIndex = 1;  
        pointCount = wayPoints.Length;  
        targetPos = wayPoints[pointIndex].transform.position;    
        //DirectionCalculate();
    }
    private void Update()
    {
        var step = speedMultiplier*speed*Time.deltaTime; // Calculate the step size based on speed and time
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step); // Move towards the target position
       if(transform.position == targetPos)
        {
            NextPoint(); // Call the NextPoint method when the obstacle reaches the target position
        }
        
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * speed;    
    }
    void NextPoint()
    {
        if (wayPoints == null || wayPoints.Length <= 1) return; // التحقق من وجود نقاط كافية
        if (pointIndex == pointCount - 1) 
        {
            direction = -1; // Change direction to backward
        }
        if (pointIndex == 0) 
        {
            direction = 1; // Change direction to forward
        }
        pointIndex += direction; // Update the waypoint index based on the direction
        targetPos = wayPoints[pointIndex].transform.position; // Set the target position to the next waypoint
       // DirectionCalculate();
        StartCoroutine(WaitNextPoint());
    }
    IEnumerator WaitNextPoint()
    {
        speedMultiplier = 0; // Stop the obstacle temporarily
        yield return new WaitForSeconds(waitDuration); // Wait for the specified duration
        speedMultiplier = 1; // Resume the obstacle's movement
        //DirectionCalculate();
    }
    // private void DirectionCalculate()
    // {
    //     moveDirection = (targetPos - transform.position).normalized;   
    // }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && movementController != null)  
        {
            movementController.isOnPlatform = true; 
            movementController.PlatformRb = rb; 
            // Removed gravityScale modification for smoother physics
            // PlayerRb.gravityScale = PlayerRb.gravityScale * 50; 
        }
        // else 
        // {
        //     Debug.LogError("Player not found or MovementController is null!");
        // }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && movementController != null)
        {
            movementController.isOnPlatform = false;    
            movementController.PlatformRb = null;   
            // Removed gravityScale modification for smoother physics
            // PlayerRb.gravityScale = PlayerRb.gravityScale / 50;  

        }
    }
}
