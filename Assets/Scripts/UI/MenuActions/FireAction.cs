using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireAction : DefaultMenuAction,MenuAction {

	public bool isActive = false;
	public Menu fightMenu;
	public List<Menu> toHide;
	
	private int lowRange;
	private int highRange;
	private Section firingSection;
	
	public override void Action() {
		firingSection = TowerSelection.GetSelectedSection();
		if (isActive) {
			isActive = false;
			PlayClickSound();
		}
		else if(TurnOrder.IsBattlePhase()) {
			if(firingSection == null || !TurnOrder.myPlayer.GetTower(TurnOrder.actionNum).GetSections().Contains(firingSection)) {
				ValueStore.helpMessage = "You must select your own tower section to fire!";
			}
			else if (firingSection.attributes.weapon.GetWeaponType() == "Nothing") {
				ValueStore.helpMessage = "You must select a tower section equipped with a weapon before firing!";
			}
			else {
				//need to change it from showing PowerBar to showing arrows (or something equivalent) for who you want to attack
				audio.Play();
				isActive = true;
				int range = firingSection.attributes.weapon.GetRange();
				lowRange = Mathf.Max(firingSection.attributes.height - range, 0);
				highRange = firingSection.attributes.height + range;
				fightMenu.on = true;
				//foreach(Menu m in toHide) {
				//	m.on = false;
				//}
				ValueStore.helpMessage = "Choose an enemy section to fire at,\nthen press the Space bar.";
			}
		}
		else {
			ValueStore.helpMessage = "Ceasefire still in effect.";
		}
	}
	
	//FIX ME!!!
	void Update() {
		if(isActive && Input.GetKeyDown(KeyCode.Space)) {
			if(CheckTarget()) {
				isActive = false;
				int firingTowerNum = TurnOrder.actionNum;
				int firingSectionNum = firingSection.attributes.height;
				int targetTowerNum = TowerSelection.GetSelectedTower().towerBase.towerNumber;
				int targetSectionNum = TowerSelection.GetSelectedSection().attributes.height;
				TurnOrder.SendAction(new Fight(firingTowerNum, firingSectionNum, targetTowerNum, targetSectionNum));
			} else {
				ValueStore.helpMessage = "You cannot fire at that section.";
			}
		} else if(isActive && fightMenu.on == false) {
			isActive = false;
		}
	}
	
	private bool CheckTarget() {
		Tower selectedTower = TowerSelection.GetSelectedTower();
		Section selectedSection = TowerSelection.GetSelectedSection();
		return selectedTower.towerBase.playerNumber != TurnOrder.myPlayer.playerNumber &&
			selectedSection != null && selectedSection.attributes.height >= lowRange &&
			selectedSection.attributes.height <= highRange;
	}
}