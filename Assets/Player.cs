using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Ground
    public float gravity;
    public Vector2 velocity;
    public float ground = -1;
    public bool isGrounded = false;

    //Vertical 
    public float jumpVelocity = 20;

    //Horizontal
    public float acceleration = 2;
    public float maxAcceleration = 20;
    public float maxHVelocity = 100;

    //Animator
    public Animator animator;

    private Vector2 Jump()
    {
        //Jump Script
        Vector2 pos = transform.position;

        if (!isGrounded)
        {
            pos.y += velocity.y * Time.fixedDeltaTime;
            velocity.y += gravity * Time.fixedDeltaTime;

            if (pos.y <= ground)
            {
                pos.y = ground;
                isGrounded = true;
            }

        }

        transform.position = pos;
        return pos;

    }

    private Vector2 Run()
    {
        //Running Script
        if (isGrounded)
        {

            //Running acceleration script
            float velocityRatio = velocity.x / maxHVelocity;
            acceleration = maxAcceleration * (1.0f - velocityRatio);

            velocity.x += acceleration * Time.fixedDeltaTime;

            //Max speed
            if (velocity.x >= maxHVelocity)
            {
                velocity.x = maxHVelocity;
            }
        }
        animator.SetFloat("Speed", Mathf.Abs(velocity.x));
        return velocity;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGrounded)
        {
            //Animator Script for Jump
            animator.SetBool("isJumping", true);
            return;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
           isGrounded = false;
           velocity.y = jumpVelocity;


        }
        if (isGrounded)
        {
            animator.SetBool("isJumping", false);
        }

    }

    private void FixedUpdate()
    {
        Jump();
        Run();

    }

}
