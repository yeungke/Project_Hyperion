using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseListener : MonoBehaviour
{
    [SerializeField] private UpgradeListView _upgradeListViewPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
/*            GameManager.Pause();
            List<Upgrade> upgrades = UpgradeManager.instance.GetUpgrades();
            UpgradeListView upgradeListView = (UpgradeListView)Instantiate(_upgradeListViewPrefab);
            upgradeListView.Prime(upgrades);*/
        }
    }
}
