using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Blinded : WeaponEffect {
	public int numAttacks = 3;
	public int missPercentage = 50;
	public Section appliedSection;
	public GameObject blindedVisual;
	
	public Blinded(int blindPercentage, SectionWeapon effectedWeapon, Section s) : base(effectedWeapon){
		this.effectType = "Blinded";
	}
	
	public Blinded(int blindPercentage, SectionWeapon effectedWeapon) : base(effectedWeapon)
	{
		missPercentage = blindPercentage;
	}
	
	
	public override List<Section> GetDamagedSections(Tower t, int center) {
		int ranNum1 = Random.Range(1, Mathf.RoundToInt(100/missPercentage) + 1);
		List<Section> list = new List<Section>();

		if (ranNum1 == 1)//If missed, miss
		{
			int ranNum2 = Random.Range(1, 3);
			CombatLog.addLine ("ran1 = " + ranNum1 + ". ran2 = " + ranNum2);
			if (ranNum2 == 1)//miss high
			{
				CombatLog.addLine("Miss Higher (blind)");
				if (center + 1 < t.GetHeight())//Missed above tower
				{
					list.Add(t.GetSection(center+1));
				}
			}
			else//miss low
			{
				CombatLog.addLine("Miss Lower (blind)");
				if (center - 1 >= 0) //Missed below tower
				{
					list.Add (t.GetSection(center - 1));
				}
			}
		}
		else{
				list.Add(t.GetSection(center));
			}
		return list;
	}


	public override void DoDamage(Tower t, int center, int damage, Tower self, int firingSec) {
		List<Section> sections = GetDamagedSections(t, center);
		if(sections.Count >= 1) {
			Section section = sections[0];
			CombatLog.addLine("Hit section " + (section.attributes.height + 1) + " for " + damage + " damage.");
			//t.DamageSection(center, damage);
			section.attributes.material.GetSectionEffect().ApplyDamage(section, damage);
		} else if(center < 0) {
			CombatLog.addLine("Attack was too low");
		} else if(center >= t.GetSections().Count) {
			CombatLog.addLine("Attack was too high");
		}
		numAttacks--;
		if (numAttacks <= 0)
		{
			self.GetSection(firingSec).attributes.weapon.SetWeaponEffect(new DefaultWeaponEffect(self.GetSection(firingSec).attributes.weapon));
			Destruct();		
		}
	}
	
	public override string GetInfo(int damage) {
		return "Blinded. " +missPercentage + "% Chance to miss target. " + damage + " single-target damage.";
	}
	public override void Destruct()
	{
		Object.Destroy(blindedVisual);
	}
	public override void Construct()
	{
		if (blindedVisual == null)
		{
			blindedVisual = GameObject.Instantiate(GameValues.visualEffects["blindedVisual"] as GameObject, appliedSection.transform.Find("Center").position, appliedSection.transform.rotation) as GameObject;
		}
	}
	
}

