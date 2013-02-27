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
		int damageToDo = appliedSection.attributes.weapon.GetDamage() * poisonDamagePercentage / 100;	
		Tower t = appliedSection.attributes.myTower;
		if (appliedSection.attributes.height >= 1)
		{
			appliedSection.attributes.weapon.GetEffect().DoDamage(t, appliedSection.attributes.height - 2, damageToDo, t, appliedSection.attributes.height);
		}
		if(appliedSection.attributes.height >= 0 && appliedSection.attributes.height <= t.GetHeight()) {
			t.GetSection(appliedSection.attributes.height - 1);
		}
	}
	
	public override void EndTurnEffect(){
		if (numTurns <= 0)
		{
			appliedSection.attributes.material.SetSectionEffect(new DefaultSectionEffect());
		}
		numTurns --;

	}
	public override string GetInfo ()
	{
		return "Poisoned for " + numTurns + " more turns.";
	}
}