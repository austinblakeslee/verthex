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
	public override void Construct ()
	{
		if (taggedVisualEffect == null)
			taggedVisualEffect = GameObject.Instantiate(GameValues.visualEffects["taggedVisual"] as GameObject, new Vector3(appliedSection.transform.Find("Center").position.x,  appliedSection.transform.Find("Center").position.y, appliedSection.transform.Find("Center").position.z + 40), appliedSection.transform.rotation) as GameObject;

	}
}