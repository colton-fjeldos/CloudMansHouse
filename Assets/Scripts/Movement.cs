//Movement.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	private Rigidbody2D body;
    private BoxCollider2D coll;
    private Animator anim;
    private TrailRenderer tr;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    private bool grounded;
    private bool doubleJump;

    private bool canDash = true;
    private bool isDashing = false;
    [SerializeField] private float dashingPower = 24f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;


    private enum MovementState { idle, running, jumping, dashing}
    private MovementState state = MovementState.idle;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpSpeed = 9f;




	
    // Start is called before the first frame update
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        tr = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isDashing)
        {
            anim.SetInteger("state", (int) MovementState.dashing);
            return;
        }
		dirX = Input.GetAxisRaw("Horizontal");
        grounded = Grounded();

        
        body.velocity = new Vector2(dirX * movementSpeed, body.velocity.y);

        

        
        

        if (grounded && !Input.GetButton("Jump"))
        {
            doubleJump = false;
        }
        if (Input.GetKeyDown(KeyCode.E) && canDash)
        {
            StartCoroutine(Dash());
        }
        
        if (Input.GetButtonDown("Jump"))
		{

            if (grounded || doubleJump)
            {
                body.velocity = new Vector2(body.velocity.x, jumpSpeed);
                doubleJump = !doubleJump;
                
            }
        }

        if (Input.GetButtonUp("Jump") && body.velocity.y > 0f) 
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y * 0.5f);
        }

        UpdateAnimations();
    }

    private bool Grounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void UpdateAnimations()
    {
        MovementState state;

        if (dirX != 0)
        {
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }

        if (body.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        if (body.velocity.y < -0.1f)
        {   
            state = MovementState.idle;
        }

        anim.SetInteger("state", (int) state);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        tr.emitting = true;

        float originalGravity = body.gravityScale;
        body.gravityScale = 0;
        body.velocity = new Vector2(dirX * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        body.gravityScale = originalGravity;
        tr.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
