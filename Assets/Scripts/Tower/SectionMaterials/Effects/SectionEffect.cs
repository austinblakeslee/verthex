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
		target.SubtractSP(power);
	}

	public virtual void EndTurnEffect(){}
	public abstract string GetInfo(int damage);
	
	public void Upgrade() {
		upgradeLevel++;
	}
	
	public string GetEffectType() {
		return effectType;
	}
	
	public int GetUpgradeLevel() {
		return upgradeLevel;
	}
	
}
