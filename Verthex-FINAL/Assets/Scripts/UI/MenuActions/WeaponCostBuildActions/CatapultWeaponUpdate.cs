using UnityEngine;
using System.Collections;

public class CatapultWeaponUpdate : WeaponCostLabelUpdate {
	public CatapultWeaponUpdate() : base() {
		this.w = new Catapult();
	}
}
