using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Poisoned : SectionEffect
{
	public int numTurns = 3;
	public int poisonDamagePercentage = 35;

	public Poisoned(Section effectedSection) : base(effectedSection)
	{
		this.effectType = "Poisoned";
		//visually show Confusion/Poison	}
	}	
	
	public override void PreAttack(Section s)
	{
		int damageToDo = appliedSection.GetWeapon().GetDamage() * poisonDamagePercentage / 100;	
		Tower t = appliedSection.GetTower ();
		if (appliedSection.GetHeight() >= 1)
		{
			appliedSection.GetWeapon().GetEffect().DoDamage(t, appliedSection.GetHeight() - 2, damageToDo, t, appliedSection.GetHeight());
		}
		if(appliedSection.GetHeight() >= 0 && appliedSection.GetHeight() <= t.GetHeight()) {
			Debug.Log ("HERRREEEEEEEEEEEEEEEEEEEEE");
			t.GetSection(appliedSection.GetHeight() - 1);
		}
	}
	
	public override void EndTurnEffect(){
		if (numTurns <= 0)
		{
			appliedSection.GetMaterial().SetSectionEffect(new DefaultSectionEffect());
		}
		numTurns --;

	}
	public override string GetInfo (int damage)
	{
		throw new System.NotImplementedException ();
	}
}