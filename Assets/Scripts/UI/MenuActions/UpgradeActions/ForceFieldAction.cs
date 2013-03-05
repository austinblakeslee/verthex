using UnityEngine;
using System.Collections;

public class ForceFieldAction : DefaultMenuAction,MenuAction {
	
	public override void Action() {
		Section s = TowerSelection.GetSelectedSection();
		if(s != null) {
			int cost = 200;
			Player p = TurnOrder.myPlayer;
			WeaponEffect effect = s.attributes.weapon.GetEffect();
			if(effect.GetEffectType() != "none") {
				ValueStore.helpMessage = "Cannot add effect. An effect is already applied!";
			} else if(effect.GetUpgradeLevel() >= 3) {
				ValueStore.helpMessage = "The weapon effect is already at max!";
			} else if(cost > p.GetResources()) {
				ValueStore.helpMessage = "You don't have enough RP to do that!";
			} else {
				TurnOrder.SendAction(new Upgrade(s.attributes.myTower.towerNum, s.attributes.height, "Force Field"));
			}
		} else {
			ValueStore.helpMessage = "You must select a section to upgrade!";
		}
	}
	
}
