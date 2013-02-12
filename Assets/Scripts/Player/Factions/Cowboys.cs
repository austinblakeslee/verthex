using UnityEngine;
using System.Collections;

public class Cowboys : Faction {

	// Use this for initialization
	public Cowboys() {
		this.factionName = "Cowboys";
		this.materials = new string[3] {"Water Tower", "Saloon", "Jail Cell"};
		this.weapons = new string[5] {"Nothing", "Pistols", "Gattling Gun", "Cannon", "Sniper"};
	}
}
