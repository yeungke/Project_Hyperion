using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitionTEST : MonoBehaviour
{
    private bool _triggered = false;
    [SerializeField] private GameObject _LevelChange;
    [SerializeField] private SpawnLocations _spawn;
    [SerializeField] private GameObject _prompt;
    [SerializeField] private string _dest;

    [SerializeField] private Animator _animator;

    [SerializeField] private bool _locked;


    private void Start()
    {
        if (_prompt != null)
        {
            _prompt = transform.GetChild(0).gameObject;
        }

        if (_LevelChange == null)
        {
            _LevelChange = GameObject.Find("LevelChanger");
        }

        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }

        _locked = false;
        _animator.SetBool("locked", _locked);
    }

    // Update is called once per frame
    void Update()
    {
        if (_prompt.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                LevelChanger lc = _LevelChange.GetComponent<LevelChanger>();

                lc.SetLevel(_dest);
                lc.FadeToLevel(_spawn);
            }
        }
    }

    public SpawnLocations GetSpawn()
    {
        return _spawn;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerMovement player = col.gameObject.GetComponent<PlayerMovement>();

        if (player != null)
        {
            if (!_prompt.activeSelf)
            {
                _prompt.SetActive(true);
            }
            _animator.SetBool("open", true);
            Debug.Log("Triggered");

/*            LevelChanger lc = _LevelChange.GetComponent<LevelChanger>();

            lc.FadeToLevel(_spawn);*/
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player != null)
        {
            if (_prompt.activeSelf)
            {
                _prompt.SetActive(false);
            }
            _animator.SetBool("open", false);
        }
    }
}
