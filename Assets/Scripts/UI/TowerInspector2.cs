using UnityEngine;
using System.Collections;

public class TowerInspector2 : MonoBehaviour {
	public int top;
	Player currentPlayer;

	// Use this for initialization
	void Start () {
		top = 450;
		currentPlayer = TurnOrder.myPlayer;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		if(GUI.Button(new Rect(5,top - 30,110,30),"Player1")) {
			currentPlayer = TurnOrder.player1;
			TowerSelection.LocalSelectSection(currentPlayer.GetTower (0), -1);
			
		}
		if(GUI.Button(new Rect(115,top - 30,110,30),"Player2")) {
			currentPlayer = TurnOrder.player2;
			TowerSelection.LocalSelectSection(currentPlayer.GetTower (0), -1);
		}
    	if(GameObject.Find("MainMenu/Fight").GetComponent<WeaponAnimator>().getSplitScreen() == false) {
			Tower[] towers = currentPlayer.GetTowers(); //selectedTower = TowerSelection.GetSelectedTower();
			for(int j = 0; j < towers.Length; j++) {
				int height = towers[j].GetSections().Count;
				Rect b = new Rect((5 * (j+1)) + (j*70),top+120,70,30);
				if(GUI.Button (b, "-")) {//, style
					//Section sc = t.GetSection(i);
					TowerSelection.LocalSelectSection(towers[j], -1); //FIX ME!!!!
				}
				if(height > 0) {
					for(int i = 0; i < height; i++) {
						//GUIStyle style = GetInspectorStyle(selectedTower, i, false);
						Section s = towers[j].GetSection(i);
						string towerStat = towers[j].GetSection(i).attributes.weapon.GetWeaponType();
						Rect r;
						if(height > 3) {
							r = new Rect((5 * (j+1)) + (j*70),(top+(120))-(150/(height)*(i+1)),70,150/(height));
						} else {
							r = new Rect((5 * (j+1)) + (j*70),(top+(120))-(30*(i+1)),70,30);
						}
						RenderButton(r, towerStat, towers[j], i);// style,
					}
				}
			}
        }
    }
	
	private void RenderButton(Rect r, string text, Tower t, int i) {// GUIStyle style,
		if(GUI.Button (r, text)) {//, style
			Section sc = t.GetSection(i);
			TowerSelection.LocalSelectSection(t, i); //FIX ME!!!!
		}
	}
}
