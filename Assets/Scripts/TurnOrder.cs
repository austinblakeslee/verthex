using UnityEngine;
using System.Collections;

public class TurnOrder : MonoBehaviour {
	public GameObject player1Base;
	public GameObject player2Base;
	public static Player player1;
	public static Player player2;
	public static Player currentPlayer;
	public static Player otherPlayer;
	//public static GameObject currentBuildMenu = GameObject.Find("Build Aliens");
	//public static GameObject otherBuildMenu = GameObject.Find("Build Cowboy");
	private static int numActionsTaken;
	private static int turnNum;
	public static int ceasefire;
	public MenuItem helpText;
	public Color player1Color;
	public Color player2Color;
	public MenuItem playerText;
	public MenuItem resources;
	public MenuItem actionsLeft;
	public MenuItem ceasefireIcon;
	public GUISkin player1Box;
	public GUISkin player2Box;
	public static Player myPlayer;
	
	public void Start () {
		player1 = new Player(1, player1Color, new Tower(), GameValues.intValues["baseResources"]);
		player2 = new Player(2, player2Color, new Tower(), GameValues.intValues["baseResources"]);
		if(Network.isServer) {
			myPlayer = player1;
		} else {
			myPlayer = player2;
		}
		currentPlayer = player1;
		otherPlayer = player2;
		//turnNum is incremented everytime control switches.
		turnNum = 0;
		//if ceasefire is less than turn number then the player can fire
		ceasefire = 3;
		player1.SetTowerLocation(player1Base, player2Base);
		player2.SetTowerLocation(player2Base, player1Base);
		ValueStore.helpMessage = "Click a section to select it.";
		CombatLog.addLineNoPlayer("Ceasefire ends in " + (ceasefire - turnNum) + " turns.");
	}
	
	public static bool MyTurn() {
		return myPlayer == currentPlayer;
	}

	public void Update () {
		if(currentPlayer.playerNumber == 1) {
			playerText.guiSkin = player1Box;
			playerText.text = "Player 1";
		} else {
			playerText.guiSkin = player2Box;
			playerText.text = "Player 2";
		}
		int a = GameValues.intValues["actionsPerTurn"] - numActionsTaken;
		actionsLeft.text = a + " left";
		resources.text = ""+currentPlayer.GetResources();
		if(turnNum == ceasefire) {
			ceasefireIcon.visible = false;
		}
		helpText.text = ValueStore.helpMessage;
	}	
	
	public static void ActionTaken() {
        numActionsTaken++;
		ValueStore.helpMessage = "Click a section to select it.";
        if(numActionsTaken >= GameValues.intValues["actionsPerTurn"]) {
			SwitchPlayer();
        }
		if(IsBattlePhase()) {
			CheckVictory();
		}
        ResetMenus();
	}
	
	public static bool IsBattlePhase() {
		return turnNum >= ceasefire;
	}
	
	private static void SwitchPlayer() {
		turnNum++;
		if(!IsBattlePhase() && turnNum != 0) {
			CombatLog.addLineNoPlayer("Ceasefire ends in " + (ceasefire - turnNum) + " turns.");
		}
		if(turnNum == ceasefire) {
			CombatLog.addLineNoPlayer("!!! CEASEFIRE HAS ENDED !!!");
		}
		numActionsTaken = 0;
		TowerSelection.LocalSelectSection(-1, -1);
		SwapPlayers(); //currentPlayer is now otherPlayer
		if(turnNum > 1) {
			currentPlayer.AccrueResources();
		}
	}
	
	private static void SwapPlayers() {
		Player temp = currentPlayer;
		currentPlayer = otherPlayer;
		otherPlayer = temp;
		MainCamera mc = GameObject.FindWithTag("MainCamera").GetComponent<MainCamera>();
		mc.ChangeTarget(currentPlayer.towerBase.transform);
		currentPlayer.NextTurn();
		Menu m = GameObject.Find ("Main").GetComponent ("Menu") as Menu;
		m.swapBuildMenus();
		
	/*	GameObject temp2 = currentBuildMenu;
		currentBuildMenu = otherBuildMenu;
		otherBuildMenu = temp2;
		
		Menu menu = GameObject.Find ("Main").GetComponent("Menu") as Menu;
		menu.menuItems[0] = currentBuildMenu.GetComponent ("MenuItem") as MenuItem;*/
	}
	
	public static void CheckVictory() {
		if(IsBattlePhase()) {
			if(player1.GetTower().GetHeight() == 0) {
				Application.LoadLevel("Player2Wins");
			} else if(player2.GetTower().GetHeight() == 0) {
				Application.LoadLevel("Player1Wins");
			}
		}
	}
	
	public static void ResetMenus() {
		ToggleMenuForTag("BuildMenu");
		ToggleMenuForTag("FortifyMenu");
		ToggleMenuForTag("UpgradeMenu");
		ValueStore.selectedWeapon = null;
		ValueStore.selectedMaterial = null;
		
		SetTextForMenuItemWithTag("BuildWeaponCost", "0");
		SetTextForMenuItemWithTag("BuildMaterialCost", "0");
		SetTextForMenuItemWithTag("BuildSumCost", "0");
		
	}
	
	private static void ToggleMenuForTag(string tag) {
		GameObject g = GameObject.FindWithTag(tag);
		if(g != null) {
			g.GetComponent<Menu>().on = false;
		}
	}
	
	private static void SetTextForMenuItemWithTag(string tag, string text) {
		GameObject g = GameObject.FindWithTag(tag);
		if(g != null) {
			g.GetComponent<MenuItem>().text = text;
		}
	}
	
	public static Player GetPlayerByNumber(int i) {
		if(i == 1) {
			return player1;
		} else if(i == 2) {
			return player2;
		} else {
			return null;
		}
	}
}
