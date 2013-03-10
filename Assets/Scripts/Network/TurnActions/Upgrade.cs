using UnityEngine;
using System;
using System.Collections;

public class Upgrade : TurnAction {

	public int sectionNum;
	public int upgradeChoice;
	private int tNum;
	private int sNum;
	
	private static string[] upgradeChoices = new string[10] {"Damage", "AoE", "DoT", "Drain", "Paralyze", "Tag", "Poison", "Alter Weight", "Force Field", "Blind"};
	
	public Upgrade(string actionMessage) : base("Upgrade") {
		ParseActionMessage(actionMessage);
	}

	public Upgrade(int t, int n, string u) : base("Upgrade") {
		this.towerNumber = t; //this is getting changed somewhere. Don't feel like figuring out where, Imma just cheat
		tNum = t;
		this.playerNumber = TurnOrder.myPlayer.playerNumber;
		this.sectionNum = n;
		sNum = n; //Same deal
		this.upgradeChoice = EncodeUpgrade(u);
		this.cost = 200;
	}
	
	public override string GetActionMessage() {
		return FormatActionMessage(this.sectionNum+"", this.upgradeChoice+"");
	}
	
	protected override void ParseActionMessage(string actionMessage) {
		string[] tokens = actionMessage.Split(TOKEN_SEPARATOR);
		this.playerNumber = int.Parse(tokens[0]);
		this.towerNumber = int.Parse(tokens[1]);
		Debug.Log ("TNum = " + towerNumber);

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
		Debug.Log ("Applied Tower = " + s.attributes.myTower.faction + ". Section " + s.attributes.height + ". Tower Num = " + towerNumber);

		string upgrade = DecodeUpgrade();
		SectionWeapon weapon = s.attributes.weapon;
		if(upgrade == "Damage") {
			weapon.Upgrade();
		} else if(upgrade == "AoE") {
			if(weapon.GetEffect().GetEffectType() == "Multi") {
				weapon.GetEffect().Upgrade();
			} else {
				weapon.SetWeaponEffect(new AreaOfEffect());
			}
		} else if(upgrade == "DoT") {
			if(weapon.GetEffect().GetEffectType() == "Burn") {
				weapon.GetEffect().Upgrade();
			} else if(weapon.GetEffect().GetEffectType() == "none") {
				weapon.SetWeaponEffect(new Burn());
			}
		} else if (upgrade == "Tag")
		{
			weapon.SetWeaponEffect(new AimCritStrike());
		} else if (upgrade == "Drain")
		{
			weapon.SetWeaponEffect(new Drain());
		}
		else if (upgrade == "Paralyze")
		{
			weapon.SetWeaponEffect (new Paralyze());
		}
		else if (upgrade == "Poison")
		{
			weapon.SetWeaponEffect(new PoisonSplash());
		}
		else if (upgrade == "Alter Weight")
		{
			weapon.SetWeaponEffect(new AlterWeight());
		}
		else if (upgrade == "Force Field")
		{
			weapon.SetWeaponEffect(new ApplyForceField());
		}
		else if (upgrade == "Blind")
		{
			weapon.SetWeaponEffect(new Blind());
		}
		else{
			Debug.Log ("Error - Upgrade is probably tagged improperly");
		}
	}
}
