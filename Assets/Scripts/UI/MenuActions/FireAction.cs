using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireAction : DefaultMenuAction,MenuAction {

	public bool isActive = false;
	public bool waitingForAnimation = false;
	public float power = 0.0f;
	private Section firingSection;
	private List<Section> hitSections;
	public GameObject hitParticle;
	private int hitIndex;
	public Menu fightMenu;
	public List<Menu> toHide;
	
	public Player player;
	public Player target;
	public Section selectedSection;
	
	public override void Action() {
		selectedSection = TowerSelection.GetSelectedSection();
		if (isActive) {
			isActive = false;
			GetComponent<PowerBar>().Hide();
			PlayClickSound();
		}
		else if(TurnOrder.IsBattlePhase()) {
			if(selectedSection == null || !TurnOrder.myPlayer.GetTower(TurnOrder.actionNum).GetSections().Contains(selectedSection)) {
				ValueStore.helpMessage = "You must select your own tower section to fire!";
			}
			else if (selectedSection.attributes.weapon.GetWeaponType() == "Nothing") {
				ValueStore.helpMessage = "You must select a tower section equipped with a weapon before firing!";
			}
			else {
				//need to change it from showing PowerBar to showing arrows (or something equivalent) for who you want to attack
				audio.Play();
			
				GetComponent<PowerBar>().setRange(selectedSection.GetComponent<Section>().attributes.weapon.GetRange());
				GetComponent<PowerBar>().Show();
				isActive = true;
				fightMenu.on = true;
				//foreach(Menu m in toHide) {
				//	m.on = false;
				//}
				ValueStore.helpMessage = "Use Up/Down arrows to select targets above and below your level. Press Space to fire. ";
			}
		}
		else {
			ValueStore.helpMessage = "Ceasefire still in effect.";
		}
	}
	
	/*
	void Fire(int attackSection) {
		firingSection = selectedSection; //What's the reason for this assignment?
		SectionController sc = firingSection.GetComponent<SectionController>();
		int index = sc.GetHeight() - 1;
		int hitCenter = index + attackSection;
		List<GameObject> hitSections = sc.GetSection().GetWeapon().GetEffect().GetDamagedSections(target.GetTower(), hitCenter);//hitIndex 
		GetComponent<WeaponAnimator>().BeginAnimation(firingSection.gameObject, hitSections, hitParticle);
	}
	*/
	
	//FIX ME!!!
	void Update() {
		if(isActive && GetComponent<PowerBar>().Complete()) {
			isActive = false;
			GetComponent<PowerBar>().Reset();
			TurnOrder.SendAction(new Fight(TurnOrder.actionNum, selectedSection.attributes.height, 0, GetComponent<PowerBar>().GetTargetSectionNumber()));
		} else if(isActive && fightMenu.on == false) {
			isActive = false;
			GetComponent<PowerBar>().Hide();
		}
	}	
}