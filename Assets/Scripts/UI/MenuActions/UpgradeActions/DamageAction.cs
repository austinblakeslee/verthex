using UnityEngine;
using System.Collections;

public class DamageAction : DefaultMenuAction,MenuAction {
	
	public override void Action() {
		SectionController selectedSection = TowerSelection.GetSelectedSection();
		if(selectedSection != null) {
			Section s = selectedSection.GetSection();
			int cost = 200;
			Player p = TurnOrder.myPlayer;
			if(s.GetWeapon().GetUpgradeLevel() >= 4) {
				ValueStore.helpMessage = "Damage cannot be upgraded further.";
			} else if(cost > p.GetResources()) {
				ValueStore.helpMessage = "You don't have enough RP to do that!";
			} else {
				TurnOrder.SendAction(new Upgrade(selectedSection.GetHeight()-1, "Damage"));
			}
		} else {
			ValueStore.helpMessage = "You must select a section to upgrade!";
		}
	}
	
}
