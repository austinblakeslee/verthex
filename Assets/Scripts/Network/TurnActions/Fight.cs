using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fight : TurnAction {

	public int firingSection;
	public int targetSection;
	
	public Fight(string actionMessage) : base("Fight") {
		ParseActionMessage(actionMessage);
	}

	public Fight(int f, int t) : base("Fight") {
		this.playerNumber = TurnOrder.myPlayer.playerNumber;
		this.firingSection = f;
		this.targetSection = t;
	}
	
	public override string GetActionMessage() {
		return FormatActionMessage(this.firingSection+"", this.targetSection+"");
	}
	
	protected override void ParseActionMessage(string actionMessage) {
		Debug.Log(actionMessage);
		string[] tokens = actionMessage.Split(TOKEN_SEPARATOR);
		this.playerNumber = int.Parse(tokens[0]);
		this.firingSection = int.Parse(tokens[2]);
		this.targetSection = int.Parse(tokens[3]);
	}
	
	public override void Perform() {
		ValueStore.helpMessage = "Firing weapon";
		Player firingPlayer = TurnOrder.GetPlayerByNumber(playerNumber);
		Player target = playerNumber == 1 ? TurnOrder.GetPlayerByNumber(2) : TurnOrder.GetPlayerByNumber(1);
		SectionController sc = firingPlayer.GetTower().GetSections()[firingSection].GetComponent<SectionController>();
		int index = sc.GetHeight() - 1;
		int hitCenter = index + targetSection;
		List<GameObject> hitSections = sc.GetSection().GetWeapon().GetEffect().GetDamagedSections(target.GetTower(), hitCenter);//hitIndex 
		WeaponAnimator.Animate(sc.gameObject, hitSections);
		int damage = sc.GetSection().GetWeapon().GetDamage();
		sc.GetSection().GetWeapon().GetEffect().DoDamage(target.GetTower(), hitCenter, damage);
	}
}
