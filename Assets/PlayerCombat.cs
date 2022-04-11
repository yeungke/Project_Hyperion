using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public float attackRange = 0.5f;
    public float attackRate = 2f;
    public LayerMask enemyLayers;

    [SerializeField] private KeyCode attackKey = KeyCode.K;
    [SerializeField] private bool canAttack = false;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackCooldown = 0f;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        UpgradeBlock upgrade = collision.gameObject.GetComponent<UpgradeBlock>();

        if (upgrade != null)
            canAttack = true;
    }

    private void GetUserInput()
    {
        if (Time.time >= attackCooldown)
        {
            if (Input.GetKeyDown(attackKey) && canAttack == true)
            {
                MeleeAttack();
                attackCooldown = Time.time + 1f / attackRate;
            }
        }
    }

    private void MeleeAttack()
    {
        // Play the attack animation
        animator.SetTrigger("AttackTrigger");

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Deal damage to enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log(enemy.name + "was hit!");
        }
    }

    void Update()
    {
        GetUserInput();
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
