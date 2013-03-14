// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class GameModeMenu : MonoBehaviour {

	GameObject playerPrefab;
	Transform spawnObject;

	private bool refreshing;
	private HostData[] hostData;
	public bool visible;
	public bool waiting;
	private float buttonX;
	private float buttonY;
	private float buttonW;
	private float buttonH;
	private bool deathmatch = true;
	private bool survival = false;
		
	private string gameType = "Online";
	public string serverName = "Enter Server Name Here!";
	string gameName = "VertHex_Network_Test";


	void  Start (){
		buttonX = Screen.width * 0.35f;
		buttonY = Screen.width * 0.1f;
		buttonW = Screen.width * 0.1f;
		buttonH = Screen.width * 0.1f;
	}

	//GUI
	void  OnGUI (){
		if(visible && waiting == false) {
		

			
			if(GUI.Toggle(new Rect(buttonX,buttonY,90,30),deathmatch,"Deathmatch")) {
   				deathmatch = true;
   				survival = false;
				GameType.setGameType("Deathmatch");
}
 
			if(GUI.Toggle(new Rect(buttonX,buttonY*1.5f,75,30),survival,"Survival")) {
   				deathmatch = false;
   				survival = true;
				GameType.setGameType("Survival");
}
			
		}
		else if(waiting && visible == true) {
			GUI.Box(new Rect(Screen.width/2-150, Screen.height/2-75,300,50),"Waiting for Player to Join Server:\n" + serverName + "...");
			if(GUI.Button(new Rect(Screen.width/2-75, Screen.height/2+75,150,50),"Disconnect Server")) {
				Network.Disconnect();
				MasterServer.UnregisterHost();
				waiting = false;
			}
		}
	}

}