using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Burned : SectionEffect
{
	public int damagePerTurn;
	public int numTurns = 3;
	public Section burnedSection;
	public Burned() : base() {
		this.effectType = "Burned";
		//visually show Initial Burn
	}
	public Burned(int _damagePerTurn, Section _burnedSection) : this()
	{
		burnedSection = _burnedSection;
		damagePerTurn = _damagePerTurn;
	}
	
	
	public override void EndTurnEffect(){
		//Damage (damagePerTurn);
		if (numTurns <= 0)
		{
			burnedSection.attributes.material.SetSectionEffect(new DefaultSectionEffect());	//destroy this script/apply DefaultEffect to the section 
		}
		numTurns --;
		Debug.Log ("Burn Damage per turn: " + damagePerTurn);
		Debug.Log("Num turns left: " + numTurns);
		ApplyDamage(burnedSection, damagePerTurn);
		//Display burn stuff

	}
	public override string GetInfo ()
	{
		return "Burned for " + (numTurns+1) + " more turns.";
	}
}