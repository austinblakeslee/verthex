using UnityEngine;
using System.Collections;

public class TowerInspector2 : MonoBehaviour {
	public int top;
	private Player currentPlayer;
	public GUIStyle nrStyle;
	public GUIStyle nyStyle;
	public GUIStyle ngStyle;
	public GUIStyle nbStyle;
	public GUIStyle baseStyle;
	public GUIStyle nonActive;
	public GUIStyle active;
	public bool show;

	// Use this for initialization
	void Start () {
		top = 450;
		currentPlayer = TurnOrder.myPlayer;
		show = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		if(show) {
		GUIStyle p1Style;
		GUIStyle p2Style;
		if(currentPlayer == TurnOrder.player1) {
			p1Style = active;
			p2Style = nonActive;
		}
		else {
			p2Style = active;
			p1Style = nonActive;
		}
		if(GUI.Button(new Rect(5,top - 30,110,30),"Player1",p1Style)) {
			currentPlayer = TurnOrder.player1;
			TowerSelection.LocalSelectSection(currentPlayer.GetTower (0), -1);
		}
		if(GUI.Button(new Rect(115,top - 30,110,30),"Player2",p2Style)) {
			currentPlayer = TurnOrder.player2;
			TowerSelection.LocalSelectSection(currentPlayer.GetTower (0), -1);
		}
    	if(GameObject.Find("MainMenu/Fight").GetComponent<WeaponAnimator>().getSplitScreen() == false) {
			Tower[] towers = currentPlayer.GetTowers(); //selectedTower = TowerSelection.GetSelectedTower();
			for(int j = 0; j < towers.Length; j++) {
				int height = towers[j].GetSections().Count;
				Rect b = new Rect((5 * (j+1)) + (j*70),top+120,70,30);
				if(GUI.Button (b, towers[j].faction.factionName, baseStyle)) {
					//Section sc = t.GetSection(i);
					TowerSelection.LocalSelectSection(towers[j], -1); //FIX ME!!!!
				}
				if(height > 0) {
					for(int i = 0; i < height; i++) {
						GUIStyle style;
						//GUIStyle style = GetInspectorStyle(selectedTower, i, false);
						Section s = towers[j].GetSection(i);
						string towerStat = towers[j].GetSection(i).attributes.weapon.GetDamage().ToString();
						int sp = s.attributes.sp - towers[j].GetWeightAboveSection(i);
						int initSP = s.attributes.material.initialSP;
						double ratio = (double)sp / (double)initSP;
						if(ratio < 0.33) {
							style = nrStyle;
						} else if(ratio >= 0.33 && ratio < 0.66) {
							style = nyStyle;
						} else if(ratio >= 0.66 && ratio <= 1.01) {
							style = ngStyle;
						} else {
							style = nbStyle;
						}
						Rect r;
						if(height > 3) {
							r = new Rect((5 * (j+1)) + (j*70),(top+(120))-(150/(height)*(i+1)),70,150/(height));
						} else {
							r = new Rect((5 * (j+1)) + (j*70),(top+(120))-(30*(i+1)),70,30);
						}
						RenderButton(r, towerStat, style, towers[j], i);
					}
				}
			}
        }
		}
    }
	
	private void RenderButton(Rect r, string text, GUIStyle style, Tower t, int i) {
		if(GUI.Button (r, text, style)) {
			TowerSelection.LocalSelectSection(t, i); //FIX ME!!!!
		}
	}
}
