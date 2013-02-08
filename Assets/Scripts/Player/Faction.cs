using UnityEngine;
using System.Collections;

public abstract class Faction {

	public static int NUM_MATERIALS = 3;
	public static int NUM_WEAPONS = 4;
	
	public string factionName;

	public abstract SectionMaterial GetSectionMaterial(int strength);
	public abstract SectionWeapon GetSectionWeapon(int strength);
}
