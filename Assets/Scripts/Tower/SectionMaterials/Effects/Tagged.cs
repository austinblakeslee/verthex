using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tagged : SectionEffect
{
	public int critStrikeBonusPercentage;
	public GameObject taggedVisualEffect;
	public float orbitSpeed = 20.0f;
	
	
	public Tagged(int critBonusPercent, Section s) : base(s)
	{
		this.effectType = "Tagged";
		
		//visually show the tag
		CombatLog.addLine("Section is tagged for a crit strike" );
		critStrikeBonusPercentage = critBonusPercent;
		taggedVisualEffect = GameObject.Instantiate(GameValues.visualEffects["taggedVisual"], new Vector3(s.transform.position.x,  s.transform.position.y+s.transform.localScale.y/2, s.transform.position.z), s.transform.rotation) as GameObject;
		taggedVisualEffect.transform.RotateAround(s.transform.position, s.transform.up, orbitSpeed * Time.deltaTime);	
	}	
	public override void ApplyDamage (Section s, int power)
	{
		CombatLog.addLine("CRIT STRIKE!");
		base.ApplyDamage (s, power + power * critStrikeBonusPercentage / 100);
		s.attributes.material.SetSectionEffect(new DefaultSectionEffect(appliedSection));
		//Destroy this effect - One time use... unless we want it destroyed at the end of the turn...
	}
}