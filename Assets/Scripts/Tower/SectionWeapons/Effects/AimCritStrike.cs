using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AimCritStrike : WeaponEffect
{
	protected List<Section> taggedSections = new List<Section>();
	protected int critStrikeDamageModifier = 50;//Percentage of bonus damage a crit strike will do.
			
	public override List<GameObject> GetDamagedSections(Tower t, int center) {
		return new List<GameObject>();
	}
		
	public override void DoDamage(Tower t, int center, int damage, Tower self, int firingSection) {
		if(t.GetSections().Count >= 1) {			
			t.DamageSection(center, damage);

			if (t.GetSection(center).GetMaterial().GetSectionEffect().GetEffectType() != "Tagged")
			{	
				t.GetSection(center).GetMaterial().SetSectionEffect(new Tagged(critStrikeDamageModifier));
				CombatLog.addLine("Hit section " + (center + 1) + " for " + damage + " damage. Section is tagged for a crit strike" );

			}
			else{
				CombatLog.addLine("CRIT STRIKE!");
				CombatLog.addLine("Hit section " + (center + 1) + " for " + damage + "(+" + (damage * critStrikeDamageModifier / 100) +  ") damage." );

			}
		}
		else if(center < 0) {
			CombatLog.addLine("Attack was too low");
			CombatLog.addLine("Fill the aim bar more.");
		} else if(center >= t.GetSections().Count) {
			CombatLog.addLine("Attack was too high");
			CombatLog.addLine("Lower the aim bar.");
		}
	}
	
	public override string GetInfo(int damage)
	{
		return "Deals " + damage + " single target damage, and tags the target section for bonus damage on the next attack.";

	}
}