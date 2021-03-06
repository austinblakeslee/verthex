using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponAnimator : MonoBehaviour {

	private static WeaponAnimator instance;
	public static bool animate;
	private string animationStage;
	private bool animationComplete;
	private bool splitScreen;
	private Section firingSection;
	private Vector3 originalProjectilePosition;
	private Quaternion originalProjectileRotation;
	private List<Section> hitSections;
	private List<Camera> sectionCams;
	private List<Vector3> sectionPos;
	public GameObject hitParticle;
	private int sectionCounter = 0;
	private float fireTime;
	private float pausePeriod = 1.5f;
	public float projectileSpeed = 5.0f;
	public float shakeSpeed = 0.003f;
	private float shakeDelta = 0.0f;
	private Vector3 hitSectionOriginalPosition;
	private RigidbodyConstraints defaultConstraints;
	private int numShakes;
	private bool miss = false;
	public Font missFont;
	
	public AudioClip ballistaFire;
	public AudioClip ballistaHit;
	public AudioClip catapultFire;
	public AudioClip catapultHit;
	
	//Actual Sounds we're using
	public AudioClip arrowsFire;
	public AudioClip blasterFire;
	public AudioClip cannonFire;
	public AudioClip cannonHit;
	public AudioClip disintegrationFire;
	public AudioClip eyeFire;
	public AudioClip gattlingFire;
	public AudioClip pistolFire;
	public AudioClip spirit1Fire;
	public AudioClip spirit2Fire;
	
	public AudioClip missSound;
	
	public Texture2D splitScreenTexture;
	
	public GUIText damageText;
	private Object cloneDamageText;
	private Vector3 damageLocale = new Vector3(0.5f, 0.5f, 0.0f);
	
	void Start() {
		instance = this;
	}
	
	public void AnimateWeapon() {
		foreach(Transform child in firingSection.transform) {
			if(child.tag == "Weapon") {
				foreach(Transform child2 in child.transform) {
					Animation a = child2.GetComponent<Animation>();
					if(a != null) {
						a.Play();
					}
				}
			}
		}
	}
	
	public void PlayFiringWeaponSound() {
		AudioClip c = null;
		string weaponType = firingSection.GetComponent<Section>().attributes.weapon.GetWeaponType();
		if(weaponType == "Arrows")
		{
			c = arrowsFire;
		}
		else if(weaponType == "Ballista")
		{
			c = ballistaFire;
		}
		else if(weaponType == "Blaster")
		{
			c = blasterFire;
		}
		else if(weaponType == "Catapult")
		{
			c = catapultFire;
		}
		else if(weaponType == "Cannon")
		{
			c = cannonFire;
		}
		else if(weaponType == "Disintegration Beam")
		{
			c = disintegrationFire;
		}
		else if(weaponType == "Eye Blaster")
		{
			c = eyeFire;
		}
		else if(weaponType == "Gattling Gun")
		{
			c = gattlingFire;
		}
		else if(weaponType == "Pistols")
		{
			c = pistolFire;
		}
		else if(weaponType == "Spirit 1")
		{
			c = spirit1Fire;
		}
		else if(weaponType == "Spirit 2")
		{
			c = spirit2Fire;
		}
		
		AudioSource.PlayClipAtPoint(cannonFire, new Vector3(0,0,0));
	}
	
	public void PlayWeaponHitSound() {
		AudioClip c = null;
		string weaponType = firingSection.GetComponent<Section>().attributes.weapon.GetWeaponType();
		if(weaponType == "Ballista") {
			c = ballistaHit;
		} else if(weaponType == "Catapult") {
			c = catapultHit;
		} else if(weaponType == "Cannon") {
			c = cannonHit;
		}
		if (GetComponent("Audio Source") != null)
		{
			AudioSource.PlayClipAtPoint(c, new Vector3(0,0,0));
		}
	}
	
	void Update () {
		if(animate) {
			Transform projectile = null;
			foreach(Transform child in firingSection.transform) {
				if(child.tag == "Weapon") {
					if(child.GetComponent<OrbitingWeapon>() != null) {
						child.GetComponent<OrbitingWeapon>().orbiting = false;
					}
					projectile = child.Find("Projectile");
				}
			}
			if(animationStage == "fire") {
				GameObject.FindWithTag("MainCamera").camera.enabled = false;
				GameObject.FindWithTag("MainMenu").GetComponent<Menu>().on = false;
				//GameObject.FindWithTag("Help").GetComponent<Menu>().on = false;
				GameObject.FindWithTag("MainCamera").GetComponent<TowerSelection>().deselectSection();
				splitScreen = true;
				
				//Set Cameras so it will not display the DamageText
				//GameObject.FindWithTag("MiniMap").camera.cullingMask &=  ~(1 << LayerMask.NameToLayer("DamageText"));
				firingSection.transform.Find("FireCam").camera.cullingMask &=  ~(1 << LayerMask.NameToLayer("DamageText"));
				firingSection.transform.Find("FireCam").camera.enabled = true;
				
				if(hitSections.Count > 0) {
					firingSection.transform.LookAt(hitSections[0].transform);
					firingSection.transform.eulerAngles = new Vector3(0, firingSection.transform.eulerAngles.y, 0);
					defaultConstraints = hitSections[0].rigidbody.constraints;
					List<Section> hitTowerSects = hitSections[0].GetComponent<Section>().attributes.myTower.GetSections();
					for(int i = 0; i < hitTowerSects.Count; i++) {
						hitTowerSects[i].rigidbody.constraints = RigidbodyConstraints.FreezeAll;
					}
					for(int i = 0; i < hitSections.Count; i++) {
						sectionCams.Add(hitSections[i].transform.Find("HitCam").camera);
						sectionPos.Add(hitSections[i].transform.position);
					}
					hitSections[sectionCounter].transform.Find("HitCam").camera.enabled = true;
					hitSectionOriginalPosition = hitSections[sectionCounter].transform.position;
				} else {
					miss = true;
				}
				animationStage = "firePause";
				originalProjectilePosition = projectile.position;
				originalProjectileRotation = projectile.rotation;
				PlayFiringWeaponSound();
				AnimateWeapon();
				fireTime = 0;
			} else if(animationStage == "firePause") {
				if(fireTime >= pausePeriod) {
					fireTime = 0;
					if(hitSections.Count <= 0) {
						animationStage = "miss";
					} else {
						animationStage = "hit";
					}
				} else {
					fireTime += Time.deltaTime;
					projectile.Translate(new Vector3(0, 0, projectileSpeed));
				}
			} else if(animationStage == "hit") {
				if(sectionCounter < hitSections.Count) {
					Section hitSection = hitSections[sectionCounter];
					hitSection.transform.Find("HitCam").camera.enabled = true;
					projectile.position = hitSection.transform.Find("ProjectileLocation").position;
					projectile.LookAt(hitSection.transform);
					if(sectionCounter != 0) {
						sectionCams[sectionCounter-1].enabled = false;
					}
					hitSectionOriginalPosition = hitSections[sectionCounter].transform.position;
					GameObject.Instantiate(hitParticle, hitSection.transform.position, hitSection.transform.rotation);
					//Create Damage Text to display on screen
					damageText.text = firingSection.GetComponent<Section>().attributes.weapon.GetDamage().ToString();
					cloneDamageText = null;
					if (!firingSection.attributes.weapon.GetEffect().CanAttackSelf()){
						cloneDamageText = Instantiate(damageText, damageLocale, Quaternion.identity);
						PlayWeaponHitSound();
					}
					animationStage = "hitPause";
				} else {
					animationStage = "end";
				}
			} else if(animationStage == "hitPause") {
				if(fireTime >= pausePeriod) {
					numShakes = 0;
					sectionCounter++;
					animationStage = "hit";
					fireTime = 0;
					if (cloneDamageText != null)
						Destroy(cloneDamageText);
				} else {
					fireTime += Time.deltaTime;
					projectile.Translate(new Vector3(0, 0, projectileSpeed));
					Section hitSection = hitSections[sectionCounter];
					sectionCams[sectionCounter].transform.parent = null;
					if(numShakes <= 10) {
						hitSection.transform.Translate(new Vector3(shakeSpeed,0,shakeSpeed));
						shakeDelta += shakeSpeed;
					}
					else {
						hitSection.transform.position = hitSectionOriginalPosition;
					}
					if(Mathf.Abs(shakeDelta) >= Mathf.Abs(shakeSpeed) * 1.5f) {
						numShakes++;
						shakeSpeed = -shakeSpeed;
						shakeDelta = 0.0f;
					}
					hitSection.attributes.material.GetSectionEffect().Construct();
					hitSection.attributes.weapon.GetEffect().Construct();
				}
			} else if(animationStage == "miss") {
				AudioSource.PlayClipAtPoint(missSound, new Vector3(0,0,0));
				animationStage = "end";
			} else if(animationStage == "end") {
				fireTime = 0;
				projectile.position = originalProjectilePosition;
				projectile.rotation = originalProjectileRotation;
				animationStage = "";
				animate = false;
				GameObject.FindWithTag("MainMenu").GetComponent<Menu>().disabled = false;
				GameObject.FindWithTag("MainCamera").camera.enabled = true;
				GameObject.FindWithTag("MainCamera").GetComponent<TowerSelection>().reselectSection();
				firingSection.transform.Find("FireCam").camera.enabled = false;
				splitScreen = false;
				miss = false;
				GameObject.FindWithTag("MainMenu").GetComponent<Menu>().on = true;
				//GameObject.FindWithTag("Help").GetComponent<Menu>().on = true;	
				if(hitSections.Count > 0) {	
					List<Section> hitTowerSects = hitSections[0].GetComponent<Section>().attributes.myTower.GetSections();
					for(int i = 0; i < hitTowerSects.Count; i++) {
						hitTowerSects[i].rigidbody.constraints = defaultConstraints;
					}
					for(int i = 0; i < hitSections.Count; i++) {
						sectionCams[i].transform.parent = hitSections[i].transform;
						hitSections[i].transform.position = sectionPos[i];
					}
					hitSections[sectionCounter-1].transform.Find("HitCam").camera.enabled = false;
				}
				OrbitingWeapon ow = projectile.parent.GetComponent<OrbitingWeapon>();
				if(ow != null) {
					ow.orbiting = true;
				}
			}
		}
	}
	
	public void BeginAnimation(Section firingSection, List<Section> hitSections) {
		this.firingSection = firingSection;
		this.hitSections = hitSections;
		sectionCounter = 0;
		animate = true;
		this.animationStage = "fire";
		this.sectionCams = new List<Camera>();
		this.sectionPos = new List<Vector3>();
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
