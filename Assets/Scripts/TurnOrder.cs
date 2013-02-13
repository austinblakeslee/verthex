using UnityEngine;
using System.Collections;

public class TurnOrder : MonoBehaviour {
	public static Player player1;
	public static Player player2;
	public static Player myPlayer;
	public static Player otherPlayer;
	private static int turnNum;
	public static int ceasefire;
	private static TurnOrder instance;
	public GameObject player1Base;
	public GameObject player2Base;
	public MenuItem helpText;
	public Color player1Color;
	public Color player2Color;
	public MenuItem playerText;
	public MenuItem resources;
	public GUISkin player1Box;
	public GUISkin player2Box;
	public MenuItem ceasefireIcon;
	private TurnAction player1Action = null;
	private TurnAction player2Action = null;
	private bool player1Confirm = false;
	private bool player2Confirm = false;
	private string networkState = "waitingForActions";
	private bool inputReady = true;
	
	void Start () {
		instance = this;
		player1 = new Player(1, player1Color, new Tower(), GameValues.intValues["baseResources"], new Generic());
		player2 = new Player(2, player2Color, new Tower(), GameValues.intValues["baseResources"], new MoreGeneric());
		if(Network.isServer || GameType.getGameType() == "Local") {
			myPlayer = player1;
			otherPlayer = player2;
			playerText.guiSkin = player1Box;
			playerText.text = "Player 1";
		} else {
			myPlayer = player2;
			otherPlayer = player1;
			playerText.guiSkin = player2Box;
			playerText.text = "Player 2";
		}
		turnNum = 0;
		ceasefire = 3;
		player1.SetTowerLocation(player1Base, player2Base);
		player2.SetTowerLocation(player2Base, player1Base);
		ValueStore.helpMessage = "Click a section to select it.";
		CombatLog.addLineNoPlayer("Ceasefire ends in " + (ceasefire - turnNum) + " turns.");
	}

	void Update () {
		resources.text = ""+myPlayer.GetResources();
		helpText.text = ValueStore.helpMessage;
		if(Network.isServer) {
			if(player1Action != null && player2Action != null) {
				if(networkState == "waitingForActions") {
					Debug.Log("Actions received, let's get going!!!");
					networkState = "waitingForReady";
					networkView.RPC("CheckReady", RPCMode.All);
				} else if(networkState == "waitingForReady" && player1Confirm && player2Confirm) {
					player1Confirm = false;
					player2Confirm = false;
					networkState = "performingPlayer1Action";
					networkView.RPC("PerformAction", RPCMode.All, player1Action.GetActionMessage());
				} else if(networkState == "performingPlayer1Action" && player1Confirm && player2Confirm) {
					player1Confirm = false;
					player2Confirm = false;
					networkState = "performingPlayer2Action";
					networkView.RPC("PerformAction", RPCMode.All, player2Action.GetActionMessage());
				} else if(networkState == "performingPlayer2Action" && player1Confirm && player2Confirm) {
					player1Confirm = false;
					player2Confirm = false;
					networkState = "resolveCollapse";
					networkView.RPC("CollapseIfNeeded", RPCMode.All);
				} else if(networkState == "resolveCollapse" && player1Confirm && player2Confirm) {
					player1Confirm = false;
					player2Confirm = false;
					player1Action = null;
					player2Action = null;
					networkState = "waitingForActions";
					networkView.RPC("Resume", RPCMode.All);
				}
			}
		}
		GameObject.FindWithTag("MainMenu").GetComponent<Menu>().on = inputReady;
	}
	
	[RPC]
	private void CheckReady() {
		if(Network.isClient) {
			networkView.RPC("PlayerReady", RPCMode.Server, myPlayer.playerNumber);
		} else {
			PlayerReady(myPlayer.playerNumber);
		}
	}
	
	[RPC]
	private void PlayerReady(int playerNum) {
		if(playerNum == 1) {
			player1Confirm = true;
		} else {
			player2Confirm = true;
		}
	}
	
	[RPC]
	private IEnumerator PerformAction(string actionMessage) {
		TurnAction action = TurnAction.GetActionForMessage(actionMessage);
		action.Perform();
		do {
			yield return new WaitForSeconds(0.5f);
		} while(CollapseAnimator.animate || WeaponAnimator.animate);
		yield return new WaitForSeconds(2.0f);
		if(Network.isClient) {
			networkView.RPC("PlayerReady", RPCMode.Server, myPlayer.playerNumber);
		} else {
			PlayerReady(myPlayer.playerNumber);
		}
	}
	
	[RPC]
	private IEnumerator CollapseIfNeeded() {
		CollapseAnimator.Animate(player1.GetTower());
		do {
			yield return new WaitForSeconds(0.5f);
		} while(CollapseAnimator.animate);
		CollapseAnimator.Animate(player2.GetTower());
		do {
			yield return new WaitForSeconds(0.5f);
		} while(CollapseAnimator.animate);
		if(Network.isClient) {
			networkView.RPC("PlayerReady", RPCMode.Server, myPlayer.playerNumber);
		} else {
			PlayerReady(myPlayer.playerNumber);
		}
	}
	
	[RPC]
	private void Resume() {
		this.inputReady = true;
		GameObject.Find("MainMenu").GetComponent<Menu>().on = true;
		EndTurn();
	}
	
	[RPC]
	private void RegisterAction(string actionMessage) {
		TurnAction a = TurnAction.GetActionForMessage(actionMessage);
		if(actionMessage[0] == '1') {
			Debug.Log("Server action received");
			player1Action = a;
		} else {
			Debug.Log("Client action received");
			player2Action = a;
		}
	}
	
	private void RegisterAction(TurnAction action) {
		ResetMenus();
		this.inputReady = false;
		foreach (Menu c in GameObject.Find("MainMenu").GetComponentsInChildren<Menu>()) {
			c.on = false;
		}
		if(Network.isClient) {
			networkView.RPC("RegisterAction", RPCMode.Server, action.GetActionMessage());
		} else if(Network.isServer) {
			RegisterAction(action.GetActionMessage());
		}
	}
	
	public static void SendAction(TurnAction action) {
		instance.RegisterAction(action);
	}
	
	private void EndTurn() {
		if(IsBattlePhase()) {
			CheckVictory();
		}
		ValueStore.helpMessage = "Click a section to select it.";
		turnNum++;
		if(!IsBattlePhase() && turnNum != 0) {
			CombatLog.addLineNoPlayer("Ceasefire ends in " + (ceasefire - turnNum) + " turns.");
		}
		if(turnNum == ceasefire) {
			ceasefireIcon.visible = false;
			CombatLog.addLineNoPlayer("!!! CEASEFIRE HAS ENDED !!!");
		}
		TowerSelection.LocalSelectSection(-1, -1);
		player1.AccrueResources();
		player2.AccrueResources();
		
	}
	
	public static bool IsBattlePhase() {
		return turnNum >= ceasefire;
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
