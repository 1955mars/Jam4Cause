using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student : MonoBehaviour
{
    public Vector2 jumpVector = new Vector2(0.0f, 20.0f);
    public float maxJumpTime = 0.5f;
    public float groundCheckDistance = 0.1f;
    public LayerMask ground;

    private Rigidbody2D body;
    private Transform groundChecker;
    private bool isGrounded;
    private bool jumping = false;

    public bool playingMacro = false;
    public FrameData currentFrame;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        groundChecker = transform.GetChild(0);
        FindObjectOfType<MacroController>().student = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (playingMacro)
            Debug.Log("PLAYING");

        if (Physics2D.OverlapCircle(groundChecker.position, groundCheckDistance, ground) != null)
            isGrounded = true;
        else
            isGrounded = false;
    }

    private void FixedUpdate()
    {
        if (currentFrame.jumpPressed && !jumping && isGrounded && playingMacro)
        {
            jumping = true;
            StartCoroutine(Jump());
        }
    }

    IEnumerator Jump()
    {
        body.velocity = Vector2.zero;
        float timer = 0.0f;

        Debug.Log(currentFrame.jumpPressed);

        while (currentFrame.jumpPressed && timer < maxJumpTime && playingMacro)
        //while (timer < maxJumpTime && playingMacro)
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
