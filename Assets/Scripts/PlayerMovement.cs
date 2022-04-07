using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;

    [SerializeField] private bool canJump = false;
    [SerializeField] private bool canCrouch = false;


    void GetUserInput()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump") && canJump == true)
            jump = true;

        if (Input.GetButtonDown("Crouch") && canCrouch == true)
            crouch = true;
        else if (Input.GetButtonUp("Crouch"))
            crouch = false;
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
