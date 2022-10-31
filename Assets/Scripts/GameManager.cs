using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    //

    [SerializeField] private int _maxHP = 100;
    [SerializeField] private int _life;
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

    public static void Reload() 
    {
        int maxHP = PlayerPrefs.GetInt("Save_MAXHP", 100);
        int currentHP = PlayerPrefs.GetInt("Save_CURRENTHP", 100);
        List<string> activeUpgrades = new List<string>(PlayerPrefs.GetString("Save_ACTIVEUPGRADES", "").Split(','));
        List<string> pickedUpgrades = new List<string>(PlayerPrefs.GetString("Save_PICKEDUPGRADES", "").Split(','));

        UpgradeManager.instance.LoadFromSave(activeUpgrades, pickedUpgrades);
        _instance._maxHP = maxHP;
        _instance._life = currentHP;


        int scene = PlayerPrefs.GetInt("Save_SCENE", 0);
        SceneManager.LoadSceneAsync(scene);
    }


    public static void ClearSave()
    {
        PlayerPrefs.DeleteAll();
    }

    // put in checkpoint object
    public static void Save(int sceneIndex)
    {
        PlayerPrefs.SetInt("Save_MAXHP", _instance._maxHP);
        PlayerPrefs.SetInt("Save_CURRENTHP", _instance._life);

        List<Upgrade> pickedUpUpgrades = UpgradeManager.instance.GetPickedUpUpgrades();
        List<Upgrade> activeUpgrades = UpgradeManager.instance.GetActiveUpgrades();

        string pickedUpgradeString = "";
        string activeUpgradeString = "";
        for (int i = 0; i < pickedUpUpgrades.Count; i++)
        {
            if (i != pickedUpUpgrades.Count - 1)
            {
                pickedUpgradeString += pickedUpUpgrades[i]._upgradeType + ",";
            }
            else
            {
                pickedUpgradeString += pickedUpUpgrades[i]._upgradeType;
            }
        }

        for (int i = 0; i < activeUpgrades.Count; i++)
        {

            if (i != activeUpgrades.Count - 1)
            {
                activeUpgradeString += activeUpgrades[i]._upgradeType + ",";
            }
            else
            {
                activeUpgradeString += activeUpgrades[i]._upgradeType;
            }
        }

        PlayerPrefs.SetString("Save_PICKEDUPGRADES", pickedUpgradeString);
        PlayerPrefs.SetString("Save_ACTIVEUPGRADES", activeUpgradeString);

        PlayerPrefs.SetInt("Save_SCENE", sceneIndex);
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

    public static void DamageTaken(int dmg)
    {
        _instance._life -= dmg;
    }

    private void Awake()
    {
        _life = _maxHP;
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
