using UnityEngine;
using System.Collections;

public class WeaponCostLabelUpdate : DefaultMenuAction,MenuAction {
	public MenuItem weaponCost;
	public SumLabel sumItem;
	public SectionWeapon w;
	
	public override void Action() {
		weaponCost.text = w.GetWeaponType() + ": " + w.GetCost() + " RP";
		int materialCost = ValueStore.selectedMaterial == null ? 0 : ValueStore.selectedMaterial.GetCost();
		int sum = materialCost + w.GetCost();
		sumItem.text = "Total: " + sum + " RP";
		sumItem.w = w;
		ValueStore.selectedWeapon = w;
		PlayClickSound();
	}
}
