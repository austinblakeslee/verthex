using UnityEngine;
using System;
using System.Collections;

public class Upgrade : TurnAction {

	public int sectionNum;
	public int upgradeChoice;
	
	private static string[] upgradeChoices = new string[10] {"Damage", "AoE", "DoT", "Drain", "Paralyze", "Tag", "Poison", "Alter Weight", "Force Field", "Blind"};
	
	public Upgrade(string actionMessage) : base("Upgrade") {
		ParseActionMessage(actionMessage);
	}

	public Upgrade(int t, int n, string u) : base("Upgrade") {
		this.towerNumber = t;
		this.playerNumber = TurnOrder.myPlayer.playerNumber;
		this.sectionNum = n;
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
				weapon.SetWeaponEffect(new AreaOfEffect(weapon));
			}
		} else if(upgrade == "DoT") {
			if(weapon.GetEffect().GetEffectType() == "Burn") {
				weapon.GetEffect().Upgrade();
			} else if(weapon.GetEffect().GetEffectType() == "none") {
				weapon.SetWeaponEffect(new Burn(weapon));
			}
		} else if (upgrade == "Tag")
		{
			weapon.SetWeaponEffect(new AimCritStrike(weapon));
		} else if (upgrade == "Drain")
		{
			weapon.SetWeaponEffect(new Drain(weapon));
		}
		else if (upgrade == "Paralyze")
		{
			weapon.SetWeaponEffect (new Paralyze(weapon));
		}
		else if (upgrade == "Poison")
		{
			weapon.SetWeaponEffect(new PoisonSplash(weapon));
		}
		else if (upgrade == "Alter Weight")
		{
			weapon.SetWeaponEffect(new AlterWeight(weapon));
		}
		else if (upgrade == "Force Field")
		{
			weapon.SetWeaponEffect(new ApplyForceField(weapon));
		}
		else if (upgrade == "Blind")
		{
			weapon.SetWeaponEffect(new Blind(weapon));
		}
		else{
			Debug.Log ("Error - Upgrade is probably tagged improperly");
		}
	}
}
