using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerDetector : MonoBehaviour
{
    [field: SerializeField]
    public bool PlayerInRange { get; private set; }
    public Transform Player { get; private set; }

    public Animator anim;

    [SerializeField]
    private string playerTag = "Player";

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            PlayerInRange = true;
            Player = collision.gameObject.transform;
            anim.SetBool("isAttacking", true);
            anim.SetBool("isMoving", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            PlayerInRange = false;
            Player = null;
            anim.SetBool("isAttacking", false);
            anim.SetBool("isMoving", true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
