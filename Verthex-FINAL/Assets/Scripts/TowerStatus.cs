using UnityEngine;
using System.Collections;

public class TowerStatus : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	/*void OnGUI() {
		int p1Height = player1.GetTower().GetSections().Count;
		Tower p1Tower = player1.GetTower();
		int p2Height = player2.GetTower().GetSections().Count;
		Tower p2Tower = player2.GetTower();
        while(p1Height > 0) {
			for(int i = 0; i < p1Height; i++) {
				string towerStat = i + ": " + p1Tower.GetSection(i).GetSP() + "/" + p1Tower.GetSection(i).GetMaxSP();
				if(GUI.Button (new Rect(0,50*i,100,50),towerStat)) {
					
				}
			}
		}
		while(p2Height > 0) {
			for(int i = 0; i < p2Height; i++) {
				string towerStat = i + ": " + p2Tower.GetSection(i).GetSP() + "/" + p2Tower.GetSection(i).GetMaxSP();
				if(GUI.Button (new Rect(0,50*i,100,50),towerStat)) {
					
				}
			}
		}
    }*/
}
