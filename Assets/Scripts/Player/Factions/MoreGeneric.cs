using UnityEngine;
using System.Collections;

public class MoreGeneric : Faction {

	// Use this for initialization
	public MoreGeneric() {
		this.factionName = "MoreGeneric";
		this.materials = new string[3] {"Stone", "Steel", "Wood"};
		this.weapons = new string[4] {"Nothing", "Catapult", "Cannon", "Ballista"};
	}
}
