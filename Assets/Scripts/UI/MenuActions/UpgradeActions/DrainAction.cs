using UnityEngine;
using System.Collections;

public class DrainAction : DefaultMenuAction,MenuAction {
	
	public override void Action() {
		SectionController selectedSection = TowerSelection.GetSelectedSection();
		if(selectedSection != null) {
			Section s = selectedSection.GetSection();
			int cost = 200;
			Player p = TurnOrder.myPlayer;
			if(s.GetWeapon().GetEffect().GetEffectType() == "Multi") {
				ValueStore.helpMessage = "Cannot add Burn effect. Multi effect already purchased!";
			} else if(s.GetWeapon().GetEffect().GetUpgradeLevel() >= 3) {
				ValueStore.helpMessage = "The weapon effect is already at max!";
			} else if(cost > p.GetResources()) {
				ValueStore.helpMessage = "You don't have enough RP to do that!";
			} else {
				TurnOrder.SendAction(new Upgrade(selectedSection.GetHeight()-1, "Drain"));
			}
		} else {
			ValueStore.helpMessage = "You must select a section to upgrade!";
		}
	}
	
}