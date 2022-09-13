using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBullet : MonoBehaviour
{
    [SerializeField] private float speed = 15f; // speed of the projectile
    [SerializeField] private Rigidbody2D rb;

    // Colliding with an enemy or boss destroys the bullet, conditional on the distance travelled and number of enemies hit
    public void OnCollisionEnter(Collision collision)
    {
        
    }

    // Method called to destroy the object
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    private void MoveForward()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
    }

    // Start function is called upon initialization
    private void Start()
    {
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        //MoveForward();
    }
}
