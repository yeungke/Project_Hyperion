using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainContainer : MonoBehaviour, Boss 
{
    [SerializeField] private int _maxHP = 100;

    [SerializeField] private TurretLogic[] _turretsLeft;
    [SerializeField] private TurretLogic[] _turretsRight;

    [SerializeField] private int _currentHP;

    [SerializeField] private GameObject _player; 


    [SerializeField] private Phases _phase;

    private bool _attackAvailable = false;
    private float _icd;

    // Start is called before the first frame update
    void Start()
    {
        _icd = 1.5f;
        _currentHP = _maxHP;
        _phase = Phases.PHASE1;
        _player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(_icd);
        _attackAvailable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (_currentHP > 0)
        {
            Fight();
        }

    }

    public void TakeDamage(int dmg)
    {
        _currentHP -= dmg;
        PhaseCheck();

    }

    private void PhaseCheck()
    {
        if (_currentHP >= _maxHP * 0.75 && _phase != Phases.PHASE1)
        {
            _phase = Phases.PHASE1;
            _icd = 1.5f;
            foreach (TurretLogic item in _turretsLeft)
            {
                item.SetAttackCD(2.5f);
            }
            foreach (TurretLogic item in _turretsRight)
            {
                item.SetAttackCD(2.5f);
            }
        }
        else if (_currentHP >= _maxHP * 0.5 && _phase != Phases.PHASE2)
        {
            _phase = Phases.PHASE2;
            _icd = 1.25f;
            foreach (TurretLogic item in _turretsLeft)
            {
                item.SetAttackCD(2f);
            }
            foreach (TurretLogic item in _turretsRight)
            {
                item.SetAttackCD(2f);
            }
        }
        else if (_currentHP >= _maxHP * 0.25 && _phase != Phases.PHASE3)
        {
            _phase = Phases.PHASE3;
            _icd = 1f;
            foreach (TurretLogic item in _turretsLeft)
            {
                item.SetAttackCD(1.25f);
            }
            foreach (TurretLogic item in _turretsRight)
            {
                item.SetAttackCD(1.25f);
            }
        }
        else if (_currentHP > 0 && _phase != Phases.PHASE4)
        {
            _phase = Phases.PHASE4;
            _icd = 0.75f;
            foreach (TurretLogic item in _turretsLeft)
            {
                item.SetAttackCD(0.75f);
            }
            foreach (TurretLogic item in _turretsRight)
            {
                item.SetAttackCD(0.75f);
            }
        }
    }

    private void Fight()
    {
        if (_player.transform.position.x <= gameObject.transform.position.x)
        {
            foreach (TurretLogic item in _turretsLeft)
            {
                item._active = true;
            }
            foreach (TurretLogic item in _turretsRight)
            {
                item._active = false;
            }
            if (_attackAvailable)
            {
                _attackAvailable = false;
                foreach (TurretLogic item in _turretsLeft)
                {
                    if (item.GetAttackAvailable())
                    {
                        item.Attack();
                        break;
                    }
                }
                StartCoroutine(Cooldown());
            }
        }
        else
        {
            foreach (TurretLogic item in _turretsRight)
            {
                item._active = true;
            }
            foreach (TurretLogic item in _turretsLeft)
            {
                item._active = false;
            }
            if (_attackAvailable)
            {
                _attackAvailable = false;
                foreach (TurretLogic item in _turretsRight)
                {
                    if (item.GetAttackAvailable())
                    {
                        item.Attack();
                        break;
                    }
                }
                StartCoroutine(Cooldown());
            }
        }
    }
}
