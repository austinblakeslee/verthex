using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour {

	private Rect p1T1Rect;
	private Rect p1T2Rect;
	private Rect p1T3Rect;
	private Rect p2T1Rect;
	private Rect p2T2Rect;
	private Rect p2T3Rect;
	
	void Start() {
		p1T1Rect = new Rect(Screen.width/2,Screen.height-100,80,20);
		p1T2Rect = new Rect(Screen.width/2,Screen.height-66,80,20);
		p1T3Rect = new Rect(Screen.width/2,Screen.height-33,80,20);
		p2T1Rect = new Rect(Screen.width/2+100,Screen.height-100,80,20);
		p2T2Rect = new Rect(Screen.width/2+100,Screen.height-66,80,20);
		p2T3Rect = new Rect(Screen.width/2+100,Screen.height-33,80,20);
		MenuItemManager.RegisterRect(p1T1Rect);
		MenuItemManager.RegisterRect(p1T2Rect);
		MenuItemManager.RegisterRect(p1T3Rect);
		MenuItemManager.RegisterRect(p2T1Rect);
		MenuItemManager.RegisterRect(p2T2Rect);
		MenuItemManager.RegisterRect(p2T3Rect);
	}
	
	void OnGUI () {
		if(GUI.Button(p1T1Rect, "P1:T1"))
		{
			Tower baseTow = TurnOrder.GetPlayerByNumber(1).GetTower(0);
			TowerSelection.LocalSelectSection(baseTow, -1);
		}

		if(GUI.Button(p1T2Rect, "P1:T2"))
		{
			Tower baseTow = TurnOrder.GetPlayerByNumber(1).GetTower(1);
			TowerSelection.LocalSelectSection(baseTow, -1);
		}
		
		if(GUI.Button(p1T3Rect, "P1:T3"))
		{
			Tower baseTow = TurnOrder.GetPlayerByNumber(1).GetTower(2);
			TowerSelection.LocalSelectSection(baseTow, -1);
		}
		
		if(GUI.Button(p2T1Rect, "P2:T1"))
		{
			Tower baseTow = TurnOrder.GetPlayerByNumber(2).GetTower(0);
			TowerSelection.LocalSelectSection(baseTow, -1);
		}
		
		if(GUI.Button(p2T2Rect, "P2:T2"))
		{
			Tower baseTow = TurnOrder.GetPlayerByNumber(2).GetTower(1);
			TowerSelection.LocalSelectSection(baseTow, -1);
		}
		
		if(GUI.Button(p2T3Rect, "P3:T3"))
		{
			Tower baseTow = TurnOrder.GetPlayerByNumber(2).GetTower(2);
			TowerSelection.LocalSelectSection(baseTow, -1);
		}
	}
}
