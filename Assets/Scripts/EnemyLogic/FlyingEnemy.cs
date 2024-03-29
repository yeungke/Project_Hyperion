using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    enum Type
    {
        RANGED,
        MELEE
    }

    public float _speed;
    public bool _chase = false;
    public Transform _start;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _bullet;
    public int _health;

    private Animator _animator;


    [SerializeField] private Transform _aimer;
    [SerializeField] private Type _type;
    [SerializeField] private CapsuleCollider2D _attackRange;

    [SerializeField] private BoxCollider2D _bodyCollider;

    [SerializeField] private float _attackCD;
    private bool _attackAvailable;

    private float _distance;
    private float _cdTimer;


    // Start is called before the first frame update
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();

        _attackRange = GetComponentInChildren<CapsuleCollider2D>();
        _bodyCollider = GetComponent<BoxCollider2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _attackAvailable = true;
        if (_type == Type.MELEE)
        {
            _distance = 3f;
            _attackRange.size = new Vector2(_distance, _distance);
            _attackCD = 1.5f;
            _health = 10;
        }
        else if (_type == Type.RANGED)
        {
            _distance = 7.5f;
            _attackRange.size = new Vector2(_distance, _distance);
            _attackCD = 2.5f;
            _health = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_player == null)
        {
            return;
        }
        if (_chase)
        {
            Vector2 offsetVector = _player.transform.position - transform.position;
            _attackRange.offset = offsetVector;
            ChasePlayer();
            //Flip();
            if (_attackAvailable && (Vector2.Distance(transform.position, _player.transform.position) < _distance / 1.99f))
            {
                Attack();
                _attackAvailable = false;
                _cdTimer = _attackCD;
            }
        }
        else
        {
            Return();
        }

        if (_cdTimer > 0f && !_attackAvailable)
        {
            _cdTimer -= Time.deltaTime;
            if (_cdTimer < 0f)
            {
                _attackAvailable = true;
                Debug.Log("attack ready");
            }
        }

        if (_health <= 0)
        {
            DestoryThis();
        }
    }


    public void TakeDamage(int dmg)
    {
        _health -= dmg;
    }

    private void Attack()
    {
        Debug.Log("attacking");


        Instantiate(_bullet, transform.position, _aimer.transform.rotation);
    }

    private void Return()
    {
        Vector2 current = transform.position;
        Vector2 target = _start.position;
        this.transform.position = Vector2.MoveTowards(current, target, _speed * Time.deltaTime);
    }

    private void ChasePlayer()
    {
        Vector2 current = transform.position;
        Vector2 target = _player.transform.position;

        Vector2 newPos = GetClosestPosition();
        this.transform.position = Vector2.MoveTowards(current, newPos, _speed * Time.deltaTime);

        _aimer.transform.right = _player.transform.position - transform.position;

        if (_player.transform.position.x > gameObject.transform.position.x)
        {
            _animator.SetBool("MoveR", true);
            _animator.SetBool("MoveL", false);
        }
        else
        {
            _animator.SetBool("MoveL", true);
            _animator.SetBool("MoveR", false);
        }

        //this.transform.position = Vector2.MoveTowards(current, target, _speed * Time.deltaTime);
    }

    private Vector2 GetClosestPosition()
    {
        Vector2 point;

        if (Vector2.Distance(transform.position, _player.transform.position) > _distance / 2f)
        {
            point = _attackRange.ClosestPoint(transform.position);
        }
        else
        {
            point = transform.position - _player.transform.position;
            point = point.normalized * (_distance / 2f);
        }

        if (transform.position.x <= _player.transform.position.x)
        {
            point = new Vector2(-1 * point.x, point.y);
        }
        return point;
    }

/*    private void Flip()
    {
        if (transform.position.x > _player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }*/

    private void DestoryThis()
    {
        Destroy(this.gameObject);
    }
}
