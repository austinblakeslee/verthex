using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoisonSplash : WeaponEffect {

	public PoisonSplash() : base() {
		this.effectType = "Posion Splash";
	}
	
	public override List<GameObject> GetDamagedSections(Tower t, int center) {
		List<GameObject> list = new List<GameObject>();
		if(center >= 0 && center < t.GetSections().Count) {
			list.Add(t.GetSections()[center]);
		}
		return list;
	}
	
	public override void DoDamage(Tower t, int center, int damage, Tower self, int firingSec) {
		List<GameObject> sections = GetDamagedSections(self, firingSec);
		if(sections.Count >= 1) {
			CombatLog.addLine("Hit section " + (center+1) + " for " + damage + " damage.");
			t.DamageSection(center, damage);
			if (t.GetSection(center).HasWeapon()){
				CombatLog.addLine("Section is confused");
				//t.GetSection(center).GetWeapon().SetEffect(new PoisonedSplash());
				t.GetSection(center).GetMaterial().SetSectionEffect(new Poisoned(t.GetSection(center)));
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
	
	public override string GetInfo(int damage) {
		return "Deals " + damage + " damage to sections above and below the firing section.";
	}
}