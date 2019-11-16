using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 velocity;
    float force = 0.05f;
    float moveMultiplier = 5f;
    float jumpHeight = 5f;

    Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        else
        {
            velocity.x = Input.GetAxis("Horizontal");
        }
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(velocity.x * moveMultiplier, rigidbody.velocity.y);
        //rigidbody.velocity = Vector2.zero;
    }

    void Jump()
    {
        velocity.y = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
        rigidbody.velocity = new Vector2(velocity.x, velocity.y);
        velocity.y += Physics2D.gravity.y * Time.deltaTime;
        rigidbody.velocity = new Vector2(velocity.x, velocity.y);
    }
}
