using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 speedVector = new Vector2(10.0f, 0.0f);
    public Vector2 jumpVector = new Vector2(0.0f, 20.0f);
    public float maxSpeed = 5.0f;
    public float maxJumpTime = 0.5f;
    public float groundCheckDistance = 0.1f;
    public LayerMask ground;

    private Rigidbody2D body;
    private Transform groundChecker;
    private bool isGrounded;
    private bool jumping = false;

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
            isGrounded = true;
        else
            isGrounded = false;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !jumping && isGrounded)
        {
            jumping = true;
            StartCoroutine(Jump());
        }
    }

    IEnumerator Jump()
    {
        body.velocity = Vector2.zero;
        float timer = 0.0f;

        while (Input.GetKey(KeyCode.Space) && timer < maxJumpTime)
        {
            float jumpProgress = timer / maxJumpTime;
            Vector2 currentJumpVector = Vector2.Lerp(jumpVector, Vector2.zero, jumpProgress);
            body.AddForce(currentJumpVector);
            timer += Time.deltaTime;
            yield return null;
        }

        jumping = false;
    }
}
