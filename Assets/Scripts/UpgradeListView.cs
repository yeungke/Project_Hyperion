using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeListView : MonoBehaviour
{
    [SerializeField] private List<Upgrade> _upgrades;
    [SerializeField] private List<UpgradeListItem> _upgradeItems;
    [SerializeField] private ViewManager _viewManager;
    public GameObject _upgradePanel;
    public UpgradeListItem _upgradeItemPrefab;
    public Transform _targetTransform;
    public Button _closeButton;

    public Text _upgradeCounter;


    public void Initialize()
    {
/*        List<Upgrade> upgrades = UpgradeManager.instance.GetUpgrades();
        _closeButton.onClick.AddListener(() => _viewManager.Show<PauseMenu>());
        Prime(upgrades);*/
        /*foreach (Upgrade upgrade in upgrades)
        {
            UpgradeListItem u = (UpgradeListItem)Instantiate(_upgradeItemPrefab);
            u.SetUpgrade(upgrade);
            u.transform.SetParent(_targetTransform, false);
            _upgradeItems.Add(u);
        }*/
    }


    private void Awake()
    {
        _viewManager = Object.FindObjectOfType<ViewManager>();

        if (_viewManager != null)
        {
            _closeButton.onClick.AddListener(() => _viewManager.ShowView());
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }


    // probably not being used
    public void PickedUpUpgrade(Upgrades type)
    {
        foreach (UpgradeListItem upgradeListItem in _upgradeItems)
        {
            if (upgradeListItem.GetUpgrade()._upgradeType == type)
            {
                upgradeListItem.PickedUpUpgrade();
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            _viewManager.ShowView();
            DestroyObject();
        }
        _upgradeCounter.text = UpgradeManager.instance.getUpgradeCount();
    }

    public void Prime(List<Upgrade> upgrades)
    {
        foreach (Upgrade upgrade in upgrades)
        {
            UpgradeListItem u = (UpgradeListItem)Instantiate(_upgradeItemPrefab);
            u.SetUpgrade(upgrade);
            u.transform.SetParent(_targetTransform, false);
            _upgradeItems.Add(u);
        }
    }

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
