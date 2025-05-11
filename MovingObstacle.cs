using System.Collections;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [Range(0, 5)]
    public float speed; // Speed of the obstacle
    [Range(0, 2)]
    public float waitDuration;
    Vector3 targetPos;
    public GameObject ways;
    public Transform[] wayPoints; // Array of waypoints
    int pointIndex; // Current waypoint index
    int pointCount; // Number of waypoints
    int direction; // Direction of movement (1 for forward, -1 for backward)

    int speedMultiplier = 1; // Speed multiplier for the obstacle
    private void Awake()
    {
        wayPoints = new Transform[ways.transform.childCount]; // Initialize the wayPoints array based on the number of child objects
        for (int i = 0; i < ways.gameObject.transform.childCount; i++)
        {
            wayPoints[i] = ways.transform.GetChild(i).gameObject.transform; // Assign each child object to the wayPoints array
        }
    }
    private void Start()
    {
        pointCount = wayPoints.Length; // Get the number of waypoints
        if (pointCount > 1) // التأكد من وجود نقطتين على الأقل
        {
            pointIndex = 0; // Start at the first waypoint
            targetPos = wayPoints[pointIndex].transform.position; // Set the target position to the first waypoint
            direction = 1; // تهيئة الاتجاه
        }
        else
        {
            Debug.LogWarning("MovingObstacle needs at least two waypoints to function.");
            enabled = false; // تعطيل السكريبت إذا لم تكن هناك نقاط كافية
        }
    }
    private void Update()
    {
        if (wayPoints == null || wayPoints.Length <= 1) return; // التحقق من وجود نقاط كافية

        var step = speedMultiplier * speed * Time.deltaTime; // Calculate the step size based on speed and time
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step); // Move towards the target position
        if (transform.position == targetPos)
        {
            NextPoint(); // Call the NextPoint method when the obstacle reaches the target position
        }
    }
    void NextPoint()
    {
        if (wayPoints == null || wayPoints.Length <= 1) return; // التحقق من وجود نقاط كافية

        if (pointIndex == pointCount - 1)
        {
            direction = -1; // Change direction to backward
        }
        else if (pointIndex == 0)
        {
            direction = 1; // Change direction to forward
        }

        pointIndex += direction; // Update the waypoint index based on the direction

        // التأكد من أن pointIndex ضمن النطاق الصحيح
        if (pointIndex < 0)
        {
            pointIndex = 0;
            direction = 1;
        }
        else if (pointIndex >= pointCount)
        {
            pointIndex = pointCount - 1;
            direction = -1;
        }

        targetPos = wayPoints[pointIndex].transform.position; // Set the target position to the next waypoint
        StartCoroutine(WaitNextPoint());
    }
    IEnumerator WaitNextPoint()
    {
        speedMultiplier = 0; // Stop the obstacle temporarily
        yield return new WaitForSeconds(waitDuration); // Wait for the specified duration
        speedMultiplier = 1; // Resume the obstacle's movement
    }
}