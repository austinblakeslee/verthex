using UnityEngine;
using System.Collections;

public class FortifyMenu : Menu {
	private bool hasLoaded = false;
	public Vector2 buttonSize;
	public Vector2 boxSize;
	public AudioClip click;
	private int numButtons;
	public GUISkin squareStyle;
	
	void Start() {
		on = false;
		buttonSize.x = 60;
		buttonSize.y = 50;
		boxSize.x = 150;
		boxSize.y = 30;
	}
	
	public override void Update() {
		if(!hasLoaded) {
			this.guiSkin = squareStyle;
			Rect rpLabelRect = new Rect(760,255,boxSize.x,boxSize.y);
			GameObject rlabel = MakeBox ("RP",rpLabelRect);
			rlabel.AddComponent("FortifyCostLabel");
			rlabel.transform.parent = transform;
			MenuItem m1 = rlabel.GetComponent<MenuItem>();
			m1.action = rlabel.GetComponent<DefaultMenuAction>();
			menuItems.Add(m1);
			Rect weaponsLabelRect = new Rect(760,295,boxSize.x,boxSize.y);
			GameObject slabel = MakeBox ("SP",weaponsLabelRect);
			slabel.AddComponent("FortifyHealthLabel");
			slabel.transform.parent = transform;
			MenuItem m3 = slabel.GetComponent<MenuItem>();
			m3.action = slabel.GetComponent<DefaultMenuAction>();
			menuItems.Add(m3);
			Rect confRect = new Rect(760,335,boxSize.x,boxSize.y);
			createGUIButton("FortifyAction","Confirm",confRect);
			numButtons++;
			Rect backRect = new Rect(760,375,boxSize.x,boxSize.y);
			GameObject back = createGUIButton("SwitchMenu","Back",backRect);
			SwitchMenu backSM = back.GetComponent<SwitchMenu>();
			backSM.fromMenu = this.gameObject;
			backSM.toMenu = this.transform.parent.gameObject;
			numButtons++;
			hasLoaded = true;
		}
		base.Update();
	}
	
	private GameObject MakeButton(string text, Rect rect) {
		GameObject item = new GameObject(text);
		item.AddComponent("MenuItem");
		MenuItem m = item.GetComponent<MenuItem>();
		m.left = ""+rect.xMin;
		m.top = ""+rect.yMin;
		m.width = ""+rect.width;
		m.height = ""+rect.height;
		m.type = MenuItem.MenuItemType.Button;
		m.visible = true;
		m.text = text;
		return item;
	}
	
	private GameObject MakeBox(string text, Rect rect) {
		GameObject item = new GameObject(text);
		item.AddComponent("MenuItem");
		MenuItem m = item.GetComponent<MenuItem>();
		m.left = ""+rect.xMin;
		m.top = ""+rect.yMin;
		m.width = ""+rect.width;
		m.height = ""+rect.height;
		m.type = MenuItem.MenuItemType.Box;
		m.visible = true;
		m.text = "";
		return item;
	}
	
	private GameObject createGUIButton(string scriptName, string goName, Rect itemButtonRect) {
		//Rect itemButtonRect = new Rect(0,0,buttonSize.x,buttonSize.y); 
		//itemButtonRect = FindPos(numButtons, itemButtonRect);
		GameObject item = MakeButton(goName,itemButtonRect);
		item.AddComponent(scriptName);
		item.transform.parent = transform;
		MenuItem m = item.GetComponent<MenuItem>();
		m.action = item.GetComponent(scriptName) as DefaultMenuAction;
		m.action.click = click;
		menuItems.Add(m);
		return item;
	}
}
