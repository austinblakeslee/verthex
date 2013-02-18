using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour {
	void OnGUI () {
		if(GUI.Button(new Rect(Screen.width/2,Screen.height-100,80,20), "P1:T1"))
		{
			Tower baseTow = TurnOrder.GetPlayerByNumber(1).GetTower(0);
			TowerSelection.LocalSelectSection(baseTow, -1);
			audio.Play();
		}

		if(GUI.Button(new Rect(Screen.width/2,Screen.height-66,80,20), "P1:T2"))
		{
			Tower baseTow = TurnOrder.GetPlayerByNumber(1).GetTower(1);
			TowerSelection.LocalSelectSection(baseTow, -1);
			audio.Play();
		}
		
		if(GUI.Button(new Rect(Screen.width/2,Screen.height-33,80,20), "P1:T3"))
		{
			Tower baseTow = TurnOrder.GetPlayerByNumber(1).GetTower(2);
			TowerSelection.LocalSelectSection(baseTow, -1);
			audio.Play();
		}
		
		if(GUI.Button(new Rect(Screen.width/2+100,Screen.height-100,80,20), "P2:T1"))
		{
			Tower baseTow = TurnOrder.GetPlayerByNumber(2).GetTower(0);
			TowerSelection.LocalSelectSection(baseTow, -1);
			audio.Play();
		}
		
		if(GUI.Button(new Rect(Screen.width/2+100,Screen.height-66,80,20), "P2:T2"))
		{
			Tower baseTow = TurnOrder.GetPlayerByNumber(2).GetTower(1);
			TowerSelection.LocalSelectSection(baseTow, -1);
			audio.Play();
		}
		
		if(GUI.Button(new Rect(Screen.width/2+100,Screen.height-33,80,20), "P3:T3"))
		{
			Tower baseTow = TurnOrder.GetPlayerByNumber(2).GetTower(2);
			TowerSelection.LocalSelectSection(baseTow, -1);
			audio.Play();
		}
	}
}
