using UnityEngine;
using System.Collections;

public class FortifyAction : DefaultMenuAction,MenuAction {
	
	public override void Action() {
		SectionController selectedSection = TowerSelection.GetSelectedSection();
		if(selectedSection != null) {
			Section s = selectedSection.GetSection();
			Player p = TurnOrder.myPlayer;
			int cost = s.GetMaterial().GetCostPerRepair();
			if(selectedSection.GetPlayer() != p) {
				ValueStore.helpMessage = "You must select your own tower section to do that!";
			} else if(cost > p.GetResources()) {
				ValueStore.helpMessage = "You don't have enough RP to do that!";
			} else if(s.GetMaterial().GetMaxSP() == s.GetSP()) {
				ValueStore.helpMessage = "This section's SP has reached the maximum available to this material!";
			} else {
				TurnOrder.SendAction(new Fortify(selectedSection.GetHeight()-1));
			}
		} else {
			ValueStore.helpMessage = "You must select a section to fortify!";
		}
	}
	
}
