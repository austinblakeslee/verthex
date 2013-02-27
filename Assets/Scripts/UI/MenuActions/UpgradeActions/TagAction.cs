using UnityEngine;
using System.Collections;

public class TagAction : DefaultMenuAction,MenuAction {
	
	public override void Action() {
		Section s = TowerSelection.GetSelectedSection();
		if(s != null) {
			int cost = 200;
			Player p = TurnOrder.myPlayer;
			WeaponEffect effect = s.attributes.weapon.GetEffect();
			if(effect.GetEffectType() != "none") {
				ValueStore.helpMessage = "Cannot add effect. There is already an effect on this weapon.";
			} else if(effect.GetUpgradeLevel() >= 3) {
				ValueStore.helpMessage = "The weapon effect is already at max!";
			} else if(cost > p.GetResources()) {
				ValueStore.helpMessage = "You don't have enough RP to do that!";
			} else {
				Debug.Log (s.attributes.myTower.towerNum);
				TurnOrder.SendAction(new Upgrade(s.attributes.myTower.towerNum, s.attributes.height, "Tag"));
			}
		} else {
			ValueStore.helpMessage = "You must select a section to upgrade!";
		}
	}
	
}
