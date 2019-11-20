using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student : MonoBehaviour
{
    public float jumpForce = 5;
    public float moveMultiplier = 5f;
    public float maxJumpTime = 0.3f;
    public float groundCheckDistance = 0.1f;
    public LayerMask ground;

    public Rigidbody2D body;
    private Transform groundChecker;
    private bool isGrounded;
    private bool jumping = false;
    private float jumpTimeCounter = 0f;

    public bool playingMacro = false;
    public FrameData currentFrame;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        groundChecker = transform.GetChild(0);
        FindObjectOfType<MacroController>().student = this;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.UI.Text uiText = GameObject.Find("Text").GetComponent<UnityEngine.UI.Text>();
        uiText.text = "PRESS 1, 2, OR 3 TO EXECUTE YOUR TEACHINGS";

        if (playingMacro)
            //Debug.Log("PLAYING");




        if (Physics2D.OverlapCircle(groundChecker.position, groundCheckDistance, ground) != null)
            isGrounded = true;
        else
            isGrounded = false;

        if (!playingMacro)
        {
            body.velocity = new Vector2(moveMultiplier / 2.0f, body.velocity.y);
        }

        if (playingMacro && isGrounded && currentFrame.jumpPressed)
        {
            jumping = true;
            jumpTimeCounter = maxJumpTime;
            body.velocity = Vector2.up * jumpForce;
            SoundScript.PlayTune("JumpLong");
        }

        if (playingMacro && currentFrame.jumpPressed && jumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                body.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                jumping = false;
            }
        }

        if (!playingMacro || !currentFrame.jumpPressed)
        {
            jumping = false;
        }

        animator.SetFloat("Velocity", body.velocity.x);
    }

    private void FixedUpdate()
    {
        if (playingMacro && currentFrame.rightPressed)
            body.velocity = new Vector2(moveMultiplier, body.velocity.y);

        if (playingMacro && currentFrame.leftPressed)
            body.velocity = new Vector2(-moveMultiplier, body.velocity.y);
    }
}
