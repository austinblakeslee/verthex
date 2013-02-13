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
		} else if(materialName == "Satellite Ring") {
			return new SatelliteRing();
		} else if(materialName == "Goo Tube") {
			return new GooTube();
		} else if(materialName == "UFO") {
			return new UFO();
		} else if(materialName == "Water Tower") {
			return new WaterTower();
		} else if(materialName == "Saloon") {
			return new Saloon();
		} else if(materialName == "Jail Cell") {
			return new JailCell();
		} else if(materialName == "Owl") {
			return new Owl();
		} else if(materialName == "Coyote") {
			return new Coyote();
		} else if(materialName == "Bison") {
			return new Bison();
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
		} else if(weaponName == "Blaster") {
			return new Blaster();
		} else if(weaponName == "Disintegration Beam") {
			return new DisintegrationBeam();
		} else if(weaponName == "Eye Blaster") {
			return new EyeBlaster();
		} else if(weaponName == "Pistols") {
			return new Pistols();
		} else if(weaponName == "Gattling Gun") {
			return new GattlingGun();
		} else if(weaponName == "Arrows") {
			return new Arrows();
		} else if(weaponName == "Spirit 1") {
			return new Spirit1();
		} else if(weaponName == "Spirit 2") {
			return new Spirit2();
		} else {
			return new Nothing();
		}
	}
}
