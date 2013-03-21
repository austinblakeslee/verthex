using UnityEngine;
using System.Collections;

public class DamageAction : DefaultMenuAction,MenuAction {
	
	public override void Action() {
		Section s = TowerSelection.GetSelectedSection();
		if(s != null) {
			int cost = 200;
			Player p = TurnOrder.myPlayer;
			if(s.attributes.weapon.GetUpgradeLevel()+1 >= s.attributes.weapon.maxUpgradeLevel) {
				ValueStore.helpMessage = "Damage cannot be upgraded further.";
			} else if(cost > p.GetResources()) {
				ValueStore.helpMessage = "You don't have enough RP to do that!";
			} else {
				TurnOrder.SendAction(new Upgrade(s.attributes.myTower.towerNum, s.attributes.height, "Damage"));
			}
		} else {
			ValueStore.helpMessage = "You must select a section to upgrade!";
		}
	}
	
}
