using UnityEngine;
using System.Collections;

public class CannonWeaponUpdate : WeaponCostLabelUpdate {
	public CannonWeaponUpdate() : base() {
		this.w = new Cannon();
	}
}
