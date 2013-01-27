using UnityEngine;
using System.Collections;

public class TowerSelection : MonoBehaviour {

	private static SectionController selectedSection;
	private static SectionController oldSelect;
	public Vector2 materialBox;
	public Vector2 weaponBox;
	public int padding;
	private Rect materialBoxRect;
	private Rect weaponBoxRect;
	
	public Texture materialBoxTexture;
	public Texture weaponBoxTexture;
	
	public Texture2D stress;
	public Texture2D currentSP;
	public Texture2D maxSP;
	public Texture2D noUpgrade;
	public Texture2D upgrade;
	
	public GUISkin skin;
	public GUISkin skinSmall;
	
	public MenuItem fortifyRP;
	public MenuItem fortifySP;
	public MenuItem dotButton;
	public MenuItem aoeButton;
	
	void Start() {
		materialBoxRect = new Rect(Screen.width - (Screen.width*(materialBox.x/960)) - padding, Screen.height - Screen.height*(materialBox.y/600) - padding, (Screen.width*(materialBox.x/960)), Screen.height*(materialBox.y/600));
		weaponBoxRect = new Rect(Screen.width - (Screen.width*(weaponBox.x/960)) - padding, Screen.height - (Screen.height*(materialBox.y/600)) - (Screen.height*(weaponBox.y/600)) - padding*2, (Screen.width*(weaponBox.x/960)), (Screen.height*(weaponBox.y/600)));
	}

	void Update () {
		if(Input.GetMouseButtonDown(0) && !MenuItemManager.MouseIsInGUI()) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit, 10000.0f)) {
				if(hit.collider == null) {
					SelectSection(null);
					return;
				}
				SectionController s = hit.collider.GetComponent<SectionController>();
				SelectSection(s);
				audio.Play();
			} else {
				SelectSection(null);
			}
		}
		if(selectedSection != null) {
			SectionMaterial m = selectedSection.GetSection().GetMaterial();
			fortifyRP.text = ""+m.GetCostPerRepair();
			fortifySP.text = ""+m.GetSPPerRepair();
			SectionWeapon w = selectedSection.GetSection().GetWeapon();
			if(w.GetEffect().GetEffectType() == "Multi") {
				dotButton.visible = false;
				aoeButton.visible = true;
			} else if(w.GetEffect().GetEffectType() == "Burn") {
				aoeButton.visible = false;
				dotButton.visible = true;
			} else {
				aoeButton.visible = true;
				dotButton.visible = true;
			}
		} else {
			fortifyRP.text = "";
			fortifySP.text = "";
		}
	}
	
	void OnGUI() {
		GUI.skin = skin;
		if(selectedSection != null) {
			DrawMaterialBox();
			if(selectedSection.GetSection().GetWeapon().GetWeaponType() != "Nothing") {
				DrawWeaponBox();
			}
		}
	}
	
	private void DrawMaterialBox() {
		GUI.Box(materialBoxRect, "");
		GUI.DrawTexture(materialBoxRect, materialBoxTexture);
		GUI.BeginGroup(materialBoxRect);
		
		/* health bar */
		float maxSPRatio = selectedSection.GetSection().GetSP() / (float)selectedSection.GetSection().GetMaxSP();
		float stressRatio = selectedSection.GetStress() / (float)selectedSection.GetSection().GetMaxSP();
		int width = 125;
		int height = 30;
		int left = (int)materialBoxRect.width - 15 - width;
		int top = 10;
		GUI.DrawTexture(new Rect(left, top, width, height), maxSP);
		GUI.BeginGroup(new Rect(left, top, width*maxSPRatio, height));
		GUI.DrawTexture(new Rect(0, 0, width, height), currentSP);
		GUI.EndGroup();
		GUI.BeginGroup(new Rect(left, top, width*stressRatio, height));
		GUI.DrawTexture(new Rect(0, 0, width, height), stress);
		GUI.EndGroup();
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		style.normal.textColor = Color.black;
		style.fontSize = 12;
		GUI.Label(new Rect(left, top, width, height), selectedSection.GetMaterialInfo(), style);
		
		/* section material label */
		left = 15;
		top = 15;
		height = 30;
		width = 50;
		GUI.Label(new Rect(left, top, width, height), selectedSection.GetSection().GetMaterial().GetMaterialType());
		
		/* dot effect */
		if(selectedSection.HasDot()) {
			left = 15;
			top = 45;
			height = 30;
			width = 200;
			
			GUI.skin = skinSmall;
			GUI.Label(new Rect(left, top, width, height), selectedSection.GetDotInfo());
			GUI.skin = skin;
		}
		GUI.EndGroup();
	}
	
	private void DrawWeaponBox() {
		GUI.Box(weaponBoxRect, "");
		GUI.DrawTexture(weaponBoxRect, weaponBoxTexture);
		GUI.BeginGroup(weaponBoxRect);
		
		/* weapon name and effect */
		int left = 15;
		int top = 15;
		int height = 70;
		int width = 210;
		GUI.Label(new Rect(left, top, width, height), selectedSection.GetWeaponInfo());
		
		/* Damage Upgrade level */
		left = 15;
		top = 85;
		width = 200;
		height = 60;
		GUI.BeginGroup(new Rect(left, top, width, height));
		GUI.Label(new Rect(0, 0, 60, 30), "Damage: ");
		Rect r = new Rect(70, 4, 16, 16);
		Texture2D toDraw = upgrade;
		for(int i=0; i < 5; i++) {
			if(i > selectedSection.GetSection().GetWeapon().GetDamageUpgradeLevel()) {
				toDraw = noUpgrade;
			}
			GUI.DrawTexture(r, toDraw);
			r = new Rect(r.left + 16, r.top, r.width, r.height);
		}
		GUI.EndGroup();
		
		/* Effect Upgrade level */
		if(selectedSection.GetSection().GetWeapon().GetEffect().GetEffectType() != "none") {
			left = Screen.width*(15/960);
			top = Screen.height*(110/600);
			width = Screen.width*(200/960);
			height = Screen.height*(60/600);
			GUI.BeginGroup(new Rect(left, top, width, height));
			GUI.Label(new Rect(0, 0, 60, 30), selectedSection.GetSection().GetWeapon().GetEffect().GetEffectType() + ": ");
			r = new Rect(70, 4, 16, 16);
			toDraw = upgrade;
			for(int i=0; i < 4; i++) {
				if(i > selectedSection.GetSection().GetWeapon().GetEffect().GetUpgradeLevel()) {
					toDraw = noUpgrade;
				}
				GUI.DrawTexture(r, toDraw);
				r = new Rect(r.left + 16, r.top, r.width, r.height);
			}
			GUI.EndGroup();
		}
		
		
		GUI.EndGroup();
	}
	
	public static SectionController GetSelectedSection() {
		return selectedSection;
	}
	
	public static void SelectSection(SectionController s) {
		if(selectedSection != null) {
			selectedSection.SetColor(Color.white);
		}
		selectedSection = s;
		MainCamera mc = GameObject.FindWithTag("MainCamera").GetComponent<MainCamera>();
		if(s == null) {
			mc.ChangeTarget(TurnOrder.currentPlayer.towerBase.transform);
		} else {
			mc.ChangeTarget(s.transform);
			selectedSection.SetColor(TurnOrder.currentPlayer.color);
		}
	}
	
	public void deselectSection() {
		oldSelect = selectedSection;
		selectedSection = null;
	}
	
	public void reselectSection() {
		if(oldSelect != null) {
			selectedSection = oldSelect;
		}
	}
}
