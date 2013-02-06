using UnityEngine;
using System.Collections;

public class DisintegrationBeamWeaponUpdate : WeaponCostLabelUpdate {
	// Use this for initialization
	public DisintegrationBeamWeaponUpdate() : base() {
		this.w = new DisintegrationBeam();
	}
}
