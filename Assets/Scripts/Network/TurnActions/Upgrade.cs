using UnityEngine;
using System;
using System.Collections;

public class Upgrade : TurnAction {

	public int sectionNum;
	public int upgradeChoice;
	
	private static string[] upgradeChoices = new string[3] {"Damage", "AoE", "DoT"};
	
	public Upgrade(string actionMessage) : base("Upgrade") {
		ParseActionMessage(actionMessage);
	}

	public Upgrade(int n, string u) : base("Upgrade") {
		this.playerNumber = TurnOrder.myPlayer.playerNumber;
		this.sectionNum = n;
		this.upgradeChoice = EncodeUpgrade(u);
	}
	
	public override string GetActionMessage() {
		return FormatActionMessage(this.sectionNum+"", this.upgradeChoice+"");
	}
	
	protected override void ParseActionMessage(string actionMessage) {
		string[] tokens = actionMessage.Split(TOKEN_SEPARATOR);
		this.playerNumber = int.Parse(tokens[0]);
		this.sectionNum = int.Parse(tokens[2]);
		this.upgradeChoice = int.Parse(tokens[3]);
	}
	
	private int EncodeUpgrade(string u) {
		return Array.IndexOf(upgradeChoices, u);
	}
	
	private string DecodeUpgrade() {
		return upgradeChoices[upgradeChoice];
	}
	
	public override void Perform() {
		TowerSelection.LocalSelectSection(playerNumber, sectionNum);
		TowerSelection.GetSelectedSection().PlayRepairSound();
		CombatLog.addLine("Upgraded section");
		Player p = TurnOrder.GetPlayerByNumber(playerNumber);
		Section s = p.GetTower().GetSection(sectionNum);
		string upgrade = DecodeUpgrade();
		int cost = 200;
		if(upgrade == "Damage") {
			s.GetWeapon().Upgrade();
			p.RemoveResources(cost);
		} else if(upgrade == "AoE") {
			if(s.GetWeapon().GetEffect().GetEffectType() == "Multi") {
				s.GetWeapon().GetEffect().Upgrade();
				p.RemoveResources(cost);
			} else {
				s.GetWeapon().SetEffect(new AreaOfEffect());
				p.RemoveResources(cost);
			}
		} else if(upgrade == "DoT") {
			if(s.GetWeapon().GetEffect().GetEffectType() == "Burn") {
				s.GetWeapon().GetEffect().Upgrade();
				p.RemoveResources(cost);
			} else if(s.GetWeapon().GetEffect().GetEffectType() == "none") {
				s.GetWeapon().SetEffect(new DamageOverTime());
				p.RemoveResources(cost);
			}
		}
	}
}
