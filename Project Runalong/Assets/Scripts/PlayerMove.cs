using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // External forces
    public BoolSO Paused;

    // Player variables
    public FloatSO MoveSpeed;
    public float JumpForce;
    public LayerMask GroundMask;

    // Player data
    [Space]
    private Rigidbody2D rb;
    private SpriteRenderer playerSprite;
    private float defaultGravity;
    public BoxCollider2D MainCollider;
    public BoxCollider2D CrouchCollider;

    // Tracking player state
    [SerializeField]
    private bool isGrounded;
    private bool jump;
    [SerializeField]
    private bool sliding;
    private bool startFastFall;
    [SerializeField]
    private bool fastfalling;
    private bool startGlide;
    [SerializeField]
    private bool gliding;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        defaultGravity = rb.gravityScale;
        stopSliding();
    }

    // Update is called once per frame
    // TODO: Generalize controls if it needs to be used on mobile too
    void Update()
    {
        if (Paused.value) return;
        // Raycast down to see if player is grounded, set variable
        BoxCollider2D activeCollider = sliding ? CrouchCollider : MainCollider;
        RaycastHit2D raycast = Physics2D.Raycast(activeCollider.bounds.center, Vector2.down, activeCollider.bounds.extents.y + 0.1f, GroundMask);
        isGrounded = raycast.collider != null;

        // Cancel fastfalling if player lands
        if (isGrounded && fastfalling)
        {
            fastfalling = false;
        }

        // Check to see if player pressed a jump key
        // Allow if: Pressed jump, grounded
        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            jump = true;
            stopSliding();
        }


        // Check to see if player wants to glide
        // Allow if: Holding jump, not fastfalling grounded or gliding, and is descending
        if (Input.GetMouseButton(0) && !fastfalling && !isGrounded && !gliding && rb.velocity.y <= 0)
        {
            startGlide = true;
        } else if (gliding && !Input.GetMouseButton(0))
        {
            stopGliding();
        }

        // Check to see if player wants to slide, or stop sliding
        // Allow if: Holding slide, not already sliding or jumping, is grounded
        if (Input.GetMouseButton(1) && isGrounded && !jump && !sliding)
        {
            startSliding();

        } else if (!Input.GetMouseButton(1) && sliding)
        {
            stopSliding();
        }

        // Check to see if player wants to fastfall
        // Allow if: Pressed slide, not grounded, not fastfalling already
        if (Input.GetMouseButtonDown(1) && !isGrounded && !fastfalling)
        {
            startFastFall = true;
        }
    }

    private void FixedUpdate()
    {
        // Forward movement speed
        rb.velocity = new Vector2(MoveSpeed.value, rb.velocity.y);

        // If a jump was detected in Update, apply jump force then untoggle jump
        if (jump)
        {
            jump = false;
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }

        if (startFastFall)
        {
            startFastFall = false;
            fastfalling = true;
            stopGliding(); // Stops gliding before fastfall
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.down * JumpForce, ForceMode2D.Impulse);
        }

        if (startGlide)
        {
            startGliding();
        }
    }

    private void startSliding()
    {
        sliding = true;
        MainCollider.enabled = false;
        CrouchCollider.enabled = true;
        playerSprite.transform.localScale = new Vector3(1, 0.5f);
    }
    private void stopSliding()
    {
        sliding = false;
        MainCollider.enabled = true;
        CrouchCollider.enabled = false;
        playerSprite.transform.localScale = new Vector3(1, 1);
    }

    private void startGliding()
    {
        startGlide = false;
        gliding = true;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.gravityScale = 0.5f;
    }

    private void stopGliding()
    {
        gliding = false;
        rb.gravityScale = defaultGravity;
    }
}
