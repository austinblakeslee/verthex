using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour {
	public static bool showMenu;
	public bool pause;
	public GUIStyle pm;
	public string stringCL;
	
	void Start () {
		showMenu = false;
		pause = false;
		stringCL = "Show Combat Log";
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) {
			if(pause == false) {
				pause = true;
				//Time.timeScale = 0.0f;
			}
			else {
				pause = false;
				//Time.timeScale = 1.0f;
			}
		}
		if(Network.connections.Length == 0) {
			GameValues.player1Faction = "EMPTY";
			GameValues.player2Faction = "EMPTY";
			Network.Disconnect(100);
			MasterServer.UnregisterHost();
			Application.LoadLevel(0);
		}
	}
	
	[RPC]
	private void Disconnect() {
		Debug.Log("Disconnecting: "+ Network.connections[0].ipAddress+":"+Network.connections[0].port);
		GameValues.player1Faction = "EMPTY";
		GameValues.player2Faction = "EMPTY";
		Network.CloseConnection(Network.connections[0], true);
		Network.Disconnect(100);
		MasterServer.UnregisterHost();
		//N
		//pause = false;
		//Time.timeScale = 1.0f;	
	}
	
	void OnDisconnectedFromServer (NetworkDisconnection info)
	{
		Application.LoadLevel(0);
	}
	
	void OnGUI () {
		if(pause) {
			if (GUI.Button(new Rect(Screen.width/2-Screen.width/10,Screen.height/2 - Screen.height/8,Screen.width/5,Screen.height/10),"Resume",pm)) {
				pause = false;
				//Time.timeScale = 1.0f;
			}
			else if (GUI.Button(new Rect(Screen.width/2-Screen.width/10,Screen.height/2,Screen.width/5,Screen.height/10),stringCL,pm)) {
				HideCL.setCombatLog();
				if(stringCL == "Hide Combat Log") {
					stringCL = "Show Combat Log";
				}
				else if (stringCL == "Show Combat Log") {
					stringCL = "Hide Combat Log";
				}
			}
			//else if (GUI.Button(new Rect(Screen.width/2-Screen.width/10,Screen.height/2,Screen.width/5,Screen.height/10),"Restart",pm)) {
			//	pause = false;
			//	Time.timeScale = 1.0f;
			//	Application.LoadLevel(1);
			//}
			else if (GUI.Button(new Rect(Screen.width/2-Screen.width/10,Screen.height/2+Screen.height/8,Screen.width/5,Screen.height/10),"Disconnect",pm)) {
				networkView.RPC ("Disconnect",RPCMode.All);
				//Application.LoadLevel(0);
			}
    	}
	}
}	
	
