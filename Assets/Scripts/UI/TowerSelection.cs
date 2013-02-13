using UnityEngine;
using System.Collections;

public class TowerSelection : MonoBehaviour {

	private static SectionController selectedSection;
	private static SectionController oldSelect;
	private static TowerSelection instance;
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
		materialBoxRect = new Rect(230, Screen.height - materialBox.y - padding, materialBox.x, materialBox.y);
		weaponBoxRect = new Rect(230, Screen.height - materialBox.y - weaponBox.y - padding*2, weaponBox.x, weaponBox.y);
		instance = this;
		if(Network.isServer) {
			LocalSelectSection(1, -1);
		} else {
			LocalSelectSection(2, -1);
		}
	}
	
	public static void NetworkedSelectSection(int playerNum, int sectionNum) {
		if(Network.isClient || Network.isServer) {
			instance.networkView.RPC("SelectSection", RPCMode.All, playerNum, sectionNum);
		} else {
			instance.SelectSection(playerNum, sectionNum);
		}
	}
	
	public static void LocalSelectSection(int playerNum, int sectionNum) {
		instance.SelectSection(playerNum, sectionNum);
	}

	void Update () {
		if(Input.GetMouseButtonDown(0) && !MenuItemManager.MouseIsInGUI()) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit, 10000.0f)) {
				if(hit.collider == null || hit.collider.tag != "Section") {
					LocalSelectSection(-1, -1);
					return;
				}
				SectionController s = hit.collider.GetComponent<SectionController>();
				int playerNum = s.GetPlayer().playerNumber;
				int sectionNum = s.GetHeight()-1;
				LocalSelectSection(playerNum, sectionNum);
				audio.Play();
			} else {
				LocalSelectSection(-1, -1);
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
		int width = 210;
		int height = 70;
		GUI.Label(new Rect(left, top, width, height), selectedSection.GetWeaponInfo());
		//GUI.Label(new Rect(left, top, width, height), "Level " + selectedSection.GetSection().GetWeapon().GetDamageUpgradeLevel().ToString());

		/* Damage Upgrade level */
		left = 15;
		top = 75;
		width = 200;
		height = 60;
		GUI.BeginGroup(new Rect(left, top, width, height));
		GUI.Label(new Rect(0, 0, 60, 30), "Damage: ");
		Rect r = new Rect(70, 4, 16, 16);
		Texture2D toDraw = upgrade;
		for(int i=0; i <= 7; i++) {
			//if(i > selectedSection.GetSection().GetWeapon().GetDamageUpgradeLevel()) {
			//120 is about the maximum number of health a weapon can have in this game right now. This should give a better visual indication of a weapons power.
			if((i+1)/8f > selectedSection.GetSection().GetWeapon().GetDamage()/120f){ 

				toDraw = noUpgrade;
			}
			GUI.DrawTexture(r, toDraw);
			r = new Rect(r.xMin + 16, r.yMin, r.width, r.height);
		}
		
		GUI.EndGroup();
		
		/* Weapon Range */
		left = 15;
		top = 95;
		width = 200;
		height = 60;
		GUI.BeginGroup(new Rect(left, top, width, height));
		GUI.Label (new Rect(0, 0, 60, 30), "Range: ");
		r = new Rect(70, 4, 16, 16);
		toDraw = upgrade;
		//max range is 5 right now
		for(int i = 0; i < 5; i++){
			if(i > selectedSection.GetSection().GetWeapon().GetRange()  - 1){
				toDraw = noUpgrade;
			}
			GUI.DrawTexture (r, toDraw);
			r = new Rect(r.xMin + 16, r.yMin, r.width, r.height);
		}
		
		GUI.EndGroup ();
		/* Effect Upgrade level */
		if(selectedSection.GetSection().GetWeapon().GetEffect().GetEffectType() != "none") {
			left = 15;
			top = 115;
			width = 200;
			height = 60;
			GUI.BeginGroup(new Rect(left, top, width, height));
			GUI.Label(new Rect(0, 0, 60, 30), selectedSection.GetSection().GetWeapon().GetEffect().GetEffectType() + ": ");
			r = new Rect(70, 4, 16, 16);
			toDraw = upgrade;
			for(int i=0; i < 4; i++) {
				if(i > selectedSection.GetSection().GetWeapon().GetEffect().GetUpgradeLevel()) {
					toDraw = noUpgrade;
				}
				GUI.DrawTexture(r, toDraw);
				r = new Rect(r.xMin + 16, r.yMin, r.width, r.height);
			}
			GUI.EndGroup();
		}
		
		
		GUI.EndGroup();
	}
	
	public static SectionController GetSelectedSection() {
		return selectedSection;
	}
	
	[RPC]
	private void SelectSection(int playerNumber, int sectionNumber) {
		Player p = null;
		if(playerNumber == 1) {
			p = TurnOrder.player1;
		} else if(playerNumber == 2) {
			p = TurnOrder.player2;
		} else {
			p = TurnOrder.myPlayer;
		}
		MainCamera mc = GameObject.FindWithTag("MainCamera").GetComponent<MainCamera>();
		if(selectedSection != null) {
			selectedSection.SetColor(Color.white);
		}
		if(sectionNumber < 0) {
			selectedSection = null;
			mc.ChangeTarget(p.towerBase.transform);
		} else {
			SectionController s = p.GetTower().GetSections()[sectionNumber].GetComponent<SectionController>();
			selectedSection = s;
			mc.ChangeTarget(s.transform);
			s.SetColor(TurnOrder.myPlayer.color);
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
