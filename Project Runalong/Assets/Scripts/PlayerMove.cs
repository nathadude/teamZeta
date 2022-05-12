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
    public CapsuleCollider2D MainCollider;
    public CapsuleCollider2D CrouchCollider;
    public Animator AC;

    // Tracking player state
    [SerializeField]
    private bool isGrounded;
    private bool jump;
    private float justJumped;
    [SerializeField]
    private bool sliding;
    private bool startFastFall;
    [SerializeField]
    private bool fastfalling;
    private bool startGlide;
    [SerializeField]
    private bool gliding;

    //jmpbffr
    private float jumpBufferCounter;
    public float jumpBufferTime;

    //cyt time
    private float coyoteTimeCounter;
    public float coyoteTime;

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
        CapsuleCollider2D activeCollider = sliding ? CrouchCollider : MainCollider;
        RaycastHit2D raycast = Physics2D.Raycast(activeCollider.bounds.center, Vector2.down, activeCollider.bounds.extents.y + 0.1f, GroundMask);
        isGrounded = raycast.collider != null;


        AC.SetFloat("VelocityY", rb.velocity.y);

        // Cancel fastfalling if player lands
        if (isGrounded && fastfalling)
        {
            fastfalling = false;
        }

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        //if jump set counter else count down
        if (Input.GetMouseButtonDown(0))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        // Check to see if player pressed a jump key
        // Allow if: Pressed jump, grounded
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            jump = true;
            stopSliding();
            //coyoteTimeCounter = 0f;
        }


        // Check to see if player wants to glide
        // Allow if: Holding jump, not fastfalling grounded or gliding, and is descending
        if (Input.GetMouseButton(0) && !(coyoteTimeCounter > 0) && !(jumpBufferCounter > 0) && !fastfalling && !isGrounded && !gliding && rb.velocity.y <= 0)
        {
            startGlide = true;
            AC.SetBool("Gliding", true);
        } else if (gliding && !Input.GetMouseButton(0))
        {
            stopGliding();
            
        }

        // Check to see if player wants to slide, or stop sliding
        // Allow if: Holding slide, not already sliding or jumping, is grounded
        if (Input.GetMouseButton(1) && isGrounded && !jump && !sliding)
        {
            AC.SetBool("Sliding", true);
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
            coyoteTimeCounter = 0f;
            jumpBufferCounter = 0f;
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
            startGlide = false;
            gliding = true;
            //rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.gravityScale = 0.5f;
        }
    }

    private void startSliding()
    {
        AC.SetBool("Sliding", true);
        sliding = true;
        MainCollider.enabled = false;
        CrouchCollider.enabled = true;
        playerSprite.transform.localScale = new Vector3(1, 0.5f);
    }
    private void stopSliding()
    {
        AC.SetBool("Sliding", false);
        sliding = false;
        MainCollider.enabled = true;
        CrouchCollider.enabled = false;
        playerSprite.transform.localScale = new Vector3(1, 1);
    }

    private void stopGliding()
    {
        AC.SetBool("Gliding", false);
        gliding = false;
        rb.gravityScale = defaultGravity;
    }

/*    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && justJumped <= 0)
        {
            Debug.Log("Landed on ground");
            AC.SetBool("Jumping", false);
        }
    }*/
}
