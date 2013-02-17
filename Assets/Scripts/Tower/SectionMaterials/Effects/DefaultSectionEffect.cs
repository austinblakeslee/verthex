using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DefaultSectionEffect : SectionEffect
{
	public DefaultSectionEffect() : base() {
		this.effectType = "Default";
	}
		
	public void Damage(int power)
	{
		section.SubtractSP(power);
	}
	
	public override void PreAttack ()
	{
		throw new System.NotImplementedException ();
	}
	public override string GetInfo (int damage)
	{
		throw new System.NotImplementedException ();
	}
	public override void PreDefend ()
	{
		throw new System.NotImplementedException ();
	}
}

