using UnityEngine;
using System.Collections;

public class Totem : Faction {

	// Use this for initialization
	public Totem() {
		this.factionName = "Totem";
		this.materials = new string[3] {"Owl", "Coyote", "Bison"};
		this.weapons = new string[4] {"Nothing", "Arrows", "Spirit 1", "Spirit 2"};
	}
}