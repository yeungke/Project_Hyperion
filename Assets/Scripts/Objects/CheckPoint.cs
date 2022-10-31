using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{

    [SerializeField] private GameObject _prompt;

    [SerializeField] private Animator _animator;

    [SerializeField] private bool _active = false; // if this is the saved checkpoint

    [SerializeField] private bool _enabled = false;

    //private int sceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        int savedSceneIndex = PlayerPrefs.GetInt("Save_SCENE", 0);

        if (savedSceneIndex == SceneManager.GetActiveScene().buildIndex)
        {
            _active = true;
            _animator.SetBool("active", _active);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_enabled && !_active)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _active = true;
                _animator.SetBool("active", _active);
                GameManager.Save(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_active)
        {
            Debug.Log("triggered checkpoint collider");

            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            if (player != null)
            {
                if (!_prompt.activeSelf)
                {
                    _prompt.SetActive(true);
                }

                _enabled = true;
            }
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

            _enabled = false;
        }
    }
}
