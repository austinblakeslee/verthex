using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Poisoned : SectionEffect
{
	public int numTurns = 3;
	public int poisonDamagePercentage = 35;
	public GameObject poisonedVisual;

	public Poisoned(Section s) : base(s)
	{
		this.effectType = "Poisoned";
		poisonedVisual = GameObject.Instantiate(GameValues.visualEffects["poisonedVisual"] as GameObject, s.transform.Find("Center").position, s.transform.rotation) as GameObject;
	}	
	
	public override void PreAttack(Section s)
	{
		int damageToDo = appliedSection.attributes.weapon.GetDamage() * poisonDamagePercentage / 100;	
		Tower t = appliedSection.attributes.myTower;
		if (appliedSection.attributes.height > 1)
		{
			ApplyDamage(t.GetSection((appliedSection.attributes.height - 0)), damageToDo);
		}
		if(appliedSection.attributes.height >= 0 && appliedSection.attributes.height < t.GetHeight()) {
			Debug.Log ("Not on top");
			ApplyDamage(t.GetSection((appliedSection.attributes.height)), damageToDo);
		}
	}
	
	public override void EndTurnEffect(){
		if (numTurns <= 0)
		{
			appliedSection.attributes.material.SetSectionEffect(new DefaultSectionEffect(appliedSection));
			Destruct();
		}
		numTurns --;

	}
	public override string GetInfo ()
	{
		return "Poisoned for " + (numTurns+1) + " more turns.";
	}
	public override void Destruct ()
	{
			Object.Destroy(poisonedVisual);
	}
}