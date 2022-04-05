using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int enemyHealth = 5;

    public float moveSpeed;

    public Transform leftPoint, rightPoint;

    private bool movingRight;

    private Rigidbody2D rb;

    public SpriteRenderer sr;

    private Animator anim;

    public float moveTime, waitTime;
    private float moveCount, waitCount;

    private float nextActionTime = 0.0f;
    public float period = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        leftPoint.parent = null;
        rightPoint.parent = null;

        movingRight = true;

        moveCount = moveTime;
    }
    
    void EnemyMovementUpdate()
    {

        if (moveCount > 0)
        {
            moveCount -= Time.deltaTime;
            

            if (movingRight)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                sr.flipX = true;

                if (transform.position.x > rightPoint.position.x)
                {
                    movingRight = false;
                }
            }
            else
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                sr.flipX = false;

                if (transform.position.x < leftPoint.position.x)
                {
                    movingRight = true;
                }
            }

            if (moveCount <= 0)
            {
                waitCount = Random.Range(waitTime * 0.75f, waitTime * 1.25f);
            }

            anim.SetBool("isMoving", true);

            if (Time.time > nextActionTime)
            {
                nextActionTime += period;
                int random = Random.Range(0, 10);
                if (random == 1)
                    anim.SetTrigger("jumping");
            }

        }
        else if (waitCount > 0)
        {
            waitCount -= Time.deltaTime;
            rb.velocity = new Vector2(0f, rb.velocity.y);

            if (waitCount <= 0)
            {
                moveCount = Random.Range(moveTime * 0.75f, moveTime * 1.25f);
            }

            anim.SetBool("isMoving", false);
        }
    }

    void EnemyAttack()
    {

    }

    void EnemyKilled()
    {
        if (enemyHealth == 0)
        {
            anim.SetTrigger("killed");
            this.transform.parent.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovementUpdate();
    }
}
