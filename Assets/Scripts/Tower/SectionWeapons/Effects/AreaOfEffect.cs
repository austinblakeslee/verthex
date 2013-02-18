using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AreaOfEffect : Effect {

	private List<int> numTargets = new List<int> {2, 3, 4, 5};
	private List<float> percentDamage = new List<float> {0.5f, 0.4f, 0.35f, 0.32f};

	public AreaOfEffect() : base() {
		this.effectType = "Multi";
	}
	
	public override List<Section> GetDamagedSections(Tower t, int center) {
		int down = numTargets[upgradeLevel]/2;
		int up = down;
		down = numTargets[upgradeLevel]%2 == 0 ? down-1 : down;
		int start = center - down;
		if(start < 0) {
			start = 0;
		}
		int end = center + up;
		if(end >= t.GetSections().Count) {
			end = t.GetSections().Count - 1;
		}
		Debug.Log("Start: " + start + " End: " + end);
		List<Section> list = new List<Section>();
		if(end >= 0) {
			for(int i=start; i <= end; i++) {
				list.Add(t.GetSection(i));
			}
		}
		return list;
	}
	
	public override void DoDamage(Tower t, int center, int damage) {
		List<Section> damagedSections = GetDamagedSections(t, center);
		foreach(Section g in damagedSections) {
			int height = g.attributes.height;
			int d = (int)(percentDamage[upgradeLevel] * damage);
			CombatLog.addLine("Hit section " + height + " for " + d + " damage.");
			t.DamageSection(height-1, (int)(percentDamage[upgradeLevel] * damage));
		}
		if(damagedSections.Count <= 0) {
			if(center < 0) {
				CombatLog.addLine("Attack was too low");
				CombatLog.addLine("Fill the aim bar more.");
			} else if(center >= t.GetSections().Count) {
				CombatLog.addLine("Attack was too high");
				CombatLog.addLine("Lower the aim bar.");
			}
		}
	}
	
	public override string GetInfo(int damage) {
		return "Hits " + numTargets[upgradeLevel] + " targets for " + (int)(percentDamage[upgradeLevel] * damage) + " damage.";
	}
	
}