using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerDetector : MonoBehaviour
{
    [field: SerializeField]
    public bool PlayerInRange { get; private set; }
    public Transform Player { get; private set; }

    public Animator anim;

    public float _attackCD = 3f;
    private float _cdTimer = 0f;
    public bool _canAttack = true;

    [SerializeField]
    private string playerTag = "Player";

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag) && _canAttack)
        {
            PlayerInRange = true;
            Player = collision.gameObject.transform;
            anim.SetBool("isAttacking", true);
            anim.SetBool("isMoving", false);
            _canAttack = false;
            _cdTimer = _attackCD;
        }

        //TEMP
        /*if (anim.GetCurrentAnimatorStateInfo(0).IsName("enemy_attack") && collision.CompareTag(playerTag))
        {
            GameManager.DamageTaken(15);
        }*/
    }


/*    public void Attack()
    {
        _attackRange.SetActive(true);

    } 

    public void StopAttack()
    {
        _attackRange.SetActive(false);
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
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
        if (!_canAttack)
        {
            if (_cdTimer > 0f)
            {
                _cdTimer -= Time.deltaTime;
            }
            else
            {
                _canAttack = true;
            }
        }
    }
}
