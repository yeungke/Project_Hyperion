using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;

    public Transform leftPoint, rightPoint;

    private bool movingRight;

    private Rigidbody2D rb;

    public SpriteRenderer sr;

    public float moveTime, waitTime;
    private float moveCount, waitCount;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        leftPoint.parent = null;
        rightPoint.parent = null;

        movingRight = true;

        moveCount = moveTime;
    }

    // Update is called once per frame
    void Update()
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

            if(moveCount<=0)
            {
                waitCount = waitTime;
            }
        } else if (waitCount > 0)
        {
            waitCount -= Time.deltaTime;
            rb.velocity = new Vector2(0f, rb.velocity.y);

            if (waitCount <= 0)
            {
                moveCount = moveTime;
            }
        }
    }
}
