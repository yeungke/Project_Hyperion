using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeListItem : MonoBehaviour
{
    [SerializeField] private Text _upgradeText;
    [SerializeField] private Button _upgradeButton;

    [SerializeField] private bool _debug;
    [SerializeField] private Button _debugButton;

    private Upgrade _upgrade;

    public void PickedUpUpgrade()
    {
        _upgradeText.text = $"{_upgrade._upgradeType}";
        _upgradeButton.enabled = true;
        if (_debug)
        {
            _debugButton.gameObject.SetActive(false);
        }
    }
    public void SetUpgrade(Upgrade upgrade)
    {
        _upgrade = upgrade;

        if (_debug && !_upgrade._pickedUp)
        {
            _debugButton.gameObject.SetActive(true);
        }
        if (upgrade._pickedUp)
        {
            _upgradeText.text = $"{upgrade._upgradeType}";
            if (_upgrade._enabled)
            {
                _upgradeText.color = new Color(0f, 1f, 0f, 1f);
            }
        }
        else
        {
            _upgradeText.text = "???";
        }
    }
    private void Awake()
    {
        _debug = GameManager.GetDebug();
/*        Debug.Log($"debug: {_debug}");
        if (_debug && !_upgrade._pickedUp)
        {
            _debugButton.gameObject.SetActive(true);
        }*/
    }

    public void DebugPickedUpUpgrade()
    {
        _upgradeText.text = $"{_upgrade._upgradeType}";
        _upgradeButton.enabled = true;
        UpgradeManager.instance.AddUpgrade(_upgrade._upgradeType);
    }

    public Upgrade GetUpgrade()
    {
        return _upgrade;
    }

    public void EnableDisable()
    {
        if (_upgrade._pickedUp)
        {
            if (_upgrade._enabled)
            {
                _upgrade._enabled = false;
                _upgradeText.color = new Color(1f, 0f, 0f, 1f);
                UpgradeManager.instance.DisableUpgrade(_upgrade._upgradeType);
            }
            else
            {
                _upgrade._enabled = true;
                _upgradeText.color = new Color(0f, 1f, 0f, 1f);
                UpgradeManager.instance.EnableUpgrade(_upgrade._upgradeType);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!_upgrade._pickedUp && _upgradeButton != null)
        {
            _upgradeButton.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
