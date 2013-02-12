// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class NetworkAction : MonoBehaviour {

GameObject playerPrefab;
Transform spawnObject;

private bool refreshing;
private HostData[] hostData;
public bool visible;

private float buttonX;
private float buttonY;
private float buttonW;
private float buttonH;
	
private string gameType = "Online";
public string serverName = "Enter Server Name Here!";
string gameName = "VertHex_Network_Test";


void  Start (){
	buttonX = Screen.width * 0.05f;
	buttonY = Screen.width * 0.05f;
	buttonW = Screen.width * 0.1f;
	buttonH = Screen.width * 0.1f;
}

void  startServer (string serverName){
	Network.InitializeServer(8, 25000, !Network.HavePublicAddress());
	MasterServer.RegisterHost(gameName, serverName, "This is a VertHex multiplayer test");
}

void  refreshHostList (){
	MasterServer.RequestHostList(gameName);
	refreshing = true;
}	

void  Update (){
	if(refreshing){
		if(MasterServer.PollHostList().Length > 0){
			refreshing = false;
			Debug.Log(MasterServer.PollHostList().Length);
			hostData = MasterServer.PollHostList();
		}
	}
}

//Messages
void  OnServerInitialized (){
	Debug.Log("Server initialized!");
	//spawnPlayer();
}

void OnPlayerConnected() {
	Application.LoadLevel("Test");
}

void  OnMasterServerEvent ( MasterServerEvent mse  ){
	if(mse == MasterServerEvent.RegistrationSucceeded){
		Debug.Log("Registered Server!");
	}
}

//GUI
void  OnGUI (){
	if(visible) {
	
		serverName = GUI.TextField(new Rect(buttonX, buttonY - 10, 200, 20), serverName, 25);
		
		if(GUI.Button( new Rect(buttonX, buttonY + 30, buttonW, 40 ), "Start Server")){
			Debug.Log("Starting Server");
			startServer(serverName);
		}
	
		if(GUI.Button( new Rect(buttonX, buttonY * 1.2f + 60, buttonW, 40 ), "Refresh Hosts")){
			Debug.Log("Refreshing");
			refreshHostList();
		}
		if(hostData != null && hostData.Length > 0){
			for(int i = 0; i<hostData.Length; i++){
				if(GUI.Button( new Rect(buttonX * 1.5f + buttonW, buttonY*1.2f + (buttonH * i), buttonW*3, buttonH*0.5f), hostData[i].gameName + " - " + "Players: " + hostData[i].connectedPlayers)){
					Network.Connect(hostData[i]);
					//Set gameType to Online to determine player turns/control
					GameType.setGameType(gameType);
					Application.LoadLevel("Test");
				}
			}
		}
	}
}
}