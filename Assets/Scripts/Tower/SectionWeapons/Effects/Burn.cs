using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Burn : WeaponEffect {
	public int burnPercentage = 40;
	public Burn(SectionWeapon effectedWeapon) : base(effectedWeapon) { 
		this.effectType = "Burn";
	}
	
	public override List<Section> GetDamagedSections(Tower t, int center) {
		List<Section> list = new List<Section>();
		if(center >= 0 && center < t.GetSections().Count) {
			list.Add(t.GetSections()[center]);
		}
		return list;
	}
	
	public override void DoDamage(Tower t, int center, int damage, Tower self, int firingSec) {
		List<Section> sections = GetDamagedSections(t, center);
		if(sections.Count >= 1) {
			CombatLog.addLine("Hit section " + (center+1) + " for " + damage + " damage.");
			t.GetSection(center).attributes.material.GetSectionEffect().ApplyDamage(t.GetSection(center), damage);		
			t.GetSection(center).attributes.material.GetSectionEffect().Destruct();
			t.GetSection(center).attributes.material.SetSectionEffect(new Burned(damage/3, t.GetSection(center), 2));
			CombatLog.addLine("Section is burned");
		} else if(center < 0) {
			CombatLog.addLine("Attack was too low");
			CombatLog.addLine("Fill the aim bar more.");
		} else if(center >= t.GetSections().Count) {
			CombatLog.addLine("Attack was too high");
			CombatLog.addLine("Lower the aim bar.");
		}
	}
	
	public override string GetInfo(int damage) {
		return "Deals " + damage + " single-target damage and burns the enemy.";
	}
}
