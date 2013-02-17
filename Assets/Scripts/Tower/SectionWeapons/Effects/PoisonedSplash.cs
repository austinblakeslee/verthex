using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoisonedSplash : WeaponEffect {
	protected int poisonedDamageModifier = 30;//Percentage of bonus damage a crit strike will do.

	public PoisonedSplash() : base() {
		this.effectType = "Posioned";
	}
	
	public override List<GameObject> GetDamagedSections(Tower t, int center) {
		List<GameObject> list = new List<GameObject>();
		if(center >= 0 && center < t.GetSections().Count) {
			list.Add(t.GetSections()[center + 1]);
		}
		if (center >= 1)
		{
			list.Add(t.GetSections()[center - 1]);
		}
		return list;
	}
	
	public override void DoDamage(Tower t, int center, int damage, Tower self, int firingSec) {
		List<GameObject> selfSections = GetDamagedSections(self, firingSec);
		foreach(GameObject g in selfSections) {
			int height = g.GetComponent<SectionController>().GetHeight();
			int d = (int)(damage*poisonedDamageModifier/100);
			CombatLog.addLine("Hit own section " + height + " for " + d + " damage.");
			self.DamageSection(height-1, d);
		}
		t.DamageSection(center, damage);
		if(center < 0) {
			CombatLog.addLine("Attack was too low");
			CombatLog.addLine("Fill the aim bar more.");
		} else if(center >= t.GetSections().Count) {
			CombatLog.addLine("Attack was too high");
			CombatLog.addLine("Lower the aim bar.");
		}
	}
	
	public override string GetInfo(int damage) {
		return "Deals " + damage + " damage to sections above and below the firing section.";
	}
}