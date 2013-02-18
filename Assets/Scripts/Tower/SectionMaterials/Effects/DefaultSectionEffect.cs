using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DefaultSectionEffect : SectionEffect
{
	public DefaultSectionEffect() : base() {
		this.effectType = "Default";
	}
	public override string GetInfo (int damage)
	{
		throw new System.NotImplementedException ();
	}
	public override void PreAttack(Section s)
	{
		Debug.Log (s.ToString() + " Pre-attack ");
	}
	
	public override void EndTurnEffect()
	{
		Debug.Log("Default End Turn effect");
	}
	
}

