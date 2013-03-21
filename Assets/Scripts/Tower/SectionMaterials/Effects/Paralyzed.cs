using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Paralyzed : SectionEffect 
{
	public int numTurns = 1;
	public Material paralyzedVisual;
	public Material defaultMaterial;

	public Paralyzed(Section s) : base(s)
	{
		this.effectType = "Paralyzed";
		CombatLog.addLine("Weapon is stunned.  It won't deal damage until it is repaired.");
		CombatLog.addLine("Paralyzed Section Transform" + s.transform.position);
		s.attributes.weapon.fire = false; //makes damage = 0... Will change to more permanent solution later
		//paralyzedVisual = GameObject.Instantiate(GameValues.visualEffects["paralyzedVisual"], new Vector3(s.transform.position.x,  s.transform.position.y+s.transform.localScale.y/2, s.transform.position.z), s.transform.rotation) as GameObject;
		paralyzedVisual = GameValues.visualEffects["paralyzedVisual"] as Material;
		foreach(Renderer r in s.GetComponentsInChildren<Renderer>()) {
		if(r.material.shader.name == "Diffuse") {
				r.material = paralyzedVisual;
				r.material.mainTexture = s.attributes.material.texture;
				Debug.Log (r.material.mainTexture.name);
				//r.material.SetFloat("_Speed", .1f);
				//r.material.SetFloat("_FallOff", .5f);
				//r.material.SetFloat("_Width", .1f);
				//r.material.SetFloat("_OutlineColorFallOdd", 1.0f);
				//r.material.SetTexture("_Ramp", GameValues.visualEffects["paralyzedRamp"] as Texture);
				//r.material.SetTexture ("_Noise", GameValues.visualEffects["paralyzedNoise"] as Texture);
				//r.material.SetTexture("_MainTex", tex);
			}
		}
	}
		
	public override void EndTurnEffect(){
		if (numTurns <= 0)
		{
			appliedSection.attributes.weapon.fire = true; 
			appliedSection.attributes.material.SetSectionEffect(new DefaultSectionEffect(appliedSection));
			Destruct ();			
		}
		else
		{
			Debug.Log ("Num turns of Paralysis left: " + numTurns);
		}
		numTurns --;
	}
	public override string GetInfo ()
	{
		return "Paralyzed for " + (numTurns+1) + " more turns.";
	}
	public override void Destruct ()
	{
		foreach(Renderer r in appliedSection.GetComponentsInChildren<Renderer>()) {
			if(r.material.shader.name == paralyzedVisual.shader.name) {
				r.material.shader = Shader.Find("Diffuse");
			}
		}
	}
}