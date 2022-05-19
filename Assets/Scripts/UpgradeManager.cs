using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    // Initialize single instance of UpgradeManager
    public static UpgradeManager instance;

    // Values that determine the player's upgrades
    [SerializeField] private bool jump;
    [SerializeField] private bool crouch;
    [SerializeField] private bool crouchJump;
    [SerializeField] private bool crouchAir;
    [SerializeField] private bool attackSword;

    // Start is called before the first frame update
    void Start()
    {
        // Singleton pattern instance
        if (instance == null)
            instance = this;
    }

    // Jump accessor and mutator
    public bool GetJump() => jump;
    public void SetJump(bool jumpBool) => jump = jumpBool;

    // Crouch accessor and mutator
    public bool GetCrouch() => crouch;
    public void SetCrouch(bool crouchBool) => crouch = crouchBool;

    // Crouch Jump accessor and mutator
    public bool GetCrouchJump() => crouchJump;
    public void SetCrouchJump(bool crJumpBool) => crouchJump = crJumpBool;

    // Crouch in Air accessor and mutator
    public bool GetCrouchAir() => crouchAir;
    public void SetCrouchAir(bool crAirBool) => crouchAir = crAirBool;

    // Sword Attack accessor and mutator
    public bool GetAttackSword() => attackSword;
    public void SetAttackSword(bool swordBool) => attackSword = swordBool;

    // Update is called once per frame
    void Update()
    {
        
    }
}
