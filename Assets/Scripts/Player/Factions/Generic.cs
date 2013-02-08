using UnityEngine;
using System.Collections;

public class Generic : Faction {

	// Use this for initialization
	public Generic() {
		this.factionName = "Generic";
	}
	
	public override SectionMaterial GetSectionMaterial(int strength) {
		switch(strength) {
			case 0:
				return new Wood();
			case 1:
				return new Stone();
			case 2:
				return new Steel();
			default:
				Debug.Log(strength + " Unknown section material for " + factionName);
				return null;
		}
	}
	
	public override SectionWeapon GetSectionWeapon(int strength) {
		switch(strength) {
			case 0:
				return new Nothing();
			case 1:
				return new Ballista();
			case 2:
				return new Catapult();
			case 3:
				return new Cannon();
			default:
				Debug.Log(strength + " Unknown section weapon for " + factionName);
				return null;
		}
	}
}
