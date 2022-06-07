using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // External forces
    public BoolSO Paused;
    public BoolSO GameOver;
    public BoolSO HoldToggle; // If true, clicking will toggle the hold state

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
    
    // Particles
    [Space]
    public ParticleSystem JumpPS;
    public ParticleSystem SlidePS;
    public ParticleSystem GlidePS;

    // Tracking player state
    private bool isGrounded;
    private bool wasGrounded; // whether it was grounded the frame before

    private bool jump;
    private float justJumped;

    private bool sliding;

    private bool startFastFall;
    private bool fastfalling;

    private bool startGlide;
    private bool gliding;
    private bool alreadyGlided;

    // Hold toggle variables
    [SerializeField]
    private bool LclickHold;
    [SerializeField]
    private bool RclickHold;

    //jmpbffr
    private float jumpBufferCounter;
    public float jumpBufferTime;

    //cyt time
    private float coyoteTimeRemaining;
    public float coyoteTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        defaultGravity = rb.gravityScale;
        stopSliding();
        //Time.timeScale = 0.05f;
    }

    // Update is called once per frame
    // TODO: Generalize controls if it needs to be used on mobile too
    void Update()
    {
        if (Paused.value || GameOver.value) return;

        // Raycast down to see if player is grounded, set variable
        CapsuleCollider2D activeCollider = sliding ? CrouchCollider : MainCollider;
        RaycastHit2D raycast = Physics2D.Raycast(activeCollider.bounds.center, Vector2.down, activeCollider.bounds.extents.y + 0.1f, GroundMask);
        isGrounded = raycast.collider != null;

        // Holdtoggle info!
        if (HoldToggle.value)
        {
            // Start "holding" the lclick if it is pressed while not grounded
            if (!isGrounded && Input.GetButtonDown("Jump")) //changed to univeral jump to support multiple buttons.
            {
                LclickHold = !LclickHold;
            }

            // Cancel jump hold on landing
            if (!wasGrounded && isGrounded)
            {
                LclickHold = false;
            }

            // Start "holding" rclick if pressed on ground
            if (isGrounded && Input.GetButtonDown("Slide")) //changed to univeral jump to support multiple buttons.
            {
                RclickHold = !RclickHold;
            }

            // Cancel slide hold on jump
            if (!isGrounded)
            {
                RclickHold = false;
            }
        }

        AC.SetFloat("VelocityY", rb.velocity.y);

        // Logic when player just landed
        if (!wasGrounded && isGrounded)
        {
            JumpPS.Play();
            alreadyGlided = false;
            AC.SetBool("Jump", false);
            if (gliding)
            {
                stopGliding();
            }
        }

        // If player just left the ground, start coyoteTime
        if (justJumped <= 0 && wasGrounded && !isGrounded)
        {
            coyoteTimeRemaining = coyoteTime;
        }

        if (coyoteTimeRemaining > 0)
        {
            coyoteTimeRemaining -= Time.deltaTime;
        }

        // Cancel fastfalling if player lands
        if (isGrounded && fastfalling)
        {
            fastfalling = false;
        }

        //if jump set counter else count down
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (justJumped >= 0)
        {
            justJumped -= Time.deltaTime;
        }

        // Check to see if player pressed a jump key
        // Allow if: Pressed jump, grounded
        if (jumpBufferCounter > 0f && (isGrounded || coyoteTimeRemaining > 0f))
        {
            AudioManager.instance.Play("Jump");
            AC.SetBool("Jump", true);

            jump = true;
            justJumped = coyoteTime;
            stopSliding();
            jumpBufferCounter = 0;
            coyoteTimeRemaining = 0;
        }


        // Check to see if player wants to glide
        // Allow if: Holding jump, not fastfalling grounded or gliding, and is descending
        if (((!HoldToggle.value && Input.GetButton("Jump")) || (HoldToggle.value && LclickHold)) && 
            coyoteTimeRemaining <= 0 && !jump && !fastfalling && !isGrounded && !gliding && rb.velocity.y <= 0)
        {
            AudioManager.instance.PlayStoppableTrack("Float");
            startGlide = true;
            AC.SetBool("Gliding", true);
            GlidePS.Play();
        } else if (gliding && ((!HoldToggle.value && !Input.GetButton("Jump")) || (HoldToggle.value && !LclickHold)))
        {
            stopGliding();
            
        }

        // Check to see if player wants to slide, or stop sliding
        // Allow if: Holding slide, not already sliding or jumping, is grounded
        if (((!HoldToggle.value && Input.GetButton("Slide")) || (HoldToggle.value && RclickHold)) && 
            isGrounded && !jump && !sliding)
        {
            AudioManager.instance.PlayStoppableTrack("Slide");
            AC.SetBool("Sliding", true);
            startSliding();

        } else if (((!HoldToggle.value && !Input.GetButton("Slide")) || (HoldToggle.value && !RclickHold)) && sliding)
        {
            stopSliding();
        }

        // Check to see if player wants to fastfall
        // Allow if: Pressed slide, not grounded, not fastfalling already
        if (Input.GetButtonDown("Slide") && !isGrounded && !fastfalling)
        {
            AudioManager.instance.Play("Fall");
            startFastFall = true;
        }

        wasGrounded = isGrounded;
    }

    private void FixedUpdate()
    {
        // Forward movement speed
        rb.velocity = new Vector2(MoveSpeed.value, rb.velocity.y);

        // If a jump was detected in Update, apply jump force then untoggle jump
        if (jump)
        {
            jump = false;
            rb.velocity = new Vector2(rb.velocity.x, 0);
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
            startGlide = false;
            gliding = true;
            // Half your y-velocity only once per jump so it can't be spammed
            if (!alreadyGlided)
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 4);
            rb.gravityScale = 0.5f;
            alreadyGlided = true;
        }
    }

    private void startSliding()
    {
        SlidePS.Play();
        AC.SetBool("Sliding", true);
        sliding = true;
        MainCollider.enabled = false;
        CrouchCollider.enabled = true;
        //playerSprite.transform.localScale = new Vector3(1, 0.5f);
    }
    private void stopSliding()
    {
        SlidePS.Stop();
        AudioManager.instance.FadeOutStoppableTrack(0.25f);
        AC.SetBool("Sliding", false);
        sliding = false;
        MainCollider.enabled = true;
        CrouchCollider.enabled = false;
        playerSprite.transform.localScale = new Vector3(1, 1);
    }

    private void stopGliding()
    {
        //Debug.Log("Stop gliding");
        GlidePS.Stop();
        AudioManager.instance.FadeOutStoppableTrack(0.25f);
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
