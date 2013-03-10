using UnityEngine;
using System.Collections;

public class TowerInspector : MonoBehaviour {

    private bool collapse = true;
    public GUIStyle wgStyle;
    public GUIStyle ngStyle;
    public GUIStyle wyStyle;
    public GUIStyle nyStyle;
    public GUIStyle wrStyle;
    public GUIStyle nrStyle;
    public GUIStyle nadarStyle;
    public GUIStyle nadayStyle;
    public GUIStyle nadagStyle;
    public GUIStyle closed;
    public GUIStyle open;
	public int top;
	private Rect openRect;
	private Rect closedRect;
	
	void Start() {
		UpdateRects();
		top = 100;
		openRect = new Rect(0,top,200,(Screen.height/3)+(Screen.height/10));
		closedRect = new Rect(-140,top,200,(Screen.height/3)+(Screen.height/10));
	}
	
	private void UpdateRects() {
		MenuItemManager.RegisterRect(collapse ? closedRect : openRect);
		MenuItemManager.UnregisterRect(!collapse ? closedRect : openRect);
	}
	
    void OnGUI() {
    	if(GameObject.Find("MainMenu/Fight").GetComponent<WeaponAnimator>().getSplitScreen() == false) {
			Tower selectedTower = TowerSelection.GetSelectedTower();
			int height = selectedTower.GetSections().Count;
			/* inspector collapse button */
			if(!collapse) {
				GUI.Box(openRect,"",open);
				if(GUI.Button (new Rect(130,top,50,(Screen.height/3)+(Screen.height/10)),"",nadagStyle)) {
					collapse = true;
					UpdateRects();
				}
			}
			else {
				if(GUI.Button (closedRect,"",closed)) {
					collapse = false;
					UpdateRects();
				}
			}
			/* the inspector */
			if(!collapse && height > 0) {
				for(int i = 0; i < height; i++) {
					GUIStyle style = GetInspectorStyle(selectedTower, i, false);
					Section s = selectedTower.GetSection(i);
					string towerStat = " " + selectedTower.GetWeightAboveSection(i) + "/" + s.attributes.sp;
					Rect r;
					if(height > 6) {
						r = new Rect(5,(top+(Screen.height/3))-((Screen.height/3)/(height)*(i)),120,(Screen.height/3)/(height));
					} else {
						r = new Rect(5,(top+(Screen.height/3))-(30*i),120,30);
					}
					RenderButton(r, towerStat, style, selectedTower, i);
				}
			}
        }
    }
	
	private void RenderButton(Rect r, string text, GUIStyle style, Tower t, int i) {
		if(GUI.Button (r, text, style)) {
			TowerSelection.LocalSelectSection(t, i); //FIX ME!!!!
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
			bool hasWeapon = s.attributes.weapon.GetWeaponType() != "Nothing";
			gStyle = hasWeapon ? wgStyle : ngStyle;
		    yStyle = hasWeapon ? wyStyle : nyStyle;
		    rStyle = hasWeapon ? wrStyle : nrStyle;
		}
		int stress = t.GetWeightAboveSection(i);
		int maxSP = s.attributes.sp;
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
