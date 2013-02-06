using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SectionController : MonoBehaviour {
	private Section sec;
	public int height; //1-indexed
	private Player myPlayer;
	public GameObject dotEffect;
	private GameObject dotEffectInstance;
	public List<GameObject> modules;
	public List<GameObject> colorables;
	public Texture damageTexture;
	public bool damaged = false;
	public AudioClip repairSound;
	
		
	public void Start () {
		dotEffectInstance = null;
	}

	public void Update () {
		if(!damaged && sec.GetSP() / (float)sec.GetInitialSP() <= 0.5) {
			damaged = true;
			ShowDamage();
		} else if(damaged && sec.GetSP() / (float)sec.GetInitialSP() > 0.5) {
			damaged = false;
			RemoveDamage();
		}
	}
	
	public void PlayRepairSound() {
		AudioSource.PlayClipAtPoint(repairSound, Vector3.zero);
	}
	
	public void ShowDamage() {
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
	}
	
	public void RemoveDamage() {
		foreach(GameObject module in modules) {
			foreach(Transform t in module.transform) {
				t.renderer.material.shader = Shader.Find("Diffuse");
			}
		}
		foreach(GameObject thing in colorables) {
			thing.renderer.material.shader = Shader.Find("Diffuse");
		}
	}
	
	public int GetStress() {
		return myPlayer.GetTower().GetWeightAboveSection(height-1);
	}
	
	public string GetMaterialInfo() {
		int stress = myPlayer.GetTower().GetWeightAboveSection(height-1);
		return stress + "/" + sec.GetSP() + "/" + sec.GetMaxSP();
	}
	
	public string GetWeaponInfo() {
		SectionWeapon weapon = sec.GetWeapon();
		string effectType = weapon.GetEffect().GetEffectType();
		return weapon.GetWeaponType() + "\nLevel " + (weapon.GetUpgradeLevel() + 1) + (effectType == "none" ? "" : (" - " + effectType)) + "\n" + weapon.GetEffect().GetInfo(weapon.GetDamage());
	}
	
	public string GetDotInfo() {
		Dot d = myPlayer.GetTower().GetDot(this);
		if(d != null) {
			return "BURN! " + d.damagePerTurn + " SP per turn for " + d.turnsRemaining + " turns.";
		} else {
			return "";
		}
	}
	
	public void SetColor(Color c) {
		foreach(GameObject module in modules) {
			foreach(Transform t in module.transform) {
				t.renderer.material.color = c;
			}
		}
		foreach(GameObject thing in colorables) {
			thing.renderer.material.color = c;
		}
	}

	public void SetSection(Section s) {
		sec = s;
	}

	public Section GetSection() {
		return sec;
	}
	
	public void SetHeight(int height) {
		this.height = height;
	}
	
	public int GetHeight() {
		return this.height;
	}
	
	public Player GetPlayer() {
		return this.myPlayer;
	}
	
	public void SetPlayer(Player p) {
		this.myPlayer = p;
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
