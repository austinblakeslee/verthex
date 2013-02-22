using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Section : MonoBehaviour {

	public SectionAttributes attributes;
	public GameObject dotEffect;
	private GameObject dotEffectInstance;
	public Texture damageTexture;
	public bool damaged = false;
	public AudioClip repairSound;
		
	public void Start () {
		dotEffectInstance = null;
	}

	public void Update () {
		if(!damaged && attributes.sp / (float)attributes.material.initialSP <= 0.5) {
			damaged = true;
			ShowDamage();
		} else if(damaged && attributes.sp / (float)attributes.material.initialSP > 0.5) {
			damaged = false;
			RemoveDamage();
		}
	}
	
	public void PlayRepairSound() {
		AudioSource.PlayClipAtPoint(repairSound, Vector3.zero);
	}
	
	public void ShowDamage() {
		/* laziness is a virtue
		foreach(GameObject module in modules) {
			foreach(Transform t in module.transform) {
				t.renderer.material.shader = Shader.Find("Decal");
				t.renderer.material.SetTexture("_DecalTex", damageTexture);
			}
		}
		foreach(GameObject thing in colorables) {
			thing.renderer.material.shader = Shader.Find("Decal");
			thing.renderer.material.SetTexture("_DecalTex", damageTexture);
		}
		*/
	}
	
	public void RemoveDamage() {
		/* according to me anyway
		foreach(GameObject module in modules) {
			foreach(Transform t in module.transform) {
				t.renderer.material.shader = Shader.Find("Diffuse");
			}
		}
		foreach(GameObject thing in colorables) {
			thing.renderer.material.shader = Shader.Find("Diffuse");
		}
		*/
	}
	
	public int GetStress() {
		return attributes.myTower.GetWeightAboveSection(attributes.height);
	}
	
	public string GetMaterialInfo() {
		int stress = attributes.myTower.GetWeightAboveSection(attributes.height);
		return stress + "/" + attributes.sp + "/" + attributes.material.maxSP;
	}
	
	public string GetWeaponInfo() {
		SectionWeapon weapon = attributes.weapon;
		string effectType = weapon.GetEffect().GetEffectType();
		return weapon.GetWeaponType() + "\nLevel " + (weapon.GetUpgradeLevel() + 1) + (effectType == "none" ? "" : (" - " + effectType)) + "\n" + weapon.GetEffect().GetInfo(weapon.GetDamage());
	}
	
	public string GetDotInfo() {
		Dot d = attributes.myTower.GetDot(this);
		if(d != null) {
			return "BURN! " + d.damagePerTurn + " SP per turn for " + d.turnsRemaining + " turns.";
		} else {
			return "";
		}
	}
	
	public void SetColor(Color c) {
		/* my laziness knows no bounds
		if (modules.Count > 0){
			foreach(GameObject module in modules) {
				foreach(Transform t in module.transform) {
					t.renderer.material.color = c;
				}
			}
			foreach(GameObject thing in colorables) {
				thing.renderer.material.color = c;
			}
		}
		*/
	}
	
	public void DotApplied() {
		if(dotEffectInstance == null) {
			dotEffectInstance = (GameObject)GameObject.Instantiate(dotEffect, transform.position, transform.rotation);
		}
	}
	
	public bool HasDot() {
		return dotEffectInstance != null;
	}
	
	public void DotFinished() {
		if(dotEffectInstance != null) {
			Object.Destroy((Object)dotEffectInstance);
		}
	}
}