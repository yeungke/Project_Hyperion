using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedAttack : MonoBehaviour
{
    // Retrieves the PlayerMovement script to determine if the player is crouching
    public PlayerMovement movementScript;

    // Basic bullet variables
    [SerializeField] private GameObject bullet;
    [SerializeField] private KeyCode key = KeyCode.L;
    [SerializeField] private Animator animator;

    // Sync the timing of the animation with the projectile; set a weapon cooldown (to prevent spamming)
    [SerializeField] private float attackBuffer = 0f;
    [SerializeField] private float cooldownTimer = 0;
    [SerializeField] private float attackTimer = 0.75f;

    // Charged bullet variables
    [Header("Charged Bullet Variables")]
    [Space(10)]
    [SerializeField] private GameObject chargedBullet;
    [SerializeField] private float chargeSpeed = 2f;
    [SerializeField] private float chargeTimer = 2.5f;
    [SerializeField] private float chargeTime;
    private bool isCharging;

    // Raycast to test for bullet travel distance
    [Header("Ray Cast Debugging Tools")]
    [Space(10)]
    [SerializeField] private Vector2 vectorDirection;
    [SerializeField] private Color colour;
    [SerializeField] private float distance;

    // Allows the player to fire a ranged attack; queues the animation and adds to the cooldown
    private void FireWeapon()
    {
        // Holding down the shoot key while not crouching will charge the bullet
        if (Input.GetKey(key) && chargeTime < chargeTimer && !movementScript.GetCrouching())
        {
            isCharging = true;
            if (isCharging == true)
            {
                chargeTime += Time.deltaTime * chargeSpeed;
            }
        }

        // If the player has the basic Gun upgrade and the player is not crouching...
        if ((UpgradeManager.instance.IsEnabled(Upgrades.ATTACKGUN)) && !movementScript.GetCrouching())
        {
            // Fire a basic bullet upon pressing down the key, and the cooldown timer is at 0
            if (Input.GetKeyDown(key) && cooldownTimer == 0)
            {
                Invoke("LaunchProjectile", attackBuffer);
                cooldownTimer += attackTimer;
                chargeTime = 0; // reset charge time after firing a basic shot
            }
            // If the player has the Charged Shot upgrade and the shoot button is released...
            else if (UpgradeManager.instance.IsEnabled(Upgrades.ATTACKGUNCHARGE) && Input.GetKeyUp(key))
            {
                // Launch a charged bullet if the player has held the shoot key for long enough
                if (chargeTime >= chargeTimer)
                {
                    LaunchChargedShot();
                    cooldownTimer += attackTimer;
                }
                // Otherwise, fire a normal bullet if the cooldown is at 0
                else if (chargeTime < chargeTimer && cooldownTimer == 0)
                {
                    Invoke("LaunchProjectile", attackBuffer);
                    cooldownTimer += attackTimer;
                    chargeTime = 0; // reset charge time after firing a basic shot
                }
            }
        }
    }

    private void LaunchChargedShot()
    {
        Instantiate(chargedBullet, transform.position, transform.rotation);
        isCharging = false;
        chargeTime = 0;
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

    public void BasicRayCast()
    {
        Vector2 vStart = transform.position;
        Vector2 vEnd = vStart + distance * vectorDirection;
        Debug.DrawLine(vStart, vEnd, colour);
    }

    // Update is called once per frame
    void Update()
    {
        FireWeapon();
        WeaponCooldown();
        BasicRayCast();
    }
}
