using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class SectionEffect {

	protected string effectType;
	protected int upgradeLevel;
	protected Section section;
	
	public SectionEffect() {
		upgradeLevel = 0;
	}
	
	public void PreAttack();
	public void Damage(int power)
	{
		section.SubtractSP(power);
	}
	public void EndTurnEffect(){}
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
