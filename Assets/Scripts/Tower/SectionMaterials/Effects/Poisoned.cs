using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Poisoned : SectionEffect
{
	public int numTurns = 3;
	public int poisonDamagePercentage = 35;
	public Poisoned() : base() {

	}
	public Poisoned(Section effectedSection) : base(effectedSection)
	{
		this.effectType = "Poisoned";
		//visually show Confusion/Poison	}
	}	
	
	public override void PreAttack(Section s)
	{
		int damageToDo = s.GetWeapon().GetDamage() * poisonDamagePercentage / 100;	
		Tower t = s.GetTower ();
		if (s.GetHeight() >= 1)
		{
			s.GetWeapon().GetEffect().DoDamage(t, s.GetHeight() - 1, damageToDo, t, s.GetHeight());
		}
		if(s.GetHeight() >= 0 && s.GetHeight() < t.GetSections().Count) {
			s.GetWeapon().GetEffect().DoDamage(t, s.GetHeight() + 1, damageToDo, t, s.GetHeight());
		}
	}
	
	public override void EndTurnEffect(){
		if (numTurns <= 0)
		{
			//destroy this script
		}
		numTurns --;

	}
	public override string GetInfo (int damage)
	{
		throw new System.NotImplementedException ();
	}
}