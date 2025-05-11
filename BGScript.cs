using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.Mathematics;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class BGScript : MonoBehaviour
{
    // public float speed = 0.5f; // Speed of the background movement
    // [SerializeField] private Renderer BGRenderer; // Reference to the background renderer
    

    // void Update()
    // {
    //     BGRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0); // Move the background texture
    // }
    //////////////////////////////////////////////////
    // Material mat;
    // float distance;
    // [Range(0f, 1f)]
    // public float speed = 0.5f; // Speed of the background movement
    // void Start()
    // {
    //     mat = GetComponent<Renderer>().material; // Get the material of the object this script is attached to
    // }
    // void Update()
    // {
    //     distance += speed * Time.deltaTime; // Increment the distance based on speed and time
    //     mat.SetTextureOffset("_MainTex",Vector2.right*distance);

    // }
    /////////////////////////////////////////////////////
    Transform cam;
    Vector3 camStartPos;
    float distance;
    GameObject[] backgrounds;
    Material[] mat;
    float [] backSpeed;
    float farthestBack;
    [Range(0f, 1f)]
    public float parallaxSpeed ; // Speed of the background movement
    void Start()
    {
        cam = Camera.main.transform; 
        camStartPos = cam.position; // Store the initial position of the camera 

        int backCount = transform.childCount; // Get the number of child objects (backgrounds)
        mat = new Material[backCount]; // Initialize the material arrayb
        backSpeed = new float[backCount]; // Initialize the speed array
        backgrounds = new GameObject[backCount]; // Initialize the background array
        for (int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject; // Get each child object (background)
            mat[i] = backgrounds[i].GetComponent<Renderer>().material; // Get the material of each background
            
        }
        BackSpeedCalculation(backCount); // Calculate the speed of each background based on its distance from the camera
    }
    void BackSpeedCalculation(int backCount)
    {
        for (int i = 0; i < backCount; i++)
        {
            if ((backgrounds[i].transform.position.z - cam.position.z) > farthestBack)
            {
                farthestBack = backgrounds[i].transform.position.z - cam.position.z;
            }
        }
        for (int i = 0; i < backCount; i++)
        {
            backSpeed[i] =1- (backgrounds[i].transform.position.z - cam.position.z) / farthestBack; // Calculate the speed of each background based on its distance from the camera
        }
    }
    
    private void LateUpdate()
    {
        distance = cam.position.x - camStartPos.x; // Calculate the distance moved by the camera in the x direction
        transform.position = new Vector3(cam.position.x, transform.position.y, 0); // Update the position of the background to follow the camera
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed; // Calculate the speed of the background based on its distance from the camera
            mat[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed); // Update the texture offset of the material to create a parallax effect
        }
    }
        
    
}
