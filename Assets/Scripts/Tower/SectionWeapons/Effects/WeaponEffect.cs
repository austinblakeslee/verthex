using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class WeaponEffect {

	protected string effectType;
	protected int upgradeLevel;
	
	public WeaponEffect() {
		upgradeLevel = 0;
	}
	
	public abstract List<Section> GetDamagedSections(Tower t, int center);
	public abstract void DoDamage(Tower opp, int oppCenter, int damage, Tower self, int firingSec);
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
