using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Burned : SectionEffect
{
	public int damagePerTurn = 40;
	public int numTurns = 3;
	public Burned() : base() {
		this.effectType = "Force Field";
		//visually show force field
	}
	
	public void EndTurnEffect(){
		Damage (damagePerTurn);
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
}