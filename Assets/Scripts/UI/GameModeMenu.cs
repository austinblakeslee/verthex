using UnityEngine;
using System.Collections;

public class GameModeMenu : MonoBehaviour {

	public bool visible = true;
	private float buttonX;
	private float buttonY;
	private float buttonW;
	private float buttonH;
	private bool deathmatch = true;
	private bool survival = false;



	void  Start (){
		buttonX = Screen.width * 0.16f;
		buttonY = Screen.width * 0.08f;
		buttonW = Screen.width * 0.1f;
		buttonH = Screen.width * 0.1f;
	}

	//GUI
	void  OnGUI (){
		if(GameObject.Find("Online Menu").GetComponent<NetworkAction>().visible) {

			if(GUI.Toggle(new Rect(buttonX,buttonY,90,30),deathmatch,"Deathmatch")) {
   				deathmatch = true;
   				survival = false;
				GameType.setGameType("Deathmatch");
			}
 
			if(GUI.Toggle(new Rect(buttonX,buttonY*1.25f,75,30),survival,"Survival")) {
   				deathmatch = false;
   				survival = true;
				GameType.setGameType("Survival");
			}	
		}

	}

}