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

		section.GetWeapon().fire = false; //makes damage = 0... Will change to more permanent solution later
	}
		
	public override void EndTurnEffect(){
		if (numTurns <= 0)
		{
			appliedSection.GetWeapon().fire = true; 
			appliedSection.GetMaterial().SetSectionEffect(new DefaultSectionEffect());
		}
		numTurns --;
		Debug.Log ("Num turns of Paralysis left: " + numTurns);

	}
	public override string GetInfo (int damage)
	{
		throw new System.NotImplementedException ();
	}
}