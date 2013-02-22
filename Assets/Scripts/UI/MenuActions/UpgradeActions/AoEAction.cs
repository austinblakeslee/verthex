using UnityEngine;
using System.Collections;

public class AoEAction : DefaultMenuAction,MenuAction {
	
	public override void Action() {
		Section s = TowerSelection.GetSelectedSection();
		if(s != null) {
			int cost = 200;
			Player p = TurnOrder.myPlayer;
			WeaponEffect effect = s.attributes.weapon.GetEffect();
			if(effect.GetEffectType() == "Burn") {
				ValueStore.helpMessage = "Cannot add Multi effect. Burn effect already purchased!";
			} else if(effect.GetUpgradeLevel() >= 3) {
				ValueStore.helpMessage = "The weapon effect is already at max!";
			} else if(cost > p.GetResources()) {
				ValueStore.helpMessage = "You don't have enough RP to do that!";
			} else {
				TurnOrder.SendAction(new Upgrade(TurnOrder.actionNum, s.attributes.height, "AoE"));
			}
		} else {
			ValueStore.helpMessage = "You must select a section to upgrade!";
		}
	}
	
}
