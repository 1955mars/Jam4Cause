using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float jumpForce = 5;
    public float moveMultiplier = 5f;
    public float maxJumpTime = 0.3f;
    public float groundCheckDistance = 0.2f;
    public LayerMask ground;

    private Rigidbody2D body;
    private Transform groundChecker;
    private bool isGrounded;
    private bool jumping = false;

    private float jumpTimeCounter = 0f;
    private bool isCollidinginAir = false;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        groundChecker = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapCircle(groundChecker.position, groundCheckDistance, ground) != null)
        {
            isGrounded = true;
            isCollidinginAir = false;
        }
        else
        {
            isGrounded = false;
        }

        if(isGrounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
        {
            jumping = true;
            jumpTimeCounter = maxJumpTime;
            body.velocity = Vector2.up * jumpForce;
        }

        if((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && jumping == true)
        {
            if(jumpTimeCounter > 0)
            {
                body.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                jumping = false;
            }
        }

        if(Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            jumping = false;
        }


        //if (Input.GetKeyDown(KeyCode.Space) && !jumping && isGrounded)
        //{
        //    jumping = true;
        //    StartCoroutine(Jump());
        //}
    }

    void FixedUpdate()
    {

            body.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveMultiplier, body.velocity.y);
    }

    //IEnumerator Jump()
    //{
    //    body.velocity = Vector2.zero;
    //    float timer = 0.0f;

    //    while (Input.GetKey(KeyCode.Space) && timer < maxJumpTime)
    //    {
    //        float jumpProgress = timer / maxJumpTime;
    //        Vector2 currentJumpVector = Vector2.Lerp(jumpVector, Vector2.zero, jumpProgress);
    //        body.AddForce(currentJumpVector);
    //        timer += Time.deltaTime;
    //        yield return null;
    //    }

    //    jumping = false;
    //}

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (jumping)
    //    {
    //        Debug.Log("Jumping and colliding");
    //        isCollidinginAir = true;
    //        //body.velocity= new Vector2(0, body.velocity.y);
    //    }
            
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (jumping && isCollidinginAir)
    //    {
    //        Debug.Log("Jumping and non-colliding");
    //        isCollidinginAir = false;
    //    }
            
    //}
}
