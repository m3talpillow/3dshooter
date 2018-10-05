using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;

    // Values for movement
    private const float crouchSpeed = 2f;
    private const float runSpeed = 4.5f;
    private const float sprintSpeed = 7;
    public float moveSpeed;
    public float rotateSpeed;

    public float jumpSpeed;
    public float jumpTime;
    private bool inAir;
    private float jumpTimer;

    public float verticalInput;
    public float horizontalInput;

    // Find necessary components
    private void Awake()
    {
        animator = this.GetComponent<Animator>();
    }

    void Start ()
    {
        moveSpeed = runSpeed;
        rotateSpeed = 150f;
        jumpSpeed = 5f;
        jumpTime = 0.2f;
        inAir = false;
        jumpTimer = 0f;
        animator.SetBool("inAir", false);
	}
	
	void Update ()
    {
        FindMovement();
	}

    // Get keyboard inputs and modify animator values
    private void FindMovement()
    {
        // Rotate character same speed at camera rotates
        transform.Rotate(0, Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime, 0);

        // Player speed input
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.SetTrigger("cont");
            moveSpeed = crouchSpeed;
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetTrigger("shift");
            moveSpeed = sprintSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            animator.SetTrigger("cont");
            moveSpeed = runSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetTrigger("shift");
            moveSpeed = runSpeed;
        }

        // Player direction input
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        if (verticalInput != 0)
        {
            animator.SetFloat("vert", verticalInput);
            transform.Translate(verticalInput * Vector3.forward * moveSpeed * Time.deltaTime);
        }

        if (horizontalInput != 0)
        {
            animator.SetFloat("horz", horizontalInput);
            transform.Translate(horizontalInput * Vector3.right * moveSpeed * Time.deltaTime);
        }

        // Handles jumping (needs rework - implement environment objects in scene)
        if (Input.GetKeyDown(KeyCode.Space) && !inAir)
        {
            inAir = true;
            animator.SetBool("inAir", true);
            jumpTimer = 0f;
            transform.Translate(Vector3.up * jumpSpeed * Time.deltaTime);
        }
        else if (inAir)
        {
            transform.Translate(Vector3.up * jumpSpeed * Time.deltaTime);

            if (jumpTimer >= jumpTime)
            {
                inAir = false;
                animator.SetBool("inAir", false);
            }
            else
                jumpTimer += Time.deltaTime;
        }
        else
        {
            RaycastHit hit;
            float yDifference = (Vector3.down * jumpSpeed * Time.deltaTime).y + 0.01f;
            Vector3 attempt = new Vector3(transform.position.x, transform.position.y + yDifference, transform.position.z);

            // If there is no hit, move character down
            if (!Physics.Linecast(transform.position, attempt, out hit))
            {
                transform.Translate(Vector3.down * jumpSpeed * Time.deltaTime);
            }
            else
                Debug.Log("trasPos: " + transform.position.ToString() + ", ExPos: " + attempt.ToString());
        }
    }
}
