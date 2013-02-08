using UnityEngine;
using System.Collections;

public abstract class Faction {

	public string factionName;

	public abstract SectionMaterial GetSectionMaterial(int strength);
	public abstract SectionWeapon GetSectionWeapon(int strength);
}
