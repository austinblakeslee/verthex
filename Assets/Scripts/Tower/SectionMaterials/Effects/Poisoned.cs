using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Poisoned : SectionEffect
{
	public int numAttacks = 1;
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
		if (appliedSection.attributes.height >= 1)
		{
			ApplyDamage(t.GetSection((appliedSection.attributes.height - 1)), damageToDo);
		}
		if(appliedSection.attributes.height >= 0 && appliedSection.attributes.height < t.GetHeight()-1) {
			Debug.Log (appliedSection.attributes.height + " = height");
			ApplyDamage(t.GetSection((appliedSection.attributes.height)+1), damageToDo);
		}
		numAttacks--;
	}
	
	public override void EndTurnEffect(){
		if (numAttacks <= 0)
		{
			appliedSection.attributes.material.SetSectionEffect(new DefaultSectionEffect(appliedSection));
			Destruct();
		}

	}
	public override string GetInfo ()
	{
		return "Poisoned for " + (numAttacks) + " more attacks.";
	}
	public override void Destruct ()
	{
			Object.Destroy(poisonedVisual);
	}
	public override void Construct ()
	{
		if (poisonedVisual == null)
			poisonedVisual = GameObject.Instantiate(GameValues.visualEffects["poisonedVisual"] as GameObject, appliedSection.transform.Find("Center").position, appliedSection.transform.rotation) as GameObject;

			
	}
	
}