using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SectionWeapon {
	protected int damage;
	protected int spcost;
	public int cost;
	protected int weight;
	protected int range;
	public string wtype;
	protected int upgradeLevel = 0;
	protected List<float> damageUpgrades = new List<float> {1.0f, 1.2f, 1.3f, 1.4f, 1.5f};
	protected List<int> rangeUpgrades = new List<int>{0,0,1,1,2};
	protected List<int> upgradeCosts = new List<int>{75, 100, 150, 250, 400};
	protected Effect upgradeEffect;
	protected Effect weaponEffect = new DefaultEffect();
	protected GameObject model;

	public string GetWeaponType() {
	    return wtype;
	}
	
	public int GetUpgradeLevel() {
		return upgradeLevel;
	}
	
	public void Upgrade() {
		upgradeLevel++;
	}

	public int GetDamage() {
	    return (int)(damage * damageUpgrades[upgradeLevel]);
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
	
	public Effect GetEffect() {
		return weaponEffect;
	}
	
	public void SetEffect(Effect e) {
		this.weaponEffect = e;
	}
	public int GetRange()
	{
		return range + rangeUpgrades[upgradeLevel];
	}
	
	public Effect getUpgradeEffect()
	{
		return upgradeEffect;
	}
	public int GetCurrentUpgradeCost()
	{
		return upgradeCosts[upgradeLevel];
	}
	public GameObject GetModel()
	{
		return model;
	}
	public void SetModel(GameObject _model)
	{
		model = _model;
	}

}
