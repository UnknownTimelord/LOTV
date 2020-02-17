using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public CharacterController controller;
	
	public float speed = 12f;
	public float walkSpeed = 12f;
	public float sprintSpeed = 1.5f;
	public float gravity = -9.81f;
	public float jumpHeight = 3f;
	
	public Transform groundCheck;
	public float groundDistance = 0.4f;
	public LayerMask groundMask;
	
	Vector3 velocity;
	bool isGrounded;
	
	Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
		
		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
		
		if(isGrounded && velocity.y < 0)
		{
			velocity.y = -2f;
			if(Input.GetKey (KeyCode.LeftShift))
			{
				speed = walkSpeed * sprintSpeed;
			}
			else
			{
				speed = walkSpeed;
			}
		}
		
        float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");
		
		
		Vector3 move = transform.right * x + transform.forward * z;
		
		controller.Move(move * speed * Time.deltaTime);

		
		
		velocity.y += gravity * Time.deltaTime;
		
		controller.Move(velocity * Time.deltaTime);
		
		//Animations
		if(isGrounded && velocity.y < 0)
		{
			animator.SetInteger("WalkCon", 0);
			if(Input.GetKey (KeyCode.W))
			{
				animator.SetInteger("WalkCon", 1);
			}
			else
			{
				animator.SetInteger("WalkCon", 0);
			}
			if(Input.GetKey (KeyCode.S))
			{
				animator.SetInteger("BackwardsCon", 1);
			}
			else
			{
				animator.SetInteger("BackwardsCon", 0);
			}
			if(Input.GetKey (KeyCode.LeftShift))
			{
				if(Input.GetKey (KeyCode.W))
				{
				animator.SetInteger("WalkCon", 0);
				animator.SetInteger("RunningCon", 1);
				}
				else
				{
				animator.SetInteger("RunningCon", 0);
				}
			}
			else
			{
				animator.SetInteger("RunningCon", 0);
			}
		}
    }
}
