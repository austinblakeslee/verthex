using UnityEngine;
using System.Collections;

public class GattlingGunWeaponUpdate : WeaponCostLabelUpdate {
	public GattlingGunWeaponUpdate() : base() {
		this.w = new GattlingGun();
	}
}
