using UnityEngine;
using System.Collections;

public class WeaponCostLabelUpdate : DefaultMenuAction,MenuAction {

	public string weaponName;
	
	public override void Action() {
		ValueStore.selectedWeapon = SectionComponentFactory.GetWeapon(weaponName);
		PlayClickSound();
	}
}
