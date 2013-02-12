using UnityEngine;
using System.Collections;

public class Area51 : Faction {

	// Use this for initialization
	public Area51() {
		this.factionName = "Area 51";
		this.materials = new string[3] {"Satellite Ring", "Goo Tube", "UFO"};
		this.weapons = new string[4] {"Nothing", "Blaster", "Disintegration Beam", "Eye Blaster"};
	}
}
