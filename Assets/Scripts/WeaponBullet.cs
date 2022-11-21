using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBullet : MonoBehaviour
{
    [SerializeField] private float speed = 8f; // speed of the projectile
    [SerializeField] private int damage = 1; // damage dealt to enemies
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float maxDistance;
    [SerializeField] private float objectDistance;


    // Colliding with an enemy or boss destroys the bullet, conditional on the distance travelled and number of enemies hit
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Prints the name of the Object the bullet collides with, for debugging purposes
        Debug.Log(collision.name);

        // Detect enemy object
        EnemyController enemy = collision.GetComponent<EnemyController>();
        if (enemy != null)
        {
            // if the player doesn't have piercing shot during enemy collision, destroy the bullet
            if (UpgradeManager.instance.IsEnabled(Upgrades.ATTACKGUNPIERCE))
                DestroyObject();
            enemy.TakeDamage(damage); // enemy takes damage upon collision with bullet
        }

        FlyingEnemy fe = collision.GetComponent<FlyingEnemy>();
        if (fe != null)
        {
            fe.TakeDamage(damage);
            DestroyObject();

        }

        BrainContainer bc = collision.GetComponent<BrainContainer>();
        if (bc != null)
        {
            bc.TakeDamage(damage);
            DestroyObject();
        }

        // Detect terrain layer
        bool terrain = collision.gameObject.layer == LayerMask.NameToLayer("Terrain");
        //TerrainLayer terrain = collision.GetComponent<TerrainLayer>();
        // If the player doesn't have x-ray shot, destroy the bullet
        if (terrain == true && UpgradeManager.instance.IsEnabled(Upgrades.ATTACKGUNXRAY))
        {
            DestroyObject();
        }
    }

    // Method called to destroy the object
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    private void MoveForward()
    {
        // A reusable variable to keep track of how far the bullet has travelled, using time and speed
        float travel = Time.deltaTime * speed;

        transform.Translate(Vector2.right * travel); // Bullet moves in the direction of the shot (right by default)
        objectDistance += travel; // Update the object's distance travelled

        // Once the bullet has reached its max distance, the bullet is destroyed
        if (objectDistance > maxDistance)
            DestroyObject();
    }

    // Start function is called upon initialization
    private void Start()
    {
        objectDistance = 0;
        rb.velocity = transform.right * speed;

        // If the player has the Long Gun Attack upgrade, increase the range of the bullet
        if (UpgradeManager.instance.IsEnabled(Upgrades.ATTACKGUNLONG))
            maxDistance = 5f;
        else
            maxDistance = 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
    }
}
