using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ForceFieldEffect : SectionEffect
{
	public int forceFieldStrength = 40;
	public int forceFieldTurnLength = 3;
	public ForceFieldEffect() : base() {
		this.effectType = "Force Field";
		//visually show force field
	}
		
	public void Damage(int power)
	{
		if (power > forceFieldStrength)
		{
			//destroy force field visually
			section.SubtractSP(power - forceFieldStrength);
		}
		else{
			//display forceField being attacked
			forceFieldStrength -= power;
		}
	}
	
	public void EndTurnEffect(){
		if (forceFieldTurnLength <= 0)
		{
			//Destroy(this); - Destroy this Effect at the end of it's last turn
		}
		forceFieldTurnLength --;

	}
	public override string GetInfo (int damage)
	{
		throw new System.NotImplementedException ();
	}
}

