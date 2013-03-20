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
	public GUIStyle veryActive;
	public bool show;
	private Vector3 scale;
	private float ow;
	private float oh;

	// Use this for initialization
	void Start () {
		ow = 960;
		oh = 600;
		top = 450;
		currentPlayer = TurnOrder.myPlayer;
		show = true;
	}
	
	// Update is called once per frame
	void Update () {
		//char star = '\u2606';
		//Debug.Log (star);
		currentPlayer = TurnOrder.GetPlayerByNumber(TowerSelection.GetSelectedTower().GetPlayerNum());
	}
	
	void OnGUI() {
		if(show) {
		scale.y = Screen.height/oh;
		scale.x = Screen.width/ow;
		scale.z = 1;
		float scaleX = Screen.width/ow;
		GUI.matrix = Matrix4x4.TRS(new Vector3((scaleX - scale.y)/2 * ow,0,0),Quaternion.identity,scale);
		GUIStyle p1Style;
		GUIStyle p2Style;
		if(currentPlayer == TurnOrder.player1) {
			p1Style = veryActive;
			p2Style = nonActive;
		}
		else {
			p2Style = veryActive;
			p1Style = nonActive;
		}
		if(GUI.Button(new Rect(5,top + 120,110,30),"Player1",p1Style)) {
			currentPlayer = TurnOrder.player1;
			TowerSelection.LocalSelectSection(currentPlayer.GetTower (0), -1);
		}
		if(GUI.Button(new Rect(115,top + 120,110,30),"Player2",p2Style)) {
			currentPlayer = TurnOrder.player2;
			TowerSelection.LocalSelectSection(currentPlayer.GetTower (0), -1);
		}
    	if(GameObject.Find("MainMenu/Fight").GetComponent<WeaponAnimator>().getSplitScreen() == false) {
			Tower[] towers = currentPlayer.GetTowers(); //selectedTower = TowerSelection.GetSelectedTower();
			for(int j = 0; j < towers.Length; j++) {
				int height = towers[j].GetSections().Count;
				Rect b = new Rect((5 * (j+1)) + (j*70),top+90,70,30);
				if(GUI.Button (b, towers[j].faction.factionName, baseStyle)) {
					//Section sc = t.GetSection(i);
					TowerSelection.LocalSelectSection(towers[j], -1); //FIX ME!!!!
				}
				if(height > 0) {
					for(int i = 0; i < height; i++) {
						GUIStyle style;
						string towerStat = "";
						//towers[j].GetSection(i).attributes.weapon.GetDamage().ToString();
						//GUIStyle style = GetInspectorStyle(selectedTower, i, false);
						char star = '\u2605';
						Section s = towers[j].GetSection(i);
						string wtype = s.attributes.weapon.GetWeaponType();
						if(wtype == "Nothing") {
							towerStat = "";	
						}
						else if(wtype == "Blaster" || wtype == "Pistols" || wtype == "Arrows") {
							towerStat = star + "";
						}
						else if(wtype == "Disintegration Beam" || wtype == "Gattling Gun" || wtype == "Spirit 1") {
							towerStat = star + " " + star;
						}
						else {
							towerStat = star + " " + star + " " + star;
						}
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
							r = new Rect((5 * (j+1)) + (j*70),(top+(90))-(150/(height)*(i+1)),70,150/(height));
						} else {
							r = new Rect((5 * (j+1)) + (j*70),(top+(90))-(30*(i+1)),70,30);
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
