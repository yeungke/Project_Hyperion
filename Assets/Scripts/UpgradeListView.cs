using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeListView : MonoBehaviour
{
    [SerializeField] private List<Upgrade> _upgrades;
    [SerializeField] private List<UpgradeListItem> _upgradeItems;
    public GameObject _upgradePanel;
    public UpgradeListItem _upgradeItemPrefab;
    public Transform _targetTransform;
    // Start is called before the first frame update
    void Start()
    {
/*        foreach (Upgrade upgrade in _upgrades)
        {
            UpgradeListItem u = (UpgradeListItem)Instantiate(_upgradeItemPrefab);
            u.SetUpgrade(upgrade);
            u.transform.SetParent(_targetTransform, false);
            _upgradeItems.Add(u);
        }*/
    }

    public void PickedUpUpgrade(Upgrades type)
    {
        foreach (UpgradeListItem upgradeListItem in _upgradeItems)
        {
            if (upgradeListItem.GetUpgrade()._upgradeType == type)
            {
                upgradeListItem.PickedUpUpgrade();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
