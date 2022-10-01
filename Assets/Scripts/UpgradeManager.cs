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
    [SerializeField] private bool crouch;
    [SerializeField] private bool crouchJump;
    [SerializeField] private bool crouchAir;
    [SerializeField] private bool attackSword;
    [SerializeField] private bool attackGun;
    [SerializeField] private bool attackGunLong;
    [SerializeField] private bool attackGunCharge;
    [SerializeField] private bool attackGunPierce;
    [SerializeField] private bool attackGunXray;

    // Start is called before the first frame update
    void Start()
    {
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
            Upgrade upgrade = new Upgrade((Upgrades)i, false, false);
            _upgradesList.Add((Upgrades) i, upgrade);
            Debug.Log(_upgradesList[(Upgrades)i].ToString());
        }
    }

    #region HashTable Upgrades
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
        /*foreach (Upgrade u in _upgrades)
        {
            if (u._upgradeType == type)
            {
                if (u._pickedUp)
                {
                    hasUpgrade = true;
                    break;
                }
                else
                {
                    break;
                }
            }
        }*/
        //return hasUpgrade;
    }

    public void AddUpgrade(Upgrades type)
    {
        /*foreach (Upgrade upgrade in _upgrades)
        {
            if (upgrade._upgradeType == type)
            {
                upgrade._pickedUp = true;

                //temp - delete later
                upgrade._enabled = true;
                break;
            }
        }*/


        if (!_upgradesList.ContainsKey(type))
        {
            Upgrade upgrade = new Upgrade(type, true, false);
            _upgradesList.Add(type, upgrade);
            Debug.Log(_upgradesList[type].ToString());
        }
        else
        {
            Upgrade upgrade = (Upgrade)_upgradesList[type];
            upgrade._pickedUp = true;
            _upgradesList[type] = upgrade;
            Debug.Log(_upgradesList[type].ToString());
        }
    }

    public void EnableUpgrade(Upgrades type)
    {
        if (_upgradesList.ContainsKey(type))
        {
            Upgrade upgrade = (Upgrade)_upgradesList[type];

            if (upgrade._pickedUp)
            {
                upgrade._enabled = true;
                _upgradesList[type] = upgrade;
                Debug.Log(_upgradesList[type].ToString());
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
