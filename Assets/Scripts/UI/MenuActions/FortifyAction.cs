using UnityEngine;
using System.Collections;

public class FortifyAction : DefaultMenuAction,MenuAction {
	
	public override void Action() {
		Section s = TowerSelection.GetSelectedSection();
		if(s != null) {
			Player p = TurnOrder.myPlayer;
			int cost = s.attributes.material.costPerRepair;
			if(cost > p.GetResources()) {
				ValueStore.helpMessage = "You don't have enough RP to do that!";
			} else if(s.attributes.material.maxSP == s.attributes.sp) {
				ValueStore.helpMessage = "This section's SP has reached the maximum available to this material!";
			} else {
				TurnOrder.SendAction(new Fortify(TurnOrder.actionNum, s.attributes.height));
			}
		} else {
			ValueStore.helpMessage = "You must select a section to fortify!";
		}
	}
	
}
