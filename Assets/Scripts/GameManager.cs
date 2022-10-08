using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    //
    [SerializeField] private int _life = 100;
    [SerializeField] private int _numEnemies = 0;

    //debug mode
    [SerializeField] private bool _debug = false;

    // player

    [SerializeField] private GameObject _player;

    public static bool GetDebug()
    {
        return _instance._debug;
    }

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
        // Toggles the Jump in the Upgrade Manager, based on the input values given
        if (name == "jump")
        {
            UpgradeManager.instance.SetJump(val);
            //_instance._player.GetComponent<PlayerMovement>()?.SetJump(val);
        }
        // Toggles the Crouch in the Upgrade Manager, based on the input values given
        else if (name == "crouch")
        {
            UpgradeManager.instance.SetCrouch(val);
            //_instance._player.GetComponent<PlayerMovement>()?.SetCrouch(val);
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
