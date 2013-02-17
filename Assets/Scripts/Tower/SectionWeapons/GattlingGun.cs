using UnityEngine;
using System.Collections;

public class GattlingGun : SectionWeapon {
    public GattlingGun () {
        this.damage = 80;
        this.spcost = 50;
        this.cost = 250;
        this.weight = 30;
        this.range = 2;
		this.range = 2;
        this.wtype = "Gattling Gun";
    	this.maxUpgradeEffect = new DamageOverTime();
		this.weaponEffect = new AimCritStrike();
	}
}
