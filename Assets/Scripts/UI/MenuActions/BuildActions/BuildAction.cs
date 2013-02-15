using UnityEngine;
using System.Collections;

public class BuildAction : DefaultMenuAction,MenuAction {
	public Menu myMenu;
	
	public override void Action() {
		SectionMaterial m = ValueStore.selectedMaterial;
		SectionWeapon w = ValueStore.selectedWeapon;
		if(m != null && w != null) {
			SectionAttributes s = new SectionAttributes(m, w);
			if(TurnOrder.myPlayer.GetResources() >= s.GetCost()) {
				myMenu.on = false;
				ValueStore.helpMessage = "";
				TurnOrder.SendAction(new Build(TurnOrder.actionNum, m, w));
			} else {
				ValueStore.helpMessage = "You don't have enough RP to do that!";
			}
		} else {
			ValueStore.helpMessage = "You must select a material and a weapon type!";
		}
	}
	
	void Update() {
		if(myMenu.on) {
			SectionMaterial m = ValueStore.selectedMaterial;
			SectionWeapon w = ValueStore.selectedWeapon;
			if(m == null || w == null) {
				ValueStore.helpMessage = "Select both a material and a weapon.";
			} else if(m.GetCost() + w.GetCost() > TurnOrder.myPlayer.GetResources()) {
				ValueStore.helpMessage = "You do not have enough resources to build that. Choose different options.";
			} else {
				int weight = (int)(m.GetWeightPerSP() * m.GetInitialSP()) + w.GetWeight();
				string help = "Weight: " + weight;
				help += "\nDamage: " + w.GetDamage();
				ValueStore.helpMessage = help;
			}
		}
	}
}