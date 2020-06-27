using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO.MemoryMappedFiles;

//This Script is intended for demoing and testing animations only.


public class bearController : MonoBehaviour {

	private float HSpeed = 10f;
	//private float maxVertHSpeed = 20f;
	private bool facingRight = true;
	private float moveXInput;

    //Used for flipping Character Direction
	public static Vector3 theScale;

	//Jumping Stuff
	public Transform groundCheck;
	public LayerMask whatIsGround;
	private bool grounded = false;
	private float groundRadius = 0.15f;
	private float jumpForce = 14f;

	private Animator anim;

    private float screenWidth = Screen.width;
    private float screenHeight = Screen.height;
    private bool right = true;

	// Use this for initialization
	void Awake ()
	{
//		startTime = Time.time;
		anim = GetComponent<Animator> ();
	}

	void FixedUpdate ()
	{

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("ground", grounded);


	}

	void Update()
	{
        moveXInput = Input.GetAxis("Horizontal");

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            touchPos.z = 0;

            //move left
            if (touch.position.x < screenWidth / 2 && touch.phase == TouchPhase.Began)
            {
                right = false;
                moveXInput = -1;
            }

            //move right
            if (touch.position.x > screenWidth / 2 && touch.phase == TouchPhase.Began)
            {
                right = true;
                moveXInput = 1;
            }

            //jump
            if (touch.position.y > screenHeight / 2 && touch.phase == TouchPhase.Began && grounded)
            {
                anim.SetBool("ground", false);

                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.y, jumpForce);
            }
        }
        else
        {
            if (right)
            {
                moveXInput = 1;
            }
            else
            {
                moveXInput = -1;
            }
        }

        if ((grounded) && Input.GetButtonDown("Jump"))
        {
            anim.SetBool("ground", false);

            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.y, jumpForce);
        }


        anim.SetFloat("HSpeed", Mathf.Abs(moveXInput));
        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);


        GetComponent<Rigidbody2D>().velocity = new Vector2((moveXInput * HSpeed), GetComponent<Rigidbody2D>().velocity.y);

        if (Input.GetButtonDown("Fire1") && (grounded)) { anim.SetTrigger("Punch"); }

        if (Input.GetButton("Fire2"))
        {
            anim.SetBool("Sprint", true);
            HSpeed = 14f;
        }
        else
        {
            anim.SetBool("Sprint", false);
            HSpeed = 10f;
        }

        //Flipping direction character is facing based on players Input
        if (moveXInput > 0 && !facingRight)
            Flip();
        else if (moveXInput < 0 && facingRight)
            Flip();
    }
    ////Flipping direction of character
    void Flip()
	{
		facingRight = !facingRight;
		theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}
