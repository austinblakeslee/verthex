using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class SectionEffect {

	protected string effectType;
	protected int upgradeLevel;
	protected Section appliedSection;
	
	public SectionEffect() {
		upgradeLevel = 0;
	}
	public SectionEffect(Section effectedSection) : this()
	{
		appliedSection = effectedSection;
	}
	
	public virtual void PreAttack(Section S){}
	public virtual void ApplyDamage(Section target, int power)
	{
		CombatLog.addLine("Hit section " + (target.attributes.height + 1) + " for " +power+  " damage.");
		target.attributes.myTower.DamageSection(target.attributes.height, power);
	}

	public virtual void EndTurnEffect(){}
	public virtual string GetInfo()
	{
		return(effectType);
	}
	
	public void Upgrade() {
		upgradeLevel++;
	}
	
	public string GetEffectType() {
		return effectType + ".";
	}
	
	public int GetUpgradeLevel() {
		return upgradeLevel;
	}
	
}
