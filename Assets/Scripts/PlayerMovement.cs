using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    private bool jump = false;
    [SerializeField] private bool crouch = false;

    void GetUserInput()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        // If the player presses the jump key, and jump is enabled in the Upgrade Manager
        if (Input.GetButtonDown("Jump") && UpgradeManager.instance.GetJump() == true)
        {
            // If the player is not crouching, the player jumps, and the jump animation plays
            if (crouch == false)
            {
                jump = true;
                animator.SetBool("IsJumping", true);
            }
            // If CrouchJump is enabled in the Upgrade Manager, the player can jump while they are crouching
            if (crouch == true && UpgradeManager.instance.GetCrouchJump() == true)
            {
                jump = true;
            }
        }

        // If the player presses the crouch key, and crouch is enabled in the Upgrade Manager
        if (Input.GetButtonDown("Crouch") && UpgradeManager.instance.GetCrouch() == true)
        {
            // The player crouches if the player is grounded
            if (Input.GetButtonDown("Crouch") && controller.m_Grounded == true)
                crouch = true;
            // If CrouchAir is enabled in the Upgrade Manager, the player can crouch when they are not grounded
            else if (controller.m_Grounded == false && UpgradeManager.instance.GetCrouchAir())
                crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    public bool GetCrouching()
    {
        return crouch;
    }

    void MoveController()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetUserInput();
    }

    private void FixedUpdate()
    {
        MoveController();
    }
}
