using UnityEngine;
using System.Collections;

public class MaterialCostLabelUpdate : DefaultMenuAction,MenuAction {
	public MenuItem materialCost;
	public SumLabel sumItem;
	public SectionMaterial m;
	
	public override void Action() {
		materialCost.text = m.GetMaterialType() + ": " + m.GetCost() + " RP";
		int weaponCost = ValueStore.selectedWeapon == null ? 0 : ValueStore.selectedWeapon.GetCost();
		int sum = weaponCost + m.GetCost();
		sumItem.text = "Total: " + sum + " RP";
		sumItem.m = m;
		ValueStore.selectedMaterial = m;
		PlayClickSound();
	}
}
