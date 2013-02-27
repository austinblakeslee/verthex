using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SectionWeapon {
	public bool fire = true;
	
	protected int damage;
	protected int spcost;
	public int cost;
	protected int weight;
	protected int range;
	public string wtype;
	public int maxUpgradeLevel = 3;
	protected int upgradeLevel = 0;
	protected List<float> damageUpgrades = new List<float> {1.0f, 1.2f, 1.3f, 1.4f, 1.5f};
	protected List<int> rangeUpgrades = new List<int>{0,0,1,1,2};
	protected WeaponEffect maxUpgradeEffect = new DefaultWeaponEffect();
	protected WeaponEffect weaponEffect = new DefaultWeaponEffect();
	
	public static string[] weapons = new string[4] {"Nothing", "Ballista", "Catapult", "Cannon"}; //?
	
	public virtual GameObject GetPrefab() {
		return Resources.Load(wtype) as GameObject;
	}

	public string GetWeaponType() {
	    return wtype;
	}
	
	public int GetUpgradeLevel() {
		return upgradeLevel;
	}
	
	public void Upgrade() {//UpgradeLevel()
		if (upgradeLevel < maxUpgradeLevel){
		 	upgradeLevel++;
		}
		if (upgradeLevel == maxUpgradeLevel){
			SetWeaponEffect(maxUpgradeEffect);
		}
	}

	public int GetDamage() {
		if (fire)
	    	return (int)(damage * damageUpgrades[upgradeLevel]);
		else
			return 0;
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
	
	public WeaponEffect GetEffect() {
		return weaponEffect;
	}
	
	public void SetWeaponEffect(WeaponEffect e) {
		this.weaponEffect = e;
	}
	public int GetRange()
	{
		return range + rangeUpgrades[upgradeLevel];
	}
}