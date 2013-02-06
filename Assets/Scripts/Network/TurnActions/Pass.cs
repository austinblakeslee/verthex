using UnityEngine;
using System.Collections;

public class Pass : TurnAction {
	
	public Pass(string actionMessage) : base("Pass") {
		ParseActionMessage(actionMessage);
	}

	public Pass() : base("Pass") {
		this.playerNumber = TurnOrder.myPlayer.playerNumber;
	}
	
	public override string GetActionMessage() {
		return FormatActionMessage("sad-times");
	}
	
	protected override void ParseActionMessage(string actionMessage) {
		string[] tokens = actionMessage.Split(TOKEN_SEPARATOR);
		this.playerNumber = int.Parse(tokens[0]);
	}
	
	public override void Perform() {
		ValueStore.helpMessage = "Passing";
		TowerSelection.LocalSelectSection(playerNumber, -1);
		CombatLog.addLine("Pass!");
	}
}
