using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    // Initialize single instance of UpgradeManager
    public static UpgradeManager instance;


    //[SerializeField]private List<Upgrade> _upgrades;

    private Hashtable _upgradesList;


    // Values that determine the player's upgrades
    [SerializeField] private bool jump;
    [SerializeField] private bool doubleJump;
    [SerializeField] private bool crouch;
    [SerializeField] private bool crouchJump;
    [SerializeField] private bool crouchAir;
    [SerializeField] private bool attackSword;
    [SerializeField] private bool attackGun;
    [SerializeField] private bool attackGunLong;
    [SerializeField] private bool attackGunCharge;
    [SerializeField] private bool attackGunPierce;
    [SerializeField] private bool attackGunXray;


    [SerializeField] private int _maxUpgradeSlots;
    [SerializeField] private int _upgradeSlots;

    public string getUpgradeCount()
    {
        return $"{_upgradeSlots}/{_maxUpgradeSlots}";
    }

    public bool GetFreeSlot()
    {
        return _upgradeSlots < _maxUpgradeSlots;
    }

    public List<Upgrade> GetActiveUpgrades()
    {
        //return _upgradesList;

        List<Upgrade> activeUpgrades = new List<Upgrade>();

        foreach (DictionaryEntry item in _upgradesList)
        {
            Upgrade upgrade = (Upgrade)item.Value;
            if (upgrade._enabled && upgrade._pickedUp)
            {
                activeUpgrades.Add(upgrade);
            }
        }

        return activeUpgrades;
    }

    public List<Upgrade> GetPickedUpUpgrades()
    {

        List<Upgrade> pickedUpUpgrades = new List<Upgrade>();

        foreach (DictionaryEntry item in _upgradesList)
        {
            Upgrade upgrade = (Upgrade)item.Value;
            if (upgrade._pickedUp)
            {
                pickedUpUpgrades.Add(upgrade);
            }
        }

        return pickedUpUpgrades;
    }


    // Start is called before the first frame update
    void Start()
    {
        _maxUpgradeSlots = 8;
        _upgradeSlots = 0;

        // Singleton pattern instance
        if (instance == null)
            instance = this;

        /*for (int i = 0; i < System.Enum.GetValues(typeof(Upgrades)).Length; i++)
        {
            Upgrade _upgrade = new Upgrade((Upgrades) i, false, false);
            _upgrades.Add(_upgrade);
            Debug.Log("added upgrade" + _upgrade);
        }*/


        // Possibly store/load hashtable from playerprefs
        _upgradesList = new Hashtable();
        for (int i = 0; i < System.Enum.GetValues(typeof(Upgrades)).Length; i++)
        {
            /*            List<bool> upgrade = new List<bool>();
                        upgrade.Add(false);
                        upgrade.Add(false);*/
            //Upgrade upgrade = new Upgrade((Upgrades)i, false, false);
            Upgrade upgrade = new Upgrade((Upgrades)i, false, false);
            _upgradesList.Add((Upgrades) i, upgrade);
            Debug.Log(_upgradesList[(Upgrades)i].ToString());
        }
    }

    public void LoadFromSave(List<string> active, List<string> pickedUp)
    {

        _maxUpgradeSlots = PlayerPrefs.GetInt("MaxUpgradeSlots", 8);
        _upgradesList = new Hashtable();
        for (int i = 0; i < System.Enum.GetValues(typeof(Upgrades)).Length; i++)
        {
            Upgrade upgrade = new Upgrade((Upgrades)i, false, false);
            _upgradesList.Add((Upgrades)i, upgrade);
            Debug.Log(_upgradesList[(Upgrades)i].ToString());
        }

        foreach (string upgrade in pickedUp)
        {
            Upgrades type;//= (Upgrades)System.Enum.Parse(typeof(Upgrades), upgrade);
            if (System.Enum.TryParse(upgrade, out type))
            {
                AddUpgrade(type);
                /*UpgradeListView upgradeUI = Object.FindObjectOfType<UpgradeListView>();

                if (!instance._upgradesList.ContainsKey(type))
                {
                    Upgrade upgrade = new Upgrade(type, true, false);
                    _upgradesList.Add(type, upgrade);
                    Debug.Log(_upgradesList[type].ToString());
                    if (upgradeUI != null)
                    {
                        Debug.Log("checkl");
                        upgradeUI.PickedUpUpgrade(type);
                    }
                }
                else
                {
                    Upgrade upgrade = (Upgrade)_upgradesList[type];
                    upgrade._pickedUp = true;
                    _upgradesList[type] = upgrade;
                    Debug.Log(_upgradesList[type].ToString());
                    if (upgradeUI != null)
                    {
                        Debug.Log("check2");
                        upgradeUI.PickedUpUpgrade(type);
                    }
                }*/
            }
        }

        foreach (string upgrade in active)
        {
            Upgrades type;
            if (System.Enum.TryParse(upgrade, out type))
            {
                EnableUpgrade(type);
            }
        }
    }

    #region HashTable Upgrades

    public List<Upgrade> GetUpgrades()
    {
        List<Upgrade> upgradeList = new List<Upgrade>();
        foreach (DictionaryEntry upgrade in _upgradesList)
        {
            upgradeList.Add((Upgrade)upgrade.Value);
        }

        return upgradeList;
    }

    public bool HasUpgrade(Upgrades type) 
    {
        //bool hasUpgrade = false;


        if (!_upgradesList.ContainsKey(type))
        {
            //Debug.Log("does not contain upgrade");
            return false;
        }
        else
        {
            //Debug.Log("contains upgrade");
            Upgrade upgrade = (Upgrade)_upgradesList[type];
            Debug.Log($"{upgrade._pickedUp}");
            return upgrade._pickedUp;
        }
    }

    public void AddUpgrade(Upgrades type)
    {
        UpgradeListView upgradeUI = Object.FindObjectOfType<UpgradeListView>();

        if (!_upgradesList.ContainsKey(type))
        {
            Upgrade upgrade = new Upgrade(type, true, false);
            _upgradesList.Add(type, upgrade);
            Debug.Log(_upgradesList[type].ToString());
            if (upgradeUI != null)
            {
                Debug.Log("checkl");
                upgradeUI.PickedUpUpgrade(type);
            }
        }
        else
        {
            Upgrade upgrade = (Upgrade)_upgradesList[type];
            upgrade._pickedUp = true;
            _upgradesList[type] = upgrade;
            Debug.Log(_upgradesList[type].ToString());
            if (upgradeUI != null)
            {
                Debug.Log("check2");
                upgradeUI.PickedUpUpgrade(type);
            }
        }
    }

    public void EnableUpgrade(Upgrades type)
    {
        if (_upgradeSlots != _maxUpgradeSlots)
        {
            if (_upgradesList.ContainsKey(type))
            {
                Upgrade upgrade = (Upgrade)_upgradesList[type];

                if (upgrade._pickedUp)
                {
                    upgrade._enabled = true;
                    _upgradesList[type] = upgrade;
                    Debug.Log(_upgradesList[type].ToString());
                    _upgradeSlots++;
                }
            }
        }
    }

    public void DisableUpgrade(Upgrades type)
    {
        if (_upgradesList.ContainsKey(type))
        {
            Upgrade upgrade = (Upgrade)_upgradesList[type];

            if (upgrade._pickedUp)
            {
                upgrade._enabled = false;
                _upgradesList[type] = upgrade;
                Debug.Log(_upgradesList[type].ToString());
                _upgradeSlots--;
            }
        }
    }

    public bool IsEnabled(Upgrades type)
    {
        if (_upgradesList.ContainsKey(type))
        {
            Upgrade upgrade = (Upgrade)_upgradesList[type];
            return upgrade._enabled;
        }
        return false;
    }
    #endregion

    // Jump accessor and mutator
    public bool GetJump() => jump;
    public void SetJump(bool jumpBool) => jump = jumpBool;

    // Double Jump accessor and mutator
    public bool GetDoubleJump() => doubleJump;
    public void SetDoubleJump(bool dblJumpBool) => doubleJump = dblJumpBool;

    // Crouch accessor and mutator
    public bool GetCrouch() => crouch;
    public void SetCrouch(bool crouchBool) => crouch = crouchBool;

    // Crouch Jump accessor and mutator
    public bool GetCrouchJump() => crouchJump;
    public void SetCrouchJump(bool crJumpBool) => crouchJump = crJumpBool;

    // Crouch in Air accessor and mutator
    public bool GetCrouchAir() => crouchAir;
    public void SetCrouchAir(bool crAirBool) => crouchAir = crAirBool;

    // Sword Attack accessor and mutator
    public bool GetAttackSword() => attackSword;
    public void SetAttackSword(bool swordBool) => attackSword = swordBool;
    
    // Gun Attack accessor and mutator
    public bool GetAttackGun() => attackGun;
    public void SetAttackGun(bool gunBool) => attackGun = gunBool;

    // Long Gun Attack accessor and mutator
    public bool GetAttackGunLong() => attackGunLong;
    public void SetAttackGunLong(bool gunBoolLong) => attackGunLong = gunBoolLong;
    
    // Long Gun Attack accessor and mutator
    public bool GetAttackGunCharge() => attackGunCharge;
    public void SetAttackGunCharge(bool gunBoolCharge) => attackGunCharge = gunBoolCharge;
    
    // Piercing Gun Attack accessor and mutator
    public bool GetAttackGunPierce() => attackGunPierce;
    public void SetAttackGunPierce(bool gunBoolPierce) => attackGunPierce = gunBoolPierce;

    // X-Ray Gun Attack accessor and mutator
    public bool GetAttackGunXray() => attackGunXray;
    public void SetAttackGunXray(bool gunBoolXray) => attackGunXray = gunBoolXray;

    // Update is called once per frame
    void Update()
    {
        
    }
}
