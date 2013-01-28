using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SectionWeapon {
	protected int damage;
	protected int spcost;
	public int cost;
	protected int weight;
	protected int range;
	protected int attackRange;
	public string wtype;
	protected int damageUpgradeLevel = 0;
	protected int rangeUpgradeLevel = 0;
	protected List<float> damageUpgrades = new List<float> {1.0f, 1.2f, 1.3f, 1.4f, 1.5f};
	protected List<int> rangeUpgrades = new List<int>{0,0,1,1,2};
	protected Effect weaponEffect = new DefaultEffect();

	public string GetWeaponType() {
	    return wtype;
	}
	
	public int GetDamageUpgradeLevel() {
		return damageUpgradeLevel;
	}
	
	public void UpgradeDamage() {//UpgradeLevel()
		damageUpgradeLevel++;
		//rangeUpgradeLevel++;
	}

	public int GetDamage() {
	    return (int)(damage * damageUpgrades[damageUpgradeLevel]);
	}

	public int GetSPCost() {
	    return spcost;
	}

	public int GetCost() {
	    return cost;
	}

	public int GetWeight() {
	    return weight;
	}

	public int GetRange() {
	    return range + rangeUpgrades[rangeUpgradeLevel];
	}
	
	public Effect GetEffect() {
		return weaponEffect;
	}
	
	public void SetEffect(Effect e) {
		this.weaponEffect = e;
	}
	public int GetAttackRange()
	{
		return attackRange;
	}
}