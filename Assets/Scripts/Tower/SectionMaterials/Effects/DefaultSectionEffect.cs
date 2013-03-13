using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DefaultSectionEffect : SectionEffect
{
	public DefaultSectionEffect(Section effectedSection) : this() {
		appliedSection = effectedSection;
	}
	public DefaultSectionEffect()
	{
		this.effectType = "Default";

	}
	public override void PreAttack(Section s)
	{
		Debug.Log (s.ToString() + " Pre-attack ");
	}
}