using UnityEngine;
using System.Collections;

public class UpgradeMenu : Menu {
	private bool hasLoaded = false;
	public Vector2 buttonSize;
	public Vector2 boxSize;
	public AudioClip click;
	private int numButtons;
	private int lastFrameActionNum;
	private bool firstUpdate = true;
	private GameObject upgradeButton;
	private string scriptName = "";
	
	void Start() {
		on = false;
		buttonSize.x = 60;
		buttonSize.y = 50;
		boxSize.x = 150;
		boxSize.y = 30;
		lastFrameActionNum = TurnOrder.actionNum;
	}
	
	public override void Update() {
		
		if (firstUpdate)
		{
			scriptName = "PoisonAction";
			Rect upgradeRect = new Rect(Screen.width/2 + 180,Screen.height/2 - 120,boxSize.x,boxSize.y);

			upgradeButton = createGUIButton(scriptName,"Poison",upgradeRect);
			numButtons++;
			firstUpdate = false;
		}
		if(lastFrameActionNum != TurnOrder.actionNum){	
			Rect upgradeRect = new Rect(Screen.width/2 + 180,Screen.height/2 - 120,boxSize.x,boxSize.y);
			string oldScriptName = scriptName;
			string goName = "";
			if (TurnOrder.myPlayer.GetTower(TurnOrder.actionNum).faction.factionName == "Totem")
			{
				scriptName = "PoisonAction";
				goName = "Poison";
			}
			else if(TurnOrder.myPlayer.GetTower(TurnOrder.actionNum).faction.factionName == "Cowboys")
			{
				scriptName = "TagAction";
				goName = "Tag Section";
			}
			else if (TurnOrder.myPlayer.GetTower(TurnOrder.actionNum).faction.factionName == "Area 51")
			{
				scriptName = "ParalyzeAction";
				goName = "Paralyze";
			}
			else{
				Debug.Log ("Error - Faction name not matched properly.");
			}
			editUpgradeButton(oldScriptName, scriptName, goName);
			lastFrameActionNum = TurnOrder.actionNum;
		}
		if(!hasLoaded) {

			Rect damRect = new Rect(Screen.width/2 + 180,Screen.height/2 - 85,boxSize.x,boxSize.y);
			GameObject damage = createGUIButton("DamageAction","Upgrade",damRect);
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
	private void editUpgradeButton(string oldScriptName, string scriptName, string goName)
	{
		Destroy(upgradeButton.GetComponent(oldScriptName));
		upgradeButton.AddComponent(scriptName);
		MenuItem m = upgradeButton.GetComponent<MenuItem>();
		m.action = upgradeButton.GetComponent(scriptName) as DefaultMenuAction;
		m.action.click = click;		
		m.text = goName;
	}
}
