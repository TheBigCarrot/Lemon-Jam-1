using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    public float moveSpeed;
    public float maxSpeed;
    public float jumpForce;
    private bool canJump;
    private bool canDoubleJump;
    private float H_Input;
    public LayerMask canJumpOn;
    
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (transform.position.y < -10)
        {
            transform.position = new Vector3(0, -3.25f, 0);
            rb.velocity = new Vector2(0, 0);
        }

        if (Physics2D.Raycast(transform.position + new Vector3(0, -0.75f, 0), Vector2.down, 0.1f, canJumpOn))
        {
            canJump = true;
            canDoubleJump = true;
        } else
        {
            canJump = false;
        }
        
        
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector2(H_Input * moveSpeed, 0f), ForceMode2D.Impulse);
        Debug.Log(H_Input);
    }

    public void MoveRight(InputAction.CallbackContext context)
    {
        if (rb.velocity.x < maxSpeed)
        {
            if (context.performed)
            {
                H_Input = 1f;
            }
            else if (context.canceled)
            {
                H_Input = 0f;
            }
        }
    }

    public void MoveLeft(InputAction.CallbackContext context)
    {
        if (rb.velocity.x > -maxSpeed)
        {
            if (context.performed)
            {
                H_Input = -1f;
            }
            else if (context.canceled)
            {
                H_Input = 0f;
            }
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (canJump)
            {
                rb.AddForce(new Vector2(0, jumpForce - rb.velocity.y), ForceMode2D.Impulse);
                canJump = false;
            }
            else if (canDoubleJump)
            {
                rb.AddForce(new Vector2(0, jumpForce - rb.velocity.y), ForceMode2D.Impulse);
                canDoubleJump = false;
            }
        }
    }
}
