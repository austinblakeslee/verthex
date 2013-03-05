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
	private GameObject upgradeButton1;
	private GameObject upgradeButton2;
	private string scriptName1 = "";
	private string scriptName2 = "";
	
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
			scriptName1 = "ParalyzeAction";
			scriptName2 = "AlterWeightAction";
			Rect upgradeRect1 = new Rect(Screen.width/2 + 180,Screen.height/2 - 120,boxSize.x,boxSize.y);
			Rect upgradeRect2 = new Rect(Screen.width/2 + 180,Screen.height/2 - 80,boxSize.x,boxSize.y);

			upgradeButton1 = createGUIButton(scriptName1,"Poison",upgradeRect1);
			numButtons++;
			
			upgradeButton2 = createGUIButton(scriptName2, "Alter Weight", upgradeRect2);
			numButtons++;
			firstUpdate = false;
		}
		if(lastFrameActionNum != TurnOrder.actionNum){	
			lastFrameActionNum = TurnOrder.actionNum;

			Rect upgradeRect1 = new Rect(Screen.width/2 + 180,Screen.height/2 - 120,boxSize.x,boxSize.y);
			Rect upgradeRect2 = new Rect(Screen.width/2 + 180,Screen.height/2 - 80,boxSize.x,boxSize.y);

			
			string oldScriptName1 = scriptName1;
			string oldScriptName2 = scriptName2;
			string goName1 = "";
			string goName2 = "";
			if (TurnOrder.myPlayer.GetTower(TurnOrder.actionNum).faction.factionName == "Totem")
			{
				scriptName1 = "ParalyzeAction";
				goName1 = "Paralyze";
				scriptName2 = "AlterWeightAction";
				goName2 = "Alter Weight";
			}
			else if(TurnOrder.myPlayer.GetTower(TurnOrder.actionNum).faction.factionName == "Cowboys")
			{
				scriptName1 = "TagAction";
				goName1 = "Tag Section";
				goName2 = "";
			}
			else if (TurnOrder.myPlayer.GetTower(TurnOrder.actionNum).faction.factionName == "Area 51")
			{
				scriptName1 = "DrainAction";
				goName1 = "Drain";
				scriptName2 = "ForceFieldAction";
				goName2 = "Force Field";
			}
			else{
				CombatLog.addLine("Error - Faction name not matched properly.");
			}
			upgradeButton1 = editUpgradeButton(upgradeButton1, oldScriptName1, scriptName1, goName1);
			if (goName2 != "")
			{	
				upgradeButton2.SetActive(true);
				upgradeButton2.renderer.enabled = true;

				upgradeButton2.GetComponent<MenuItem>().SetVisible(true);
				upgradeButton2 = editUpgradeButton(upgradeButton2, oldScriptName2, scriptName2, goName2);

			}
			else{
				CombatLog.addLine("Here");
				upgradeButton2.SetActive(false);
				upgradeButton2.renderer.enabled = false;
				upgradeButton2.GetComponent<MenuItem>().SetVisible(false);
			}
		}
		if(!hasLoaded) {

			Rect damRect = new Rect(Screen.width/2 + 180,Screen.height/2 - 40,boxSize.x,boxSize.y);
			GameObject damage = createGUIButton("DamageAction","Upgrade",damRect);
			numButtons++;

			Rect backRect = new Rect(Screen.width/2 + 180,Screen.height/2 - 0,boxSize.x,boxSize.y);
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
	private GameObject editUpgradeButton(GameObject upgradeButton, string oldScriptName, string scriptName, string goName)
	{
		Destroy(upgradeButton.GetComponent(oldScriptName));

		upgradeButton.AddComponent(scriptName);
		MenuItem m = upgradeButton.GetComponent<MenuItem>();

		m.action = upgradeButton.GetComponent(scriptName) as DefaultMenuAction;
		m.action.click = click;		
		m.text = goName;
		return upgradeButton;
	}
}
