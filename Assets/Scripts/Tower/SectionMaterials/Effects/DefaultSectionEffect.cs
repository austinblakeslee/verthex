using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DefaultSectionEffect : SectionEffect
{
	public DefaultSectionEffect(Section effectedSection) : base() {
		this.effectType = "Default";
		appliedSection = effectedSection;
	}
	public override void PreAttack(Section s)
	{
		Debug.Log (s.ToString() + " Pre-attack ");
	}
}