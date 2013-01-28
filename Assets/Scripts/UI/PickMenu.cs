using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickMenu : MonoBehaviour {
	public static bool showMenu;
	public bool pause;
	public GUIStyle pm;
	
	void Start () {
		showMenu = false;
		pause = false;
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) {
			if(pause == false) {
				pause = true;
				Time.timeScale = 0.0f;
			}
			else {
				pause = false;
				Time.timeScale = 1.0f;
			}
		}
	}
	
	void OnGUI () {
		if(pause) {
			if (GUI.Button(new Rect(Screen.width/2-Screen.width/10,Screen.height/2 - Screen.height/8,Screen.width/5,Screen.height/10),"Resume",pm)) {
				pause = false;
				Time.timeScale = 1.0f;
			}
			else if (GUI.Button(new Rect(Screen.width/2-Screen.width/10,Screen.height/2,Screen.width/5,Screen.height/10),"Restart",pm)) {
				pause = false;
				Time.timeScale = 1.0f;
				Application.LoadLevel(1);
			}
			else if (GUI.Button(new Rect(Screen.width/2-Screen.width/10,Screen.height/2+Screen.height/8,Screen.width/5,Screen.height/10),"Quit",pm)) {
				pause = false;
				Time.timeScale = 1.0f;
				Application.LoadLevel(0);
			}
    	}
	}
}	
	
