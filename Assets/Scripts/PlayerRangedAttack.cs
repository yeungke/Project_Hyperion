using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedAttack : MonoBehaviour
{
    // Retrieves the PlayerMovement script to determine if the player is crouching
    public PlayerMovement movementScript;

    [SerializeField] private GameObject bullet;
    [SerializeField] private KeyCode key = KeyCode.L;
    [SerializeField] private Animator animator;

    // Sync the timing of the animation with the projectile; set a weapon cooldown (to prevent spamming)
    [SerializeField] private float attackBuffer = 0.2f;
    [SerializeField] private float cooldownTimer = 0;
    [SerializeField] private float attackTimer = 1.0f;

    // Allows the player to fire a ranged attack; queues the animation and adds to the cooldown
    private void FireWeapon()
    {
        if (Input.GetKeyDown(key) == true && cooldownTimer == 0 && UpgradeManager.instance.GetAttackGun() == true &&
            movementScript.GetCrouching() == false)
        {
            Invoke("LaunchProjectile", attackBuffer);
            cooldownTimer += attackTimer;
        }
    }

    // Fires a projectile according to the currently equipped weapon
    private void LaunchProjectile()
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }

    private void WeaponCooldown()
    {
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;
        else
            cooldownTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        FireWeapon();
        WeaponCooldown();
    }
}
