using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Animator animator;
    public int speed;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start ()
    {
        if (animator == null)
            Debug.Log("PlayerScript: animator not found.");

        speed = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        FindMovement();
    }

    private void FindMovement()
    {
        // Find if player is holding shift
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.LeftShift))
            animator.SetBool("sprint", true);
        else if (animator.GetBool("sprint"))
            animator.SetBool("sprint", false);

        // Find if player is going forward or backwards
        if (Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("back", false);
            animator.SetBool("fwd", true);
        }
        else if (animator.GetBool("fwd"))
            animator.SetBool("fwd", false);
        else if (Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.S))
        {
            animator.SetBool("fwd", false);
            animator.SetBool("back", true);
        }
        else if (animator.GetBool("back"))
            animator.SetBool("back", false);

        // Find if player is going left or right
        if (Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.D))
        {
            animator.SetBool("left", false);
            animator.SetBool("right", true);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKeyDown(KeyCode.A))
        {
            animator.SetBool("right", false);
            animator.SetBool("left", true);
        }
        else if (animator.GetBool("right") || animator.GetBool("left"))
        {
            animator.SetBool("right", false);
            animator.SetBool("left", false);
        }
    }
}
