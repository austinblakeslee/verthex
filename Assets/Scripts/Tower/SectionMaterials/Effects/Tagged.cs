using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tagged : SectionEffect
{
	public int critStrikeBonusPercentage;
	public GameObject taggedVisualEffect = null;
	public float orbitSpeed = 55.0f;
	
	
	public Tagged(int critBonusPercent, Section s) : base(s)
	{
		this.effectType = "Tagged";
		
		//visually show the tag
		CombatLog.addLine("Section is tagged for a crit strike" );
		critStrikeBonusPercentage = critBonusPercent;
		taggedVisualEffect = GameObject.Instantiate(GameValues.visualEffects["taggedVisual"] as GameObject, new Vector3(s.transform.Find("Center").position.x,  s.transform.Find("Center").position.y, s.transform.Find("Center").position.z + 40), s.transform.rotation) as GameObject;
	}	
	public override void ApplyDamage (Section s, int power)
	{
		CombatLog.addLine("CRIT STRIKE!");
		base.ApplyDamage (s, power + power * critStrikeBonusPercentage / 100);
		s.attributes.material.SetSectionEffect(new DefaultSectionEffect(appliedSection));
		Destruct();		//Destroy this effect - One time use... unless we want it destroyed at the end of the turn...
	}
	public override void Update ()
	{
		if (taggedVisualEffect != null)
		{
			taggedVisualEffect.transform.RotateAround(appliedSection.transform.position, appliedSection.transform.up, orbitSpeed * Time.deltaTime);	

		}
	}
	public override void Destruct ()
	{
		Object.Destroy(taggedVisualEffect);
	}
	
}