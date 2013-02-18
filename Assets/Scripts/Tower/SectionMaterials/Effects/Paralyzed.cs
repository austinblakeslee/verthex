using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Paralyzed : SectionEffect
{
	public int numTurns = 2;
	public Paralyzed() : base() {
		this.effectType = "Force Field";
		//visually show force field
	}
	
	public void EndTurnEffect(){
		if (numTurns <= 0)
		{
			//destroy this script
		}
		numTurns --;

	}
	public override string GetInfo (int damage)
	{
		throw new System.NotImplementedException ();
	}
	
	public void Stun(Tower t, int center){
		Section s = t.GetSection(center);
		s.GetWeapon().fire = false;
		CombatLog.addLine("Weapon is stunned.  It won't deal damage until it is repaired.");
	}
}