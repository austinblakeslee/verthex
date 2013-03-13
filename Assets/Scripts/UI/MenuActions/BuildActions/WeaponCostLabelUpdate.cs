using UnityEngine;
using System.Collections;

public class WeaponCostLabelUpdate : DefaultMenuAction,MenuAction {

	public string weaponName;
	public GameObject fromMenu = null;
	public GameObject toMenu = null;
	
	public override void Action() {
		ValueStore.selectedWeapon = SectionComponentFactory.GetWeapon(weaponName);
		fromMenu.GetComponent<Menu>().on = false;
		toMenu.GetComponent<Menu>().on = true;

	}
}