using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.UI; // Required for UI elements
using UnityEngine.EventSystems; // Required for Event Systems

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : MonoBehaviour
{
    [Header("Movement Settings")]
    [Range(1, 10)] [SerializeField] private int speed ;
    [Range(1, 10)] [SerializeField] private float acceleration;
    private float speedMultiplier;
    private bool btnPressed;

    [Header("Collision Settings")]
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform wallCheckpoint;
    [SerializeField] private float coyoteTime = 0.1f;

    private Rigidbody2D rb;
    private bool isWallTouch;
    private Vector2 relativeTransform;
    private float coyoteTimer;

    [Header("Platform Settings")]
    public bool isOnPlatform;
    public Rigidbody2D PlatformRb;

    [Header("Effects")]
    public ParticleController particleController;

    public event Action OnFlipped;
    private bool controlsEnabled = true;

    // [Header("Touch Control")]
    // public RectTransform fullScreenButtonRect; // Assign in Inspector
    // private Button fullScreenButton;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        UpdateRelativeTransform();
        //SetupFullScreenButton();
    }

    private void FixedUpdate()
    {
        if (!controlsEnabled) return;
        HandleMovement();
        CheckWallCollision();
        HandleWallFlip();
    }

    private void HandleMovement()
    {
        UpdateSpeedMultiplier();
        float targetSpeed = speed * speedMultiplier * relativeTransform.x;

        if (isOnPlatform && PlatformRb != null)
        {
            Vector2 platformVelocity = PlatformRb.linearVelocity;
            rb.linearVelocity = new Vector2(
                targetSpeed + platformVelocity.x,
                rb.linearVelocity.y + platformVelocity.y
            );
        }
        else
        {
            rb.linearVelocity = new Vector2(targetSpeed, rb.linearVelocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            isOnPlatform = true;
            PlatformRb = collision.gameObject.GetComponent<Rigidbody2D>();
            transform.SetParent(collision.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            isOnPlatform = false;
            PlatformRb = null;
            transform.SetParent(null);
        }
    }

    private void CheckWallCollision()
    {
        isWallTouch = Physics2D.OverlapBox(
            wallCheckpoint.position,
            new Vector2(0.06f, 0.6f),
            0,
            wallLayer
        ) != null;
    }

    private void HandleWallFlip()
    {
        if (isWallTouch)
        {
            coyoteTimer += Time.fixedDeltaTime;
            if (coyoteTimer >= coyoteTime)
            {
                Flip();
                coyoteTimer = 0;
            }
        }
        else
        {
            coyoteTimer = 0;
        }
    }

    public void Flip()
    {
        particleController?.PlayParticle(ParticleController.Particles.touch, (Vector2)wallCheckpoint.position);
        transform.Rotate(0f, 180f, 0f);
        UpdateRelativeTransform();
        OnFlipped?.Invoke();
    }

    private void UpdateRelativeTransform() => relativeTransform = transform.InverseTransformVector(Vector2.one);

    //  Input System Action (You can remove this if you're not using it)
    public void Move(InputAction.CallbackContext context) => btnPressed = context.ReadValueAsButton();

    private void UpdateSpeedMultiplier()
    {
        float targetMultiplier = btnPressed ? 1 : 0;
        speedMultiplier = Mathf.Lerp(speedMultiplier, targetMultiplier, acceleration * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        if (wallCheckpoint != null)
        {
            Gizmos.color = isWallTouch ? Color.green : Color.red;
            Gizmos.DrawWireCube(wallCheckpoint.position, new Vector3(0.06f, 0.6f, 0));
        }
    }

    public void DisableControls()
    {
        controlsEnabled = false;
        rb.linearVelocity = Vector2.zero;
    }

    public void EnableControls()
    {
        controlsEnabled = true;
    }

    // //  New Touch Input System
    // private void SetupFullScreenButton()
    // {
    //     GameObject buttonObj = new GameObject("FullScreenButton", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button));
    //     buttonObj.transform.SetParent(FindCorrectParent(), false);

    // Transform FindCorrectParent()
    // {
    //     Transform parentTransform = GameObject.Find("PauseMenu Controller")?.transform;

    //     if (parentTransform == null)
    //     {
    //         parentTransform = GameObject.Find("FinalScene Pause Menu")?.transform;
    //     }

    //     if (parentTransform == null)
    //     {
    //         Debug.LogError("لم يتم العثور على أي من 'PauseMenu Controller' أو 'FinalPass' في المشهد!");
    //         // يمكنك هنا إرجاع قيمة افتراضية أو التعامل مع الخطأ بطريقة أخرى مناسبة لمشروعك
    //         return null; // أو يمكنك إرجاع ترانسفورم الـ Canvas الرئيسي مثلاً
    //     }

    //     return parentTransform;
    // }
    //     fullScreenButton = buttonObj.GetComponent<Button>();
    //     RectTransform buttonRect = buttonObj.GetComponent<RectTransform>();

    //     buttonRect.anchorMin = Vector2.zero;
    //     buttonRect.anchorMax = Vector2.one;
    //     buttonRect.sizeDelta = Vector2.zero;
    //     buttonRect.offsetMin = Vector2.zero;
    //     buttonRect.offsetMax = Vector2.zero;

    //     //  Optionally, make the button transparent
    //     Color tempColor = fullScreenButton.GetComponent<Image>().color;
    //     tempColor.a = 0f; //  Transparent
    //     fullScreenButton.GetComponent<Image>().color = tempColor;

    //     fullScreenButton.onClick.AddListener(HandleScreenTouch);
    // }

    // private void HandleScreenTouch()
    // {
    //     btnPressed = true;
    //     UpdateSpeedMultiplier(); //  Update speed immediately
    //     Invoke("ResetButtonPress", 0.1f); // Reset after a short delay to simulate a tap
    // }

    // private void ResetButtonPress()
    // {
    //     btnPressed = false;
    //     UpdateSpeedMultiplier(); //  Update speed again when resetting
    // }
}