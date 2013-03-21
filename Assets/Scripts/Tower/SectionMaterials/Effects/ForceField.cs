using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ForceFieldEffect : SectionEffect
{
	public int forceFieldStrength = 100;
	public int numTurns = 2;
	public GameObject forceFieldVisual;
	
	public ForceFieldEffect(Section s, int fieldStrength) : base(s)
	{
		this.effectType = "Force Field";
		forceFieldStrength = fieldStrength;

	}
		
	public override void ApplyDamage(Section s, int power)
	{
		if (power >= forceFieldStrength)
		{
			//destroy force field visually
			s.attributes.sp -= (power - forceFieldStrength);
			s.attributes.material.SetSectionEffect(new DefaultSectionEffect(s));
			Destruct ();
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
			Destruct();
		}
		numTurns --;
		Debug.Log ("Force Field Strength: " + forceFieldStrength);
		Debug.Log("Num turns left: " + numTurns);

	}
	public override string GetInfo ()
	{
		return "Force Field (+" + forceFieldStrength + ") for " + (numTurns+1) + " more turns.";
	}
	 public override void Destruct ()
	{
		Object.Destroy(forceFieldVisual);
	}
	public override void Construct ()
	{
		if (forceFieldVisual == null)
			forceFieldVisual = GameObject.Instantiate(GameValues.visualEffects["forceFieldVisual"] as GameObject, appliedSection.transform.Find("Center").position, appliedSection.transform.rotation) as GameObject;
	}
}

