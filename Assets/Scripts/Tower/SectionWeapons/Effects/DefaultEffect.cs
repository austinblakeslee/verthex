using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DefaultEffect : Effect {

	public DefaultEffect() : base() {
		this.effectType = "none";
	}
	
	public override List<Section> GetDamagedSections(Tower t, int center) {
		List<Section> list = new List<Section>();
		if(center >= 0 && center < t.GetSections().Count) {
			list.Add(t.GetSection(center));
		}
		Debug.Log ("Sections Count = " + t.GetSections().Count + ". Attacking count = " + list.Count);
		return list;
	}
	
	public override void DoDamage(Tower t, int center, int damage) {
		List<Section> sections = GetDamagedSections(t, center);
		if(sections.Count >= 1) {
			CombatLog.addLine("Hit section " + (center+1) + " for " + damage + " damage.");
			t.GetSection(center).attributes.material.GetSectionEffect().ApplyDamage(t.GetSection(center), damage);
			//t.DamageSection(center, damage);
		} else if(center < 0) {
			CombatLog.addLine("Attack was too low");
			CombatLog.addLine("Fill the aim bar more.");
		} else if(center >= t.GetSections().Count) {
			CombatLog.addLine("Attack was too high");
			CombatLog.addLine("Lower the aim bar.");
		}
	}
	
	public override string GetInfo(int damage) {
		return "Deals " + damage + " single-target damage.";
	}
	
}