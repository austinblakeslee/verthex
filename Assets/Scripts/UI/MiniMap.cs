using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour {

	private Rect p1T1Rect;
	private Rect p1T2Rect;
	private Rect p1T3Rect;
	private Rect p2T1Rect;
	private Rect p2T2Rect;
	private Rect p2T3Rect;
	private Vector3 scale;
	private float ow;
	private float oh;
	
	void Start() {
		ow = 960;
		oh = 600;
		p1T1Rect = new Rect(147,430,30,30);//new Rect(80,435,20,20);
		p1T2Rect = new Rect(75,430,30,30);//new Rect(20,480,20,20);
		p1T3Rect = new Rect(15,475,30,30);//new Rect(50,565,20,20);
		p2T1Rect = new Rect(45,560,30,30);
		p2T2Rect = new Rect(117,560,30,30);//new Rect(183,520,20,20);
		p2T3Rect = new Rect(178,515,30,30);
		MenuItemManager.RegisterRect(p1T1Rect);
		MenuItemManager.RegisterRect(p1T2Rect);
		MenuItemManager.RegisterRect(p1T3Rect);
		MenuItemManager.RegisterRect(p2T1Rect);
		MenuItemManager.RegisterRect(p2T2Rect);
		MenuItemManager.RegisterRect(p2T3Rect);
	}
	
	void OnGUI () {
		scale.y = Screen.height/oh;
		scale.x = Screen.width/ow;
		scale.z = 1;
		float scaleX = Screen.width/ow;
		Matrix4x4 svMat = GUI.matrix;
		GUI.matrix = Matrix4x4.TRS(new Vector3((scaleX - scale.y)/2 * ow,0,0),Quaternion.identity,scale);
		if(GUI.Button(p1T1Rect, "1:1"))
		{
			Tower baseTow = TurnOrder.GetPlayerByNumber(1).GetTower(0);
			TowerSelection.LocalSelectSection(baseTow, -1);
		}

		if(GUI.Button(p1T2Rect, "1:2"))
		{
			Tower baseTow = TurnOrder.GetPlayerByNumber(1).GetTower(1);
			TowerSelection.LocalSelectSection(baseTow, -1);
		}
		
		if(GUI.Button(p1T3Rect, "1:3"))
		{
			Tower baseTow = TurnOrder.GetPlayerByNumber(1).GetTower(2);
			TowerSelection.LocalSelectSection(baseTow, -1);
		}
		
		if(GUI.Button(p2T1Rect, "2:1"))
		{
			Tower baseTow = TurnOrder.GetPlayerByNumber(2).GetTower(0);
			TowerSelection.LocalSelectSection(baseTow, -1);
		}
		
		if(GUI.Button(p2T2Rect, "2:2"))
		{
			Tower baseTow = TurnOrder.GetPlayerByNumber(2).GetTower(1);
			TowerSelection.LocalSelectSection(baseTow, -1);
		}
		
		if(GUI.Button(p2T3Rect, "2:3"))
		{
			Tower baseTow = TurnOrder.GetPlayerByNumber(2).GetTower(2);
			TowerSelection.LocalSelectSection(baseTow, -1);
		}
	}
}
