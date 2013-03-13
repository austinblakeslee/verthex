using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ForceFieldEffect : SectionEffect
{
	public int forceFieldStrength = 40;
	public int numTurns = 3;
	public GameObject forceFieldVisual;
	
	public ForceFieldEffect(Section s, int fieldStrength) : base(s)
	{
		this.effectType = "Force Field";
		forceFieldStrength = fieldStrength;
		forceFieldVisual = GameObject.Instantiate(GameValues.visualEffects["forceFieldVisual"], new Vector3(s.transform.position.x,  s.transform.position.y+s.transform.localScale.y/2, s.transform.position.z), s.transform.rotation) as GameObject;

	}
		
	public override void ApplyDamage(Section s, int power)
	{
		if (power > forceFieldStrength)
		{
			//destroy force field visually
			s.attributes.sp -= (power - forceFieldStrength);
			s.attributes.material.SetSectionEffect(new DefaultSectionEffect(s));
			Object.Destroy(forceFieldVisual);

		}
		else{
			//display forceField being attacked
			forceFieldStrength -= power;
		}
	}
	
	public override void EndTurnEffect(){
		if (numTurns <= 0)
		{
			appliedSection.attributes.material.SetSectionEffect(new DefaultSectionEffect(appliedSection));
			Object.Destroy(forceFieldVisual);

		}
		numTurns --;
		Debug.Log ("Force Field Strength: " + forceFieldStrength);
		Debug.Log("Num turns left: " + numTurns);

	}
	public override string GetInfo ()
	{
		return "Force Field (+" + forceFieldStrength + ") for " + (numTurns+1) + " more turns.";
	}
}

