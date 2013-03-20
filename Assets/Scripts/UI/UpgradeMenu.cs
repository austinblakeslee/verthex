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
	private string info1 = "";
	private string info2 = "";
	public GUISkin squareStyle;
	MenuItem m1, m2;
	
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
			Rect upgradeRect1 = new Rect(660,180,boxSize.x,boxSize.y);
			Rect upgradeRect2 = new Rect(660,220,boxSize.x,boxSize.y);

			upgradeButton1 = createGUIButton(scriptName1,"Poison",upgradeRect1);
			numButtons++;
			upgradeButton2 = createGUIButton(scriptName2, "Alter Weight", upgradeRect2);
			numButtons++;
			firstUpdate = false;
		}
		if(lastFrameActionNum != TowerSelection.GetSelectedTower().towerNum){	
			lastFrameActionNum = TowerSelection.GetSelectedTower().towerNum;
			
			string oldScriptName1 = scriptName1;
			string oldScriptName2 = scriptName2;
			string goName1 = "";
			string goName2 = "";
			if (TowerSelection.GetSelectedTower().faction.factionName == "Totem")
			{
				scriptName1 = "ParalyzeAction";
				goName1 = "Paralyze";
				info1 = "Weapon is stunned. It won't deal damage until it is repaired.";
				scriptName2 = "AlterWeightAction";
				goName2 = "Alter Weight";
				info2 = "Aims at your own tower, and lowers weight of targeted section.";
			}
			else if(TowerSelection.GetSelectedTower().faction.factionName == "Cowboys")
			{
				scriptName1 = "TagAction";
				goName1 = "Tag Section";
				info1 = "Deals single target damage, and tags the target section for bonus damage on the next attack.";
				goName2 = "";
				info2 = "";
			}
			else if (TowerSelection.GetSelectedTower().faction.factionName == "Area 51")
			{
				scriptName1 = "DrainAction";
				goName1 = "Drain";
				info1 = "Gives you some damage dealt as back health.";
				scriptName2 = "ForceFieldAction";
				goName2 = "Force Field";
				info2 = "Prevents some damage to upgraded sections.";
			}
			else{
				CombatLog.addLine("Error - Faction name not matched properly.");
			}
			upgradeButton1 = editUpgradeButton(upgradeButton1, oldScriptName1, scriptName1, goName1);
			if (goName2 != "")
			{	

				upgradeButton2 = editUpgradeButton(upgradeButton2, oldScriptName2, scriptName2, goName2);

			}
			else{
				CombatLog.addLine("Here");
		
			}
			
		}
		if(!hasLoaded) {
			this.guiSkin = squareStyle;
			Rect damRect = new Rect(660,260,boxSize.x,boxSize.y);
			createGUIButton("DamageAction","Upgrade Damage",damRect);
			numButtons++;

			Rect backRect = new Rect(660,300,boxSize.x,boxSize.y);
			GameObject back = createGUIButton("SwitchMenu","Back",backRect);
			SwitchMenu backSM = back.GetComponent<SwitchMenu>();
			backSM.fromMenu = this.gameObject;
			backSM.toMenu = this.transform.parent.gameObject;
			numButtons++;
			m1 = upgradeButton1.GetComponent<MenuItem>();
			m2 = upgradeButton2.GetComponent<MenuItem>();
			hasLoaded = true;
		}
		if(hasLoaded) {
			m1.tooltipLeftRel = (Input.mousePosition.x * (960f/Screen.width));
			m1.tooltipTopRel = (600) - ((Input.mousePosition.y) * (600f/Screen.height));
			m2.tooltipLeftRel = (Input.mousePosition.x * (960f/Screen.width));
			m2.tooltipTopRel = (600) - ((Input.mousePosition.y) * (600f/Screen.height));
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
		m.tooltipSkin = squareStyle;
		m.SetTooltipLabel(false);
		if(upgradeButton == upgradeButton1) {
			m.tooltip = info1;
		}
		else if(upgradeButton == upgradeButton2) {
			m.tooltip = info2;	
		}
		m.tooltipHeight = 100;
		m.tooltipWidth = 100;
		m.tooltipLeftRel = -105;
		m.tooltipTopRel = 0;
		return upgradeButton;
	}
}
