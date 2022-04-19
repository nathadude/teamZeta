using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Player variables
    public float MoveSpeed;
    public float JumpForce;
    public LayerMask GroundMask;

    // Player data
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    // Tracking player state
    [SerializeField]
    private bool isGrounded;
    private bool jump;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Raycast down to see if player is grounded, set variable
        RaycastHit2D raycast = Physics2D.Raycast(boxCollider.bounds.center, Vector2.down, boxCollider.bounds.extents.y + 0.1f, GroundMask);
        isGrounded = raycast.collider != null;

        // Check to see if player pressed a jump key
        // TODO: Generalize this if it needs to be used on mobile too
        // TODO: Player glide, Player slide, Player fastfall (?)
        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        // Forward movement speed
        rb.velocity = new Vector2(MoveSpeed, rb.velocity.y);

        // If a jump was detected in Update, apply jump force then untoggle jump
        if (jump)
        {
            jump = false;
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
    }
}
