using UnityEngine;
using System.Collections;

public class DamageAction : DefaultMenuAction,MenuAction {
	
	public override void Action() {
		SectionController selectedSection = TowerSelection.GetSelectedSection();
		if(selectedSection != null) {
			Section s = selectedSection.GetSection();
			int cost = 200;
			Player p = TurnOrder.currentPlayer;
			if(s.GetWeapon().GetDamageUpgradeLevel() >= 3) {
				ValueStore.helpMessage = "Damage cannot be upgraded further.";
			} else if(cost > p.GetResources()) {
				ValueStore.helpMessage = "You don't have enough RP to do that!";
			} else {
				s.GetWeapon().UpgradeDamage();
				p.RemoveResources(cost);
				TurnOrder.ActionTaken();
			}
		} else {
			ValueStore.helpMessage = "You must select a section to upgrade!";
		}
	}
	
}
