using UnityEngine;
using System.Collections;

public class Generic : Faction {

	// Use this for initialization
	public Generic() {
		this.factionName = "Generic";
		this.materials = new string[3] {"Wood", "Stone", "Steel"};
		this.weapons = new string[4] {"Nothing", "Ballista", "Catapult", "Cannon"};
	}
}
