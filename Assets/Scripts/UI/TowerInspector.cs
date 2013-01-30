using UnityEngine;
using System.Collections;

public class TowerInspector : MonoBehaviour {

    private bool p1Collapse = true;
    private bool p2Collapse = true;
    public GUIStyle wgStyle;
    public GUIStyle ngStyle;
    public GUIStyle wyStyle;
    public GUIStyle nyStyle;
    public GUIStyle wrStyle;
    public GUIStyle nrStyle;
    public GUIStyle nadarStyle;
    public GUIStyle nadayStyle;
    public GUIStyle nadagStyle;
    public GUIStyle p1Closed;
    public GUIStyle p1Open;
    public GUIStyle p2Closed;
    public GUIStyle p2Open;
	public GUIStyle player1Label;
	public GUIStyle player2Label;
	public int top;
	private Rect p1OpenRect;
	private Rect p1ClosedRect;
	private Rect p2OpenRect;
	private Rect p2ClosedRect;
	
	void Start() {
		UpdateRects();
		top = 100;
		p1OpenRect = new Rect(0,top,200,(Screen.height/3)+(Screen.height/10));
		p1ClosedRect = new Rect(-140,top,200,(Screen.height/3)+(Screen.height/10));
		p2OpenRect = new Rect(Screen.width-200,top,200,(Screen.height/3)+(Screen.height/10));
		p2ClosedRect = new Rect(Screen.width-60,top,200,(Screen.height/3)+(Screen.height/10));
	}
	
	private void UpdateRects() {
		MenuItemManager.RegisterRect(p1Collapse ? p1ClosedRect : p1OpenRect);
		MenuItemManager.UnregisterRect(!p1Collapse ? p1ClosedRect : p1OpenRect);
		MenuItemManager.RegisterRect(p2Collapse ? p2ClosedRect : p2OpenRect);
		MenuItemManager.UnregisterRect(!p2Collapse ? p2ClosedRect : p2OpenRect);
	}
	
    void OnGUI() {
    	if(GameObject.Find("Main/Fight").GetComponent<WeaponAnimator>().getSplitScreen() == false) {
        Player player1 = TurnOrder.GetPlayerByNumber(1);
        Player player2 = TurnOrder.GetPlayerByNumber(2);
        Tower p1Tower = player1.GetTower();
        Tower p2Tower = player2.GetTower();
		int p1Height = p1Tower.GetSections().Count;
		int p2Height = p2Tower.GetSections().Count;
		/* inspector collapse button */
        if(!p1Collapse) {
            GUI.Box(p1OpenRect,"",p1Open);
            if(GUI.Button (new Rect(130,top,50,(Screen.height/3)+(Screen.height/10)),"",nadagStyle)) {
                p1Collapse = true;
				UpdateRects();
            }
			GUI.Label(new Rect(150,top+20,50,50),"P1",player1Label);
        }
		else {
			if(GUI.Button (p1ClosedRect,"",p1Closed)) {
				p1Collapse = false;
				UpdateRects();
			}
			GUI.Label(new Rect(10,top+20,50,50),"P1",player1Label);
		}
        if(!p2Collapse) {
            GUI.Box(p2OpenRect,"",p2Open);
            if(GUI.Button (new Rect(Screen.width-180,top,50,(Screen.height/3)+(Screen.height/10)),"",nadagStyle)) {
                p2Collapse = true;
				UpdateRects();
            }
			GUI.Label (new Rect(Screen.width-170,top+20,50,50),"P2",player2Label);
        }
		else {
			if(GUI.Button (p2ClosedRect,"",p2Closed)) {
				p2Collapse = false;
				UpdateRects();
			}
			GUI.Label (new Rect(Screen.width-30,top+20,50,50),"P2",player2Label);
		}
		/* the inspector */
        if(!p1Collapse && p1Height > 0) {
			for(int i = 0; i < p1Height; i++) {
				GUIStyle style = GetInspectorStyle(p1Tower, i, false);
				Section s = p1Tower.GetSection(i);
				string towerStat = " " + p1Tower.GetWeightAboveSection(i) + "/" + s.GetSP();
				Rect r;
				if(p1Height > 6) {
					r = new Rect(5,(top+(Screen.height/3))-((Screen.height/3)/(p1Height)*(i)),120,(Screen.height/3)/(p1Height));
				} else {
					r = new Rect(5,(top+(Screen.height/3))-(30*i),120,30);
				}
				RenderButton(r, towerStat, style, p1Tower, i);
			}
        }
        if(!p2Collapse && p2Height > 0) {
			for(int i = 0; i < p2Height; i++) {
				GUIStyle style = GetInspectorStyle(p2Tower, i, false);
				GUIStyle nadaStyle = GetInspectorStyle(p2Tower, i, true);
				Section s = p2Tower.GetSection(i);
				string towerStat = p2Tower.GetWeightAboveSection(i) + "/" + s.GetSP();
				Rect buttonRect;
				Rect boxRect;
				if(p2Height > 6) {
					buttonRect = new Rect(Screen.width-125,(top+(Screen.height/3))-((Screen.height/3)/(p2Height)*(i)),120,(Screen.height/3)/(p2Height));
					boxRect = new Rect(Screen.width-5,(top+(Screen.height/3))-((Screen.height/3)/(p2Height)*(i)),-120,(Screen.height/3)/(p2Height));
				} else {
					buttonRect = new Rect(Screen.width-125,(top+(Screen.height/3))-(30*(i)),120,30);
					boxRect = new Rect(Screen.width-5,(top+(Screen.height/3))-(30*(i)),-120,30);
				}
				GUI.Box(boxRect, "", style);
				RenderButton(buttonRect, towerStat, nadaStyle, p2Tower, i);
			}
        }
        }
    }
	
	private void RenderButton(Rect r, string text, GUIStyle style, Tower t, int i) {
		if(GUI.Button (r, text, style)) {
			SectionController sc = t.GetSections()[i].GetComponent<SectionController>();
			TowerSelection.NetworkedSelectSection(sc.GetPlayer().playerNumber, i);
		}
	}
	
	private GUIStyle GetInspectorStyle(Tower t, int i, bool nada) {
		Section s = t.GetSection(i);
		GUIStyle gStyle;
		GUIStyle yStyle;
		GUIStyle rStyle;
		if(nada) {
			gStyle = nadagStyle;
		    yStyle = nadayStyle;
		    rStyle = nadarStyle;
		} else {
			bool hasWeapon = s.GetWeapon().GetWeaponType() != "Nothing";
			gStyle = hasWeapon ? wgStyle : ngStyle;
		    yStyle = hasWeapon ? wyStyle : nyStyle;
		    rStyle = hasWeapon ? wrStyle : nrStyle;
		}
		int stress = t.GetWeightAboveSection(i); //WHY DO I HAVE TO DO THIS
		int maxSP = s.GetSP();
		double ratio = (double)stress / (double)maxSP;
		if(ratio < 0.33) {
			return gStyle;
		} else if(ratio >= 0.33 && ratio < 0.66) {
			return yStyle;
		} else {
			return rStyle;
		}
	}
}
