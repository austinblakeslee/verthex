using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tagged : SectionEffect
{
	public int critStrikeBonusPercentage = 40;
	public Tagged() : base() {
		this.effectType = "Tagged";
		//visually show the tag
	}
	public Tagged(int critBonusPercent) : this()
	{
		critStrikeBonusPercentage = critBonusPercent;
	}
	
	public override string GetInfo (int damage)
	{
		throw new System.NotImplementedException ();
	}
	public override void ApplyDamage (Section s, int power)
	{
		base.ApplyDamage (s, power + power * critStrikeBonusPercentage / 100);
		//Destroy this effect - One time use... unless we want it destroyed at the end of the turn...
	}
}