using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform target;
    Vector3 vel = Vector3.zero;
    [Range(0,1)]
    public float smoothTime ;
    public Vector3 offset;

    [Header("Camera Limits")]
    public Vector2 xLimit;
    public Vector2 yLimit;
     Animation anim;


    private void Awake()
    {
        anim = GetComponent<Animation>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player GameObject with tag 'Player' not found!", this);
            target = null;
        }
    }
    public void PlayDeathAnimation()
    {
        if (anim != null)
        {
            anim.Play("White Screen");
        }
        else
        {
            Debug.LogWarning("Animation component not found on CameraController.");
        }
    }
    
    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = target.position + offset;
        targetPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, xLimit.x, xLimit.y),
            Mathf.Clamp(targetPosition.y, yLimit.x, yLimit.y),
            -10f);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, smoothTime);
    }
}
