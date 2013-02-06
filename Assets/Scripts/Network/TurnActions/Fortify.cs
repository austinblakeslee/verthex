using UnityEngine;
using System.Collections;

public class Fortify : TurnAction {

	public int sectionNum;
	
	public Fortify(string actionMessage) : base("Fortify") {
		ParseActionMessage(actionMessage);
	}

	public Fortify(int n) : base("Fortify") {
		this.playerNumber = TurnOrder.myPlayer.playerNumber;
		this.sectionNum = n;
	}
	
	public override string GetActionMessage() {
		return FormatActionMessage(this.sectionNum+"");
	}
	
	protected override void ParseActionMessage(string actionMessage) {
		string[] tokens = actionMessage.Split(TOKEN_SEPARATOR);
		this.playerNumber = int.Parse(tokens[0]);
		this.sectionNum = int.Parse(tokens[2]);
	}
	
	public override void Perform() {
		ValueStore.helpMessage = "Fortifying";
		TowerSelection.LocalSelectSection(playerNumber, sectionNum);
		TowerSelection.GetSelectedSection().PlayRepairSound();
		CombatLog.addLine("Fortified section");
		TurnOrder.GetPlayerByNumber(playerNumber).RepairSection(sectionNum);
	}
}
