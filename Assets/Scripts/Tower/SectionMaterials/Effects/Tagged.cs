using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tagged : SectionEffect
{
	public int critStrikeBonusPercentage;
	public Tagged(int critBonusPercent) : base()
	{
		this.effectType = "Tagged";
		//visually show the tag
		CombatLog.addLine("Section is tagged for a crit strike" );

		critStrikeBonusPercentage = critBonusPercent;
	}
	
	public override string GetInfo (int damage)
	{
		throw new System.NotImplementedException ();
	}
	public override void ApplyDamage (Section s, int power)
	{
		CombatLog.addLine("CRIT STRIKE!");
		base.ApplyDamage (s, power + power * critStrikeBonusPercentage / 100);
		s.attributes.material.SetSectionEffect(new DefaultSectionEffect());
		//Destroy this effect - One time use... unless we want it destroyed at the end of the turn...
	}
}