using System.Collections;
using System.Collections.Generic;

public class Upgrade
{
    public Upgrades _upgradeType { get; set; }
    public bool _pickedUp { get; set; }
    public bool _enabled { get; set; }


    public Upgrade(Upgrades upgradeType, bool pickedUp, bool enabled)
    {
        _upgradeType = upgradeType;
        _pickedUp = pickedUp;
        _enabled = enabled;
    }

    public override string ToString()
    {
        return $"{_upgradeType}. Picked up: {_pickedUp}. Enabled: {_enabled}";
    }
}
