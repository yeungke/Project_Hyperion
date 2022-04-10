using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    //
    [SerializeField] private int _life = 100;
    [SerializeField] private int _numEnemies = 0;

    // player

    [SerializeField] private GameObject _player;


    public static void Pause()
    {
        Time.timeScale = 0f;
        
        //LevelManager.SwitchSceneToMenu();
    }

    public static void Play()
    {
        Time.timeScale = 1f;
        //LevelManager.SwitchMenuToScene();
    }

    private void OnEnable()
    {
        _instance = this;
    }

    public static int LifeUIUpdate()
    {
        return _instance._life;
    }

    public static void ToggleUpgrade(string name, bool val)
    {
        if (name == "jump")
        {
            _instance._player.GetComponent<PlayerMovement>()?.SetJump(val);
        }
        else if (name == "crouch")
        {
            _instance._player.GetComponent<PlayerMovement>()?.SetCrouch(val);
        }
    }

    public static void SetPlayer(GameObject obj)
    {
        _instance._player = obj;
    }

    public static void SetNumEnemies(int count)
    {
        _instance._numEnemies = count;
    }

    public static void EnemySlain()
    {
        _instance._numEnemies -= 1;
    }

    public static int GetNumEnemies()
    {
        return _instance._numEnemies;
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
