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
		}else if(materialName == "Satellite Ring") {
			return new SatelliteRing();
		}else if(materialName == "Goo Tube") {
			return new GooTube();
		}else if(materialName == "UFO") {
			return new UFO();
		}else if(materialName == "Water Tower") {
			return new WaterTower();}
		else if(materialName == "Saloon") {
			return new Saloon();}
		else if(materialName == "Jail Cell") {
			return new JailCell();
		}else {
			return null;
		}
		/*(
		switch(materialName){
		case "Wood":
			return new Wood();
		case "Stone":
			return new Stone();
		case "Steel":
			return new Steel();
		case "Satellite Ring":
			return new SatelliteRing();
		case "Goo Tube":
			return new GooTube();
		case "UFO":
			return new UFO();
		case "Water Tower":
			return new WaterTower();
		case "Saloon":
			return new Saloon();
		case "Jail Cell":
			return new JailCell();
		}
		return null;
		*/
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
		} else {
			return new Nothing();
		}

		/*switch(weaponName){
		case "Ballista":
			return new Ballista();
		case "Catapult":
			return new Catapult();
		case "Cannon":
			return new Cannon();
		case "Blaster":
			return new Blaster();
		case "Disintegration Beam":
			return new DisintegrationBeam();
		case "Eye Blaster":
			return new EyeBlaster();
		case "Pistols":
			return new Pistols();
		case "Gattling Gun":
			return new GattlingGun();
		}
		return new Nothing();
		*/
	}
}
