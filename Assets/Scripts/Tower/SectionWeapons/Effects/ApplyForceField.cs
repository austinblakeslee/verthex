using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ApplyForceField : WeaponEffect
{
	protected List<Section> taggedSections = new List<Section>();
	protected int forceFieldStrength = 50;//Percentage of protection the force field will offer
			
	public ApplyForceField(SectionWeapon effectedWeapon) : base(effectedWeapon) {
		this.effectType = "Force Field";
		canAttackSelf = true;
	}
	public ApplyForceField(int _forceFieldStrength, SectionWeapon effectedWeapon) : this(effectedWeapon)
	{
		forceFieldStrength = _forceFieldStrength;
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
			//if force field is not already applied and you're attacking your own tower, apply forceField
			if (s.attributes.material.GetSectionEffect().GetEffectType() != "Force Field" && t.GetPlayerNum() == self.GetPlayerNum())
			{	
				s.attributes.material.SetSectionEffect(new ForceFieldEffect(s, forceFieldStrength));
				CombatLog.addLine("own " + s.attributes.myTower.faction + " section has FF.");
			}
			//if attacking opponents tower, Just do damage
			else if (t.GetPlayerNum() != self.GetPlayerNum())
			{
				CombatLog.addLine("opponent's " + s.attributes.myTower.faction + " section - didn't FF.");
				t.GetSection(center).attributes.material.GetSectionEffect().ApplyDamage(t.GetSection(center), damage);
			}
			else
			{
				Debug.Log("Error with applying force field.");
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