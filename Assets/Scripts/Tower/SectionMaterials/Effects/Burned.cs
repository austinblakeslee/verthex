using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Burned : SectionEffect
{
	public int damagePerTurn;
	public int numTurns = 3;
	public GameObject burnedVisualEffect;

	public Burned(int _damagePerTurn, Section s, int turns)
	{
		this.effectType = "Burned";
		appliedSection = s;
		damagePerTurn = _damagePerTurn;
		numTurns = turns;
		burnedVisualEffect = GameObject.Instantiate(GameValues.visualEffects["burnedVisual"] as GameObject, s.transform.Find("Center").position, s.transform.rotation) as GameObject;
	}
	
	
	public override void EndTurnEffect(){
		//Damage (damagePerTurn);
		if (numTurns <= 0)
		{
			Destruct ();
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
		return "Burned for " + (numTurns) + " more turns.";
	}
	public override void Destruct ()
	{
		Object.Destroy(burnedVisualEffect);
	}
	
}
