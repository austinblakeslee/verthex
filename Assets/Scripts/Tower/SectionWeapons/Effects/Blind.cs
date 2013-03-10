using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Blind : WeaponEffect
{
	protected List<Section> taggedSections = new List<Section>();
	protected int missChance = 50;//Percentage of bonus damage a crit strike will do.
			
	public Blind() : base() {
		this.effectType = "Blind";
	}
	public override List<Section> GetDamagedSections(Tower t, int center) {
		List<Section> secs = new List<Section>();
		secs.Add(t.GetSection(center));
		return secs;
	}
		
	public override void DoDamage(Tower t, int center, int damage, Tower self, int firingSection) {
		if(t.GetSections().Count >= 1) {		
			//t.DamageSection(center, damage);
			Section s = t.GetSection (center);
			SectionEffect eff = s.attributes.material.GetSectionEffect();
			eff.ApplyDamage(s, damage);
			if (s.attributes.HasWeapon())
			{
				if (s.attributes.weapon.GetEffect().GetEffectType() != "Blinded")
				{
					s.attributes.weapon.SetWeaponEffect(new Blinded(missChance));
				}
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