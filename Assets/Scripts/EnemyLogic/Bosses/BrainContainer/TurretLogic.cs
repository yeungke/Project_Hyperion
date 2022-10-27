using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLogic : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _turret;
    [SerializeField] private GameObject _aimer;

    [SerializeField] private float _attackCD;
    [SerializeField] private bool _attackAvailable;

    [SerializeField] private int _damage;

    [SerializeField] private GameObject _bullet;

    public bool _active = false;
    private float _cd;


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

    }

    public void SetAttackCD(float cd)
    {
        _attackCD = cd;
    }

    public bool GetAttackAvailable()
    {
        return _attackAvailable;
    }

    // Update is called once per frame
    void Update()
    {
        if (_active)
        {
            Aim();
        }

        if (!_attackAvailable)
        {
            if (_cd < _attackCD)
            {
                _cd += Time.deltaTime;
            }
            else
            {
                _cd = 0;
                _attackAvailable = true;
            }

        }

    }

    private void Aim()
    {
        _turret.transform.right = _player.transform.position - transform.position;
    }

    public void Attack()
    {

        Instantiate(_bullet, _aimer.transform.position, _aimer.transform.rotation);

        _attackAvailable = false;
    }
}
