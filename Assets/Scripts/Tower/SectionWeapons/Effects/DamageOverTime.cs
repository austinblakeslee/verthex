using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageOverTime : WeaponEffect {

	private List<float> initialDamage = new List<float> {0.5f, 0.5f, 0.6f, 0.8f};
	private List<float> dotDamage = new List<float> {0.6f, 0.7f, 0.8f, 0.8f};

	public DamageOverTime() : base() {
		this.effectType = "Burn";
	}
	
	public override List<Section> GetDamagedSections(Tower t, int center) {
		List<Section> list = new List<Section>();
		if(center >= 0 && center < t.GetSections().Count) {
			list.Add(t.GetSection(center));
		}
		return list;
	}
	
	public override void DoDamage(Tower t, int center, int damage, Tower self, int firingSec) {
		if(center >= 0 && center < t.GetSections().Count) {
			int impact = (int)(damage * initialDamage[upgradeLevel]);
			int dot = (int)(damage * dotDamage[upgradeLevel]);
			CombatLog.addLine("Hit section " + (center+1) + " for " + impact + " damage.");
			CombatLog.addLine("Gave section " + (center+1) + " a " + dot + " damage burn.");
			t.DamageSection(center, impact);
			t.ApplyDot(center, dot);
		} else if(center < 0) {
			CombatLog.addLine("Attack was too low");
			CombatLog.addLine("Fill the aim bar more.");
		} else if(center >= t.GetSections().Count) {
			CombatLog.addLine("Attack was too high");
			CombatLog.addLine("Lower the aim bar.");
		}
	}
	
	public override string GetInfo(int damage) {
		int initialD = (int)(initialDamage[upgradeLevel] * damage);
		int dotD = (int)(dotDamage[upgradeLevel] * damage);
		return "Hits for " + initialD + " then burns for " + dotD + " damage.";
	}
	
}