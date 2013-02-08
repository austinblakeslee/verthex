using UnityEngine;
using System;
using System.Collections;

public abstract class Faction {

	public static int NUM_MATERIALS = 3;
	public static int NUM_WEAPONS = 4;
	
	public string factionName;
	protected string[] materials;
	protected string[] weapons;

	public SectionMaterial GetSectionMaterial(int strength) {
		return SectionComponentFactory.GetMaterial(materials[strength]);
	}
	public SectionWeapon GetSectionWeapon(int strength) {
		return SectionComponentFactory.GetWeapon(weapons[strength]);
	}
	
	public int EncodeSectionMaterial(string mtype) {
		return Array.IndexOf(materials, mtype);
	}
	
	public int EncodeSectionWeapon(string wtype) {
		return Array.IndexOf(weapons, wtype);
	}
}
