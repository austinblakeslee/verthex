using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Paralyzed : SectionEffect
{
	public int numTurns = 2;

	public Paralyzed(Section section) : base(section)
	{
		this.effectType = "Paralyzed";
		CombatLog.addLine("Weapon is stunned.  It won't deal damage until it is repaired.");

		section.attributes.weapon.fire = false; //makes damage = 0... Will change to more permanent solution later
	}
		
	public override void EndTurnEffect(){
		if (numTurns <= 0)
		{
			appliedSection.attributes.weapon.fire = true; 
			appliedSection.attributes.material.SetSectionEffect(new DefaultSectionEffect());
			//TODO - Destroy this script so it's not just floating in memory forever			
		}
		else
		{
			Debug.Log ("Num turns of Paralysis left: " + numTurns);
		}
		numTurns --;


	}
	public override string GetInfo ()
	{
		return "Paralyzed for " + (numTurns+1) + " more turns.";
	}
}