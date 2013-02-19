using UnityEngine;
using System.Collections;

public class UpgradeMenu : Menu {
	private bool hasLoaded = false;
	public Vector2 buttonSize;
	public Vector2 boxSize;
	public AudioClip click;
	private int numButtons;
	
	void Start() {
		on = false;
		buttonSize.x = 60;
		buttonSize.y = 50;
		boxSize.x = 150;
		boxSize.y = 30;
	}
	
	public override void Update() {
		if(!hasLoaded) {
			Rect aoeRect = new Rect(Screen.width/2 + 180,Screen.height/2 - 120,boxSize.x,boxSize.y);
			GameObject aoe = createGUIButton("AoEAction","MultiShot",aoeRect);
			numButtons++;
			Rect damRect = new Rect(Screen.width/2 + 180,Screen.height/2 - 85,boxSize.x,boxSize.y);
			GameObject damage = createGUIButton("DamageAction","Damage",damRect);
			numButtons++;
			Rect dotRect = new Rect(Screen.width/2 + 180,Screen.height/2 - 50,boxSize.x,boxSize.y);
			GameObject dot = createGUIButton("DoTAction","Burn",dotRect);
			numButtons++;
			Rect backRect = new Rect(Screen.width/2 + 180,Screen.height/2 - 5,boxSize.x,boxSize.y);
			GameObject back = createGUIButton("SwitchMenu","Back",backRect);
			back.GetComponent<SwitchMenu>().fromMenu = this.gameObject;
			back.GetComponent<SwitchMenu>().toMenu = this.transform.parent.gameObject;
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

	private Rect FindPos(int i, Rect rect) {
		if(i%2 != 0 && i%3 != 0 && i > 0) {
			rect.x = Screen.width - 150;
		}
		else if(i%3 == 0) {
			rect.x = Screen.width - 225;
		}
		else if(i%2 == 0 && i > 0) {
			rect.x = Screen.width - 75;
		}
		
		if(i <= 2) {
			rect.y = Screen.height - 165;
		}
		else if(i > 2 && i <= 5) {
			rect.y = Screen.height - 110;
		}
		else if(i > 5 && i <= 8) {
			rect.y = Screen.height - 55;
		}
		
		return rect;
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
