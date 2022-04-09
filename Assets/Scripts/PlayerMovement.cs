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
    private bool crouch = false;

    [SerializeField] private KeyCode attackKey = KeyCode.K;

    [SerializeField] private bool canJump = false;
    [SerializeField] private bool canCrouch = false;
    [SerializeField] private bool canAttack = false;


    void GetUserInput()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump") && canJump == true)
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Crouch") && canCrouch == true)
            crouch = true;
        else if (Input.GetButtonUp("Crouch"))
            crouch = false;

        if (Input.GetKeyDown(attackKey))
        {
            animator.SetTrigger("AttackTrigger");
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

    public void SetJump(bool val)
    {
        canJump = val;
    }

    public void SetCrouch(bool val)
    {
        canCrouch = val;
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
