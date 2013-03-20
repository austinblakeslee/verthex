using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponAnimator : MonoBehaviour {

	private static WeaponAnimator instance;
	public static bool animate;
	private bool splitScreen;
	private Section firingSection;
	private Transform weapon;
	private Transform projectile;
	private Vector3 originalProjectilePosition;
	private Quaternion originalProjectileRotation;
	private List<Section> hitSections;
	public GameObject hitParticle;
	private bool miss = false;
	public Font missFont;
	
	public AudioClip cannonFire;	
	public AudioClip missSound;
	
	public Texture2D splitScreenTexture;
	
	public GUIText damageText;
	private Object cloneDamageText;
	private Vector3 damageLocale = new Vector3(0.5f, 0.5f, 0.0f);
	
	void Start() {
		instance = this;
	}
	
	public void AnimateWeapon() {
		foreach(Transform child in weapon) {
			Animation a = child.GetComponent<Animation>();
			if(a != null) {
				a.Play();
			}
		}
	}
	
	public void PlayFiringWeaponSound() {
		AudioSource.PlayClipAtPoint(cannonFire, new Vector3(0,0,0));
	}
	
	private void PlayMissSound() {
		AudioSource.PlayClipAtPoint(missSound, new Vector3(0,0,0));
	}
	
	private IEnumerator Animate() {
		Weapon w = weapon.GetComponent<Weapon>();
		AnimateWeapon();
		yield return new WaitForSeconds(w.projectileTimeout);
		PlayFiringWeaponSound();
		if(miss) {
			yield return new WaitForSeconds(w.missTimeout);
			PlayMissSound();
			yield return new WaitForSeconds(2.0f);
		} else {
			Section hitSection = hitSections[hitSections.Count/2];
			Projectile p = (Instantiate(w.projectile.gameObject, weapon.position, weapon.rotation) as GameObject).GetComponent<Projectile>();
			p.SetTarget(hitSection);
			yield return new WaitForSeconds(w.cameraSwitchTimeout);
			weapon.Find("FireCam").camera.enabled = false;
			hitSection.transform.Find("HitCam").camera.enabled = true;
			while(p.inProgress) {
				yield return new WaitForSeconds(0.3f);
			}
			yield return StartCoroutine(p.ImpactTarget());
			hitSection.transform.Find("HitCam").camera.enabled = false;
		}
		animate = false;
	}
	
	void Update () {
	}
	
	private void FindWeaponAndProjectile() {
		foreach(Transform child in firingSection.transform) {
			if(child.tag == "Weapon") {
				weapon = child;
				if(child.GetComponent<OrbitingWeapon>() != null) {
					child.GetComponent<OrbitingWeapon>().orbiting = false;
				}
				projectile = child.Find("Projectile");
				originalProjectilePosition = projectile.position;
				originalProjectileRotation = projectile.rotation;
				return;
			}
		}
	}
	
	private void InitializeCameras() {
		GameObject.FindWithTag("MainCamera").camera.enabled = false;
		GameObject.FindWithTag("MainMenu").GetComponent<Menu>().on = false;
		GameObject.FindWithTag("MainCamera").GetComponent<TowerSelection>().deselectSection();
		
		splitScreen = true;
		weapon.transform.Find("FireCam").camera.enabled = true;
	}
	
	private void Breakdown() {
		projectile.position = originalProjectilePosition;
		projectile.rotation = originalProjectileRotation;
		GameObject.FindWithTag("MainMenu").GetComponent<Menu>().disabled = false;
		GameObject.FindWithTag("MainCamera").camera.enabled = true;
		GameObject.FindWithTag("MainCamera").GetComponent<TowerSelection>().reselectSection();
		splitScreen = false;
		miss = false;
		GameObject.FindWithTag("MainMenu").GetComponent<Menu>().on = true;
		OrbitingWeapon ow = projectile.parent.GetComponent<OrbitingWeapon>();
		if(ow != null) {
			ow.orbiting = true;
		}
	}
	
	public void BeginAnimation(Section firingSection, List<Section> hitSections) {
		animate = true;
		this.firingSection = firingSection;
		this.hitSections = hitSections;
		FindWeaponAndProjectile();
		InitializeCameras();
		Animate();
		Breakdown();
	}
	
	public static void Animate(Section firingSection, List<Section> hitSections) {
		instance.BeginAnimation(firingSection, hitSections);
	}
	
	public bool AnimationComplete() {
		return !animate;
	}
	
	public bool getSplitScreen() {
		return splitScreen;
	}
	
	void OnGUI() {
		if(splitScreen) {
			GUI.DrawTexture(new Rect(Screen.width/2-5, 0, 10, Screen.height), splitScreenTexture);
			if(miss) {
				GUIStyle style = new GUIStyle();
				style.font = missFont;
				style.fontSize = 60;
				style.normal.textColor = Color.white;
				GUI.Label(new Rect(Screen.width * 5/8, Screen.height/2 - 30, 250, 80), "MISS!", style);
			}
		}
	}
}
