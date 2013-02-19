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
	public TowerBase[] player1Bases;
	public TowerBase[] player2Bases;
	public MenuItem helpText;
	public Color player1Color;
	public Color player2Color;
	public MenuItem playerText;
	public MenuItem resources;
	public GUISkin player1Box;
	public GUISkin player2Box;
	public MenuItem ceasefireIcon;
	private TurnAction[] myActions = new TurnAction[3];
	private TurnAction[] player1Actions = new TurnAction[3];
	private TurnAction[] player2Actions = new TurnAction[3];
	private int player1ActionsReceived = 0;
	private int player2ActionsReceived = 0;	
	private bool player1Confirm = false;
	private bool player2Confirm = false;
	private string networkState = "waitingForActions";
	private bool inputReady = true;
	public static int actionNum = 0;
	private int displayActionNum = 0;
	private bool showDebug = false;
	
	void Start () {
		instance = this;
		player1 = new Player(1, player1Color, GameValues.intValues["baseResources"]);
		player2 = new Player(2, player2Color, GameValues.intValues["baseResources"]);
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
		actionNum = 0;
		ceasefire = 3;
		Faction[] factions = new Faction[3] { new Totem(), new Cowboys(), new Area51() };
		for(int i=0; i<player1Bases.Length; i++) {
			player1Bases[i].SetTowerNumber(i);
			player1Bases[i].SetPlayerNumber(1);
			player1.AddTower(player1Bases[i], factions[i], i);
		}
		for(int i=0; i<player2Bases.Length; i++) {
			player2Bases[i].SetTowerNumber(i);
			player2Bases[i].SetPlayerNumber(2);
			player2.AddTower(player2Bases[i], factions[i], i);
		}
		CombatLog.addLineNoPlayer("Ceasefire ends in " + (ceasefire - turnNum) + " turns.");
		TowerSelection.Deselect();
	}
	
	private Faction MakeFactionForString(string f) {
		if(f == "Area51") {
			return new Area51();
		} else if(f == "Cowboys") {
			return new Cowboys();
		} else if(f == "Totem") {
			return new Totem();
		} else {
			return new Generic();
		}
	}

	void Update () {
		resources.text = ""+myPlayer.GetResources();
		helpText.text = ValueStore.helpMessage;
		if(Network.isServer) {
			if(player1ActionsReceived == 3 && player2ActionsReceived == 3) {
				if(networkState == "waitingForActions") {
					Debug.Log("Actions received, let's get going!!!");
					networkState = "waitingForReady";
					networkView.RPC("CheckReady", RPCMode.All);
				} else if(networkState == "waitingForReady" && player1Confirm && player2Confirm) {
					player1Confirm = false;
					player2Confirm = false;
					networkState = "performingPlayer1Action";
					networkView.RPC("PerformAction", RPCMode.All, player1Actions[displayActionNum].GetActionMessage());
				} else if(networkState == "performingPlayer1Action" && player1Confirm && player2Confirm) {
					player1Confirm = false;
					player2Confirm = false;
					displayActionNum++;
					if(displayActionNum >= 3) {
						displayActionNum = 0;
						networkState = "performingPlayer2Action";
						networkView.RPC("PerformAction", RPCMode.All, player2Actions[displayActionNum].GetActionMessage());
					} else {
						networkView.RPC("PerformAction", RPCMode.All, player1Actions[displayActionNum].GetActionMessage());
					}
				} else if(networkState == "performingPlayer2Action" && player1Confirm && player2Confirm) {
					player1Confirm = false;
					player2Confirm = false;
					displayActionNum++;
					if(displayActionNum >= 3) {
						displayActionNum = 0;
						networkState = "resolveCollapse";
						networkView.RPC("CollapseIfNeeded", RPCMode.All);
					} else {
						networkView.RPC("PerformAction", RPCMode.All, player2Actions[displayActionNum].GetActionMessage());
					}
				} else if(networkState == "resolveCollapse" && player1Confirm && player2Confirm) {
					player1Confirm = false;
					player2Confirm = false;
					player1ActionsReceived = 0;
					player2ActionsReceived = 0;
					networkState = "waitingForActions";
					networkView.RPC("Resume", RPCMode.All);
				}
			}
		}
		if(Input.GetKeyDown(KeyCode.D)) {
			showDebug = !showDebug;
		}
		//GameObject.FindWithTag("MainMenu").GetComponent<Menu>().on = inputReady;
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
		for(int i=0; i<3; i++) {
			CollapseAnimator.Animate(player1.GetTower(i));
			do {
				yield return new WaitForSeconds(0.1f);
			} while(CollapseAnimator.animate);
		}
		for(int i=0; i<3; i++) {
			CollapseAnimator.Animate(player2.GetTower(i));
			do {
				yield return new WaitForSeconds(0.1f);
			} while(CollapseAnimator.animate);
		}
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
	private void RegisterAction(int actionNum, string actionMessage) {
		TurnAction a = TurnAction.GetActionForMessage(actionMessage);
		if(actionMessage[0] == '1') {
			Debug.Log("Server action received");
			player1Actions[actionNum] = a;
			player1ActionsReceived++;
		} else {
			Debug.Log("Client action received");
			player2Actions[actionNum] = a;
			player2ActionsReceived++;
		}
	}
	
	private void RegisterAction(TurnAction action) {
		ResetMenus();
		myActions[actionNum] = action;
		if(actionNum >= 2) {
			for(int i=0; i<3; i++) {
				if(Network.isClient) {
					networkView.RPC("RegisterAction", RPCMode.Server, i, myActions[i].GetActionMessage());
				} else if(Network.isServer) {
					RegisterAction(i, myActions[i].GetActionMessage());
				}
			}
			foreach (Menu c in GameObject.Find("MainMenu").GetComponentsInChildren<Menu>()) {
				c.on = false;
			}
		} else {
			actionNum++;
		}
		TowerSelection.LocalSelectSection(myPlayer.GetTower(actionNum), -1);
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
		player1.AccrueResources();
		player2.AccrueResources();
		actionNum = 0;
		TowerSelection.Deselect();
	}
	
	public static bool IsBattlePhase() {
		return turnNum >= ceasefire;
	}
	
	public static void CheckVictory() { //FIX ME
		if(IsBattlePhase()) {
			if(player1.Loses()) {
				Debug.Log("--------------PLAYER2 WINS----------------");
			} else if(player2.Loses()) {
				Debug.Log("--------------PLAYER1 WINS----------------");
			}
		}
	}
	
	public static void ResetMenus() {
		ValueStore.selectedWeapon = null;
		ValueStore.selectedMaterial = null;
		ValueStore.helpMessage = "";
		
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
	
	void OnGUI() {
		if(showDebug) {
			Rect r = new Rect(Screen.width/2 - 150, Screen.height/2 - 150, 250, 150);
			string debugText = "";
			debugText += "Turn number: " + turnNum + "\n";
			debugText += "---Server text below---\n";
			debugText += "Player 1 actions receied" + player1ActionsReceived + "\n";
			debugText += "Player 2 actions receied" + player2ActionsReceived + "\n";
			debugText += "Network State: " + networkState + "\n";
			debugText += "Displaying action: " + displayActionNum + "\n";
			debugText += "Player 1 confirmed: " + player1Confirm + "\n";
			debugText += "Player 2 confirmed: " + player2Confirm + "\n";
			GUI.Box(r, debugText);
		}
	}
}