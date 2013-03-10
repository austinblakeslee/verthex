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
				Debug.Log (firingSection);
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
				Tower targetTower = TowerSelection.GetSelectedTower();
				int targetSectionNum = TowerSelection.GetSelectedSection().attributes.height;
				Debug.Log ("Target Player = " + targetTower.GetPlayerNum() + ". Target tNum = " + targetTower.towerNum + ". targetSection = " + targetSectionNum);
				TurnOrder.SendAction(new Fight(firingTowerNum, firingSectionNum, targetTower, targetSectionNum));
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
		bool basicCheck = selectedSection != null && selectedSection.attributes.height >= lowRange &&
			selectedSection.attributes.height <= highRange;
		if (!firingSection.attributes.weapon.GetEffect().CanAttackSelf()){
			return selectedTower.towerBase.playerNumber != TurnOrder.myPlayer.playerNumber && basicCheck;
		}
		return basicCheck;
	}
	
	private void hide() {
		//GameObject.FindWithTag("MainCamera").camera.enabled = false;
		GameObject.Find("MainMenu").GetComponent<Menu>().on = false;
		//GameObject.Find ("TopMenu").GetComponent<Menu>().on = false;
		GameObject.FindGameObjectWithTag("Player").GetComponent<TowerInspector2>().show = false;
		GameObject cam = GameObject.FindWithTag("MainCamera");
		string firingCam = "";
	//	if (firingSection.attributes.weapon.GetEffect().CanAttackOpponent() && firingSection.attributes.weapon.GetEffect().CanAttackSelf()) //if can attack either opp or self,
	//	{
			//TODO: Add option to choose who to attack / enabling a "Switch Camera" button from p1FireCamera to p2FireCamera would work
			
	//	}
	//	else
			if(firingSection.attributes.weapon.GetEffect().CanAttackSelf()) //if can only attack self
		{
			firingCam = TurnOrder.myPlayer == TurnOrder.player1 ? "p2FireCamera" : "p1FireCamera";
		}
		else if(firingSection.attributes.weapon.GetEffect().CanAttackOpponent()) //if can only attack opp
		{
			firingCam = TurnOrder.myPlayer == TurnOrder.player1 ? "p1FireCamera" : "p2FireCamera";
		}
		else
		{
			Debug.Log ("Error choosing which Firing Camera to use");
		}
		//cam.GetComponent<MainCamera>().enabled = false;
		cam.transform.position = GameObject.FindWithTag(firingCam).transform.position; 
		cam.transform.rotation = GameObject.FindWithTag(firingCam).transform.rotation;
		cam.transform.camera.fieldOfView = 60;

	}
	
	private void unhide() {
		//if(TurnOrder.myPlayer == TurnOrder.player1) {
			//GameObject.FindWithTag("p1FireCamera").camera.enabled = false;
	//	}
//		if(TurnOrder.myPlayer == TurnOrder.player2) {
			//GameObject.FindWithTag("p2FireCamera").camera.enabled = false;
//		}
		//GameObject.FindWithTag("MainCamera").camera.enabled = true;
		GameObject cam = GameObject.FindWithTag("MainCamera");
		//cam.GetComponent<MainCamera>().enabled = true;
		cam.transform.camera.fieldOfView = 20;
		cam.GetComponent<InGameCamera>().returnPostion();
		GameObject.Find("MainMenu").GetComponent<Menu>().on = true;
		//GameObject.Find ("TopMenu").GetComponent<Menu>().on = true;
		GameObject.FindGameObjectWithTag("Player").GetComponent<TowerInspector2>().show = true;
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