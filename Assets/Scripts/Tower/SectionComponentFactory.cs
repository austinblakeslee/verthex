using UnityEngine;
using System.Collections;

public class SectionComponentFactory : MonoBehaviour {

	public static SectionMaterial GetMaterial(string materialName) {
		if(materialName == "Wood") {
			return new Wood();
		} else if(materialName == "Stone") {
			return new Stone();
		} else if(materialName == "Steel") {
			return new Steel();
		} else {
			return null;
		}
	}
	
	public static SectionWeapon GetWeapon(string weaponName) {
		if(weaponName == "Ballista") {
			return new Ballista();
		} else if(weaponName == "Catapult") {
			return new Catapult();
		} else if(weaponName == "Cannon") {
			return new Cannon();
		} else {
			return new Nothing();
		}
	}
}
