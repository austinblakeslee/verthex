using UnityEngine;
using System.Collections;

public class Pass : TurnAction {
	
	public Pass(string actionMessage) : base("Pass") {
		ParseActionMessage(actionMessage);
	}

	public Pass(int t) : base("Pass") {
		this.playerNumber = TurnOrder.myPlayer.playerNumber;
		this.towerNumber = t;
	}
	
	public override string GetActionMessage() {
		return FormatActionMessage("sad-times");
	}
	
	protected override void ParseActionMessage(string actionMessage) {
		string[] tokens = actionMessage.Split(TOKEN_SEPARATOR);
		this.playerNumber = int.Parse(tokens[0]);
		this.towerNumber = int.Parse(tokens[1]);
	}
	
	public override void Perform() {
		ValueStore.helpMessage = "Passing";
		Player p = TurnOrder.GetPlayerByNumber(playerNumber);
		TowerSelection.LocalSelectSection(p.GetTower(towerNumber), -1);
		CombatLog.addLine("Pass!");
	}
}
