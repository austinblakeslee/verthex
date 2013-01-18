using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Effect {

	protected string effectType;
	protected int upgradeLevel;
	
	public Effect() {
		upgradeLevel = 0;
	}
	
	public abstract List<GameObject> GetDamagedSections(Tower t, int center);
	public abstract void DoDamage(Tower t, int center, int damage);
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
