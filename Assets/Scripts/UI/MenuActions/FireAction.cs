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
				hide ();
				TowerSelection.Deselect();
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
				unhide ();
				int firingTowerNum = TurnOrder.actionNum;
				int firingSectionNum = firingSection.attributes.height;
				int targetTowerNum = TowerSelection.GetSelectedTower().towerBase.towerNumber;
				int targetSectionNum = TowerSelection.GetSelectedSection().attributes.height;
				Debug.Log ("Target tNum = " + targetTowerNum + ". targetSection = " + targetSectionNum);
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
	
	private void hide() {
		GameObject.FindWithTag("MainCamera").camera.enabled = false;
		//GameObject.FindWithTag("MiniMap").camera.enabled = false;
		//GameObject.FindWithTag("MiniMap").GetComponent<MiniMap>().hidden = true;
		GameObject.Find("MainMenu").GetComponent<Menu>().on = false;
		if(TurnOrder.myPlayer == TurnOrder.player1) {
			GameObject.FindWithTag("p1FireCamera").camera.enabled = true;	
		}
		else if(TurnOrder.myPlayer == TurnOrder.player2) {
			GameObject.FindWithTag("p2FireCamera").camera.enabled = true;
		}
	}
	
	private void unhide() {
		if(TurnOrder.myPlayer == TurnOrder.player1) {
			GameObject.FindWithTag("p1FireCamera").camera.enabled = false;
		}
		if(TurnOrder.myPlayer == TurnOrder.player2) {
			GameObject.FindWithTag("p2FireCamera").camera.enabled = false;
		}
		//GameObject.FindWithTag("MiniMap").GetComponent<MiniMap>().hidden = false;
		GameObject.FindWithTag("MainCamera").camera.enabled = true;
		//GameObject.FindWithTag("MiniMap").camera.enabled = true;
		GameObject.Find("MainMenu").GetComponent<Menu>().on = true;
	}
	
	void OnGUI() {
		if(isActive) {
		if(GUI.Button (new Rect(Screen.width - 105, Screen.height - 55,100,50), "Cancel") ){
			isActive = false;
			unhide ();
		}
		/*if(GUI.Button (new Rect(Screen.width - 105, Screen.height - 110,100,50), "Confirm") ){
			if(CheckTarget()) {
				isActive = false;
				unhide ();
				int firingTowerNum = TurnOrder.actionNum;
				int firingSectionNum = firingSection.attributes.height;
				int targetTowerNum = TowerSelection.GetSelectedTower().towerBase.towerNumber;
				int targetSectionNum = TowerSelection.GetSelectedSection().attributes.height;
				TurnOrder.SendAction(new Fight(firingTowerNum, firingSectionNum, targetTowerNum, targetSectionNum));
			} else {
				ValueStore.helpMessage = "You cannot fire at that section.";
			}
		}*/
		}
	}
}