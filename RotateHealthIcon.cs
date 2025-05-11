using UnityEngine;

public class RotateHealthIcon : MonoBehaviour
{
    [Header("---Rotation Speed---")]
    public float rotationSpeed = 90f;

    private void Update()
    {
        RotateIcon();
    }
    private void RotateIcon()
    {
        
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        
    }
}