using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireAction : DefaultMenuAction,MenuAction {

	public bool isActive = false;
	public bool waitingForAnimation = false;
	public float power = 0.0f;
	private SectionController firingSection;
	private List<GameObject> hitSections;
	public GameObject hitParticle;
	private int hitIndex;
	public Menu fightMenu;
	public List<Menu> toHide;
	
	public Player player;
	public Player target;
	public SectionController selectedSection;
	
	public override void Action() {
		selectedSection = TowerSelection.GetSelectedSection();
		if (isActive) {
			isActive = false;
			GetComponent<PowerBar>().Hide();
			PlayClickSound();
		}
		else if(TurnOrder.IsBattlePhase()) {
			if(selectedSection == null || !TurnOrder.currentPlayer.GetTower().GetSections().Contains(selectedSection.gameObject)) {
				ValueStore.helpMessage = "You must select your own tower section to fire!";
			}
			else if (selectedSection.GetSection().GetWeapon().GetWeaponType() == "Nothing") {
				ValueStore.helpMessage = "You must select a tower section equipped with a weapon before firing!";
			}
			else {
				//need to change it from showing PowerBar to showing arrows (or something equivalent) for who you want to attack
				audio.Play();
				GetComponent<PowerBar>().Show();
				player = TurnOrder.currentPlayer;
				target = TurnOrder.otherPlayer;
				isActive = true;
				fightMenu.on = true;
				foreach(Menu m in toHide) {
					m.on = false;
				}
				//ValueStore.helpMessage = "Hold and release space to fire (fuller bar = higher shot).";
				ValueStore.helpMessage = "Use Up/Down arrows to select targets above and below your level. Press Space to fire. ";
			}
		}
		else {
			ValueStore.helpMessage = "Ceasefire still in effect.";
		}
	}
	
	void Fire(int attackSection) {
		firingSection = selectedSection; //What's the reason for this assignment?
		SectionController sc = firingSection.GetComponent<SectionController>();
		int index = sc.GetHeight() - 1;
		int hitCenter = index + attackSection;
		List<GameObject> hitSections = sc.GetSection().GetWeapon().GetEffect().GetDamagedSections(target.GetTower(), hitCenter);//hitIndex 
		GetComponent<WeaponAnimator>().BeginAnimation(firingSection.gameObject, hitSections, hitParticle);
	}
	
	void Update() {
		if(waitingForAnimation && GetComponent<WeaponAnimator>().AnimationComplete()) {
			Section s = firingSection.GetComponent<SectionController>().GetSection();
			int damage = s.GetWeapon().GetDamage();
			s.GetWeapon().GetEffect().DoDamage(target.GetTower(), hitIndex, damage);
			waitingForAnimation = false;
			GetComponent<PowerBar>().Reset();
			GetComponent<CollapseAnimator>().BeginAnimation(target.GetTower());
			TurnOrder.ActionTaken();
		}
		if(isActive && GetComponent<PowerBar>().Complete()) {
			isActive = false;
			Fire(GetComponent<PowerBar>().GetTargetSectionNumber());
			waitingForAnimation = true;
		} else if(isActive && fightMenu.on == false) {
			isActive = false;
			GetComponent<PowerBar>().Hide();
		}
	}	
}