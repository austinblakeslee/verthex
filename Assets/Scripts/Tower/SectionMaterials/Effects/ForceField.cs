using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ForceFieldEffect : SectionEffect
{
	public int forceFieldStrength = 40;
	public int numTurns = 3;
	public ForceFieldEffect(Section effectedSection, int fieldStrength) : base(effectedSection)
	{
		this.effectType = "Force Field";
		forceFieldStrength = fieldStrength;
		//visually show force field

	}
		
	public override void ApplyDamage(Section s, int power)
	{
		if (power > forceFieldStrength)
		{
			//destroy force field visually
			s.attributes.sp -= (power - forceFieldStrength);
		}
		else{
			//display forceField being attacked
			forceFieldStrength -= power;
		}
	}
	
	public override void EndTurnEffect(){
		if (numTurns <= 0)
		{
			appliedSection.attributes.material.SetSectionEffect(new DefaultSectionEffect());
		}
		numTurns --;
		Debug.Log ("Force Field Strength: " + forceFieldStrength);
		Debug.Log("Num turns left: " + numTurns);

	}
	public override string GetInfo (int damage)
	{
		throw new System.NotImplementedException ();
	}
}

