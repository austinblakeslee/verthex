using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fight : TurnAction {

	public int firingSection;
	public int targetTower;
	public int targetSection;
	
	public Fight(string actionMessage) : base("Fight") {
		ParseActionMessage(actionMessage);
	}

	public Fight(int tNum, int firingSection, int targetTower, int targetSection) : base("Fight") {
		this.towerNumber = tNum;
		this.playerNumber = TurnOrder.myPlayer.playerNumber;
		this.firingSection = firingSection;
		this.targetTower = targetTower;
		this.targetSection = targetSection;
	}
	
	public override string GetActionMessage() {
		return FormatActionMessage(this.firingSection+"", this.targetSection+"");
	}
	
	protected override void ParseActionMessage(string actionMessage) {
		Debug.Log(actionMessage);
		string[] tokens = actionMessage.Split(TOKEN_SEPARATOR);
		this.playerNumber = int.Parse(tokens[0]);
		this.towerNumber = int.Parse(tokens[1]);
		this.firingSection = int.Parse(tokens[FIRST_AVAILABLE_INDEX]);
		this.targetTower = int.Parse(tokens[FIRST_AVAILABLE_INDEX+1]);
		this.targetSection = int.Parse(tokens[FIRST_AVAILABLE_INDEX+2]);
	}
	
	public override void Perform() {
		ValueStore.helpMessage = "Firing weapon";
		Player firingPlayer = TurnOrder.GetPlayerByNumber(playerNumber);
		Player target = playerNumber == 1 ? TurnOrder.GetPlayerByNumber(2) : TurnOrder.GetPlayerByNumber(1);
		Section sc = firingPlayer.GetTower(towerNumber).GetSection(firingSection);
		int index = sc.attributes.height;
		int hitCenter = index + targetSection;
		List<Section> hitSections = sc.attributes.weapon.GetEffect().GetDamagedSections(target.GetTower(targetTower), hitCenter);
		WeaponAnimator.Animate(sc, hitSections);
		int damage = sc.attributes.weapon.GetDamage();
		sc.attributes.weapon.GetEffect().DoDamage(target.GetTower(targetTower), hitCenter, damage);
	}
}
