using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Blind : WeaponEffect
{
	protected List<Section> taggedSections = new List<Section>();
	protected int missChance = 50;//Percentage of bonus damage a crit strike will do.
	
	
	public Blind(SectionWeapon effectedWeapon) : base(effectedWeapon) {
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
					s.attributes.weapon.GetEffect().Destruct();
					s.attributes.weapon.SetWeaponEffect(new Blinded(missChance, s.attributes.weapon, s));
				}
			}		
		}
		else if(center < 0) {
			CombatLog.addLine("Attack was too low");
		} else if(center >= t.GetSections().Count) {
			CombatLog.addLine("Attack was too high");
		}
	}
	
	public override string GetInfo(int damage)
	{
		return "Deals " + damage + " single target damage, and blinds the target section.";
	}
}