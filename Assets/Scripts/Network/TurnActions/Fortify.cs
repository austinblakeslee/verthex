using UnityEngine;
using System.Collections;

public class Fortify : TurnAction {

	public int sectionNum;
	
	public Fortify(string actionMessage) : base("Fortify") {
		ParseActionMessage(actionMessage);
	}

	public Fortify(int t, int n) : base("Fortify") {
		this.playerNumber = TurnOrder.myPlayer.playerNumber;
		this.towerNumber = t;
		this.sectionNum = n;
	}
	
	public override string GetActionMessage() {
		return FormatActionMessage(this.sectionNum+"");
	}
	
	protected override void ParseActionMessage(string actionMessage) {
		string[] tokens = actionMessage.Split(TOKEN_SEPARATOR);
		this.playerNumber = int.Parse(tokens[0]);
		this.sectionNum = int.Parse(tokens[FIRST_AVAILABLE_INDEX]);
	}
	
	public override void Perform() {
		ValueStore.helpMessage = "Fortifying";
		Player p = TurnOrder.GetPlayerByNumber(playerNumber);
		TowerSelection.LocalSelectSection(p.GetTower(towerNumber), sectionNum);
		TowerSelection.GetSelectedSection().PlayRepairSound();
		CombatLog.addLine("Fortified section");
		p.RepairSection(sectionNum, towerNumber);
	}
}
