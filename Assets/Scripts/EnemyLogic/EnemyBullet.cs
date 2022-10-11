using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private float _maxDistance;
    private float _distance;
    [SerializeField] private Rigidbody2D _rb;
    private Vector2 _startingPoint;

    // Start is called before the first frame update
    void Start()
    {
        _rb.velocity = transform.right * _speed;
        _maxDistance = 3.25f;
        _distance = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Travel();
    }

    private void Travel()
    {
        float travel = Time.deltaTime * _speed;
        transform.Translate(Vector2.right * travel);

        _distance += travel;

        if (_distance > _maxDistance)
        {
            DestroyThis();
        }
    }

    private void DestroyThis()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        bool terrain = collision.gameObject.layer == LayerMask.NameToLayer("Terrain");

        if (terrain)
        {
            DestroyThis();
        }

        PlayerMovement player = collision.GetComponent<PlayerMovement>();

        if (player != null)
        {
            GameManager.DamageTaken(_damage);
            DestroyThis();
        }
    }
}
