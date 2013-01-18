using UnityEngine;
using System.Collections;

public class PassAction : DefaultMenuAction,MenuAction {
	
	public override void Action() {
		PlayClickSound();
		TurnOrder.ActionTaken();
	}
	
}