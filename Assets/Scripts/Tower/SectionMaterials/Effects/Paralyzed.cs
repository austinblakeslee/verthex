using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Paralyzed : SectionEffect 
{
	public int numTurns = 2;
	public GameObject paralyzedVisual;

	public Paralyzed(Section s) : base(s)
	{
		this.effectType = "Paralyzed";
		CombatLog.addLine("Weapon is stunned.  It won't deal damage until it is repaired.");
		CombatLog.addLine("Paralyzed Section Transform" + s.transform.position);
		s.attributes.weapon.fire = false; //makes damage = 0... Will change to more permanent solution later
		paralyzedVisual = GameObject.Instantiate(GameValues.visualEffects["paralyzedVisual"], new Vector3(s.transform.position.x,  s.transform.position.y+s.transform.localScale.y/2, s.transform.position.z), s.transform.rotation) as GameObject;

	}
		
	public override void EndTurnEffect(){
		if (numTurns <= 0)
		{
			appliedSection.attributes.weapon.fire = true; 
			appliedSection.attributes.material.SetSectionEffect(new DefaultSectionEffect(appliedSection));
			Object.Destroy(paralyzedVisual);

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