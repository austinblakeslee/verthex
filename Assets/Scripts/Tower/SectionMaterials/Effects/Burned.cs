using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Burned : SectionEffect
{
	public int damagePerTurn;
	public int numTurns = 3;

	public Burned(int _damagePerTurn, Section _burnedSection)
	{
		this.effectType = "Burned";
		appliedSection = _burnedSection;
		damagePerTurn = _damagePerTurn;
	}
	
	
	public override void EndTurnEffect(){
		//Damage (damagePerTurn);
		if (numTurns <= 0)
		{
			appliedSection.attributes.material.SetSectionEffect(new DefaultSectionEffect(appliedSection));	//destroy this script/apply DefaultEffect to the section 
		}
		numTurns --;
		Debug.Log ("Burn Damage per turn: " + damagePerTurn);
		Debug.Log("Num turns left: " + numTurns);
		ApplyDamage(appliedSection, damagePerTurn);
		//Display burn stuff

	}
	public override string GetInfo ()
	{
		return "Burned for " + (numTurns+1) + " more turns.";
	}
}