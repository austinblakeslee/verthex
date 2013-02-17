using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AimCritStrike : Effect
{
	protected List<Section> taggedSections = new List<Section>();
	protected int maxTaggedSections = 2;
	protected int critStrikeDamageModifier = 50;//Percentage of bonus damage a crit strike will do.
			
	public override List<GameObject> GetDamagedSections(Tower t, int center) {
		return new List<GameObject>();
	}
		
	public override void DoDamage(Tower t, int center, int damage, Tower self, int firingSection) {
		if (center >= 0 && center < t.GetSections().Count){
			if (taggedSections.Contains(t.GetSection(center)))
			{
				
				taggedSections.Remove(t.GetSection(center));
				CombatLog.addLine("Crit Strike! Hit section " + (center + 1) + " for " + damage + "(+" + (damage * critStrikeDamageModifier/100) + ") damage." );
				t.DamageSection(center, damage + damage * critStrikeDamageModifier / 100);
			}
			else
			{
				taggedSections.Add(t.GetSection(center));
				CombatLog.addLine("Hit section " + (center + 1) + " for " + damage + " damage. Section is tagged for a crit strike" );

				t.DamageSection(center, damage);
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