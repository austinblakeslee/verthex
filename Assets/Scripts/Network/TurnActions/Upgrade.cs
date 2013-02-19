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

	public Upgrade(int t, int n, string u) : base("Upgrade") {
		this.towerNumber = t;
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
		this.sectionNum = int.Parse(tokens[FIRST_AVAILABLE_INDEX]);
		this.upgradeChoice = int.Parse(tokens[FIRST_AVAILABLE_INDEX+1]);
	}
	
	private int EncodeUpgrade(string u) {
		return Array.IndexOf(upgradeChoices, u);
	}
	
	private string DecodeUpgrade() {
		return upgradeChoices[upgradeChoice];
	}
	
	public override void Perform() {
		ValueStore.helpMessage = "Upgrading";
		Player p = TurnOrder.GetPlayerByNumber(playerNumber);
		TowerSelection.LocalSelectSection(p.GetTower(towerNumber), sectionNum);
		TowerSelection.GetSelectedSection().PlayRepairSound();
		CombatLog.addLine("Upgraded section");
		Section s = p.GetTower(towerNumber).GetSection(sectionNum);
		string upgrade = DecodeUpgrade();
		int cost = 200;
		SectionWeapon weapon = s.attributes.weapon;
		if(upgrade == "Damage") {
			weapon.Upgrade();
			p.RemoveResources(cost);
		} else if(upgrade == "AoE") {
			if(weapon.GetEffect().GetEffectType() == "Multi") {
				weapon.GetEffect().Upgrade();
				p.RemoveResources(cost);
			} else {
				weapon.SetEffect(new AreaOfEffect());
				p.RemoveResources(cost);
			}
		} else if(upgrade == "DoT") {
			if(weapon.GetEffect().GetEffectType() == "Burn") {
				weapon.GetEffect().Upgrade();
				p.RemoveResources(cost);
			} else if(weapon.GetEffect().GetEffectType() == "none") {
				weapon.SetEffect(new DamageOverTime());
				p.RemoveResources(cost);
			}
		}
	}
}
