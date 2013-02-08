using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Paralyze : Effect {

	public Paralyze() : base() {
		this.effectType = "Paralyze";
	}
	
	public override List<GameObject> GetDamagedSections(Tower t, int center) {
		List<GameObject> list = new List<GameObject>();
		if(center >= 0 && center < t.GetSections().Count) {
			list.Add(t.GetSections()[center]);
		}
		return list;
	}
	
	public override void DoDamage(Tower t, int center, int damage) {
		List<GameObject> sections = GetDamagedSections(t, center);
		if(sections.Count >= 1) {
			CombatLog.addLine("Hit section " + (center+1) + " for " + damage + " damage.");
			t.DamageSection(center, damage);
			Stun(t, center);
			CombatLog.addLine("Section is paralyzed");
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
	
	public void Stun(Tower t, int center){
		Section s = t.GetSection(center);
		s.GetWeapon().fire = false;
		CombatLog.addLine("Weapon is stunned.  It won't deal damage until it is repaired.");
	}
}