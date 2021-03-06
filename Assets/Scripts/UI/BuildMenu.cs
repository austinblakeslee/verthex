using UnityEngine;
using System.Collections;

public class BuildMenu : Menu {

	private bool hasLoaded = false;
	public Vector2 buttonSize;
	public Vector2 boxSize;
	public AudioClip click;
	private MenuItem[] materialButtons;
	public MenuItem[] weaponButtons;
	private int numButtons;
	public GUISkin squareStyle;
	MenuItem m;
	MenuItem m1;
	
	void Start() {
		buttonSize.x = 60;
		buttonSize.y = 50;
		boxSize.x = 150;
		boxSize.y = 50;
		materialButtons = new MenuItem[Faction.NUM_MATERIALS];
		weaponButtons = new MenuItem[Faction.NUM_WEAPONS];
	}
	
	public override void Update() {
		foreach(BuildWeaponMenu b in this.gameObject.GetComponentsInChildren<BuildWeaponMenu>()) {
			b.sm = ValueStore.selectedMaterial;
		}
		if(hasLoaded) {
			/*m1.tooltipLeftRel = (Input.mousePosition.x * (960f/Screen.width));
			m1.tooltipTopRel = (600) - ((Input.mousePosition.y) * (600f/Screen.height));*/
			Tower t = TowerSelection.GetSelectedTower();
			for(int i=0; i<Faction.NUM_MATERIALS; i++) {
				string text = t.faction.materials[i];
				materialButtons[i].text = text + ": $" + SectionComponentFactory.GetMaterial(text).cost;
				materialButtons[i].GetComponent<MaterialCostLabelUpdate>().materialName = text;
				materialButtons[i].tooltip = "Health: " + SectionComponentFactory.GetMaterial(text).GetInitialSP() + "\nWeight: " + SectionComponentFactory.GetMaterial(text).GetWeight();
				materialButtons[i].tooltipLeftRel = (Input.mousePosition.x * (960f/Screen.width));
				materialButtons[i].tooltipTopRel = (600) - ((Input.mousePosition.y) * (600f/Screen.height));
			}
		}
		if(!hasLoaded) {
			this.guiSkin = squareStyle;
			GameObject values = new GameObject("BuildValues");
			values.AddComponent("GameValues");
			values.transform.parent = transform;
			numButtons=0;
			GameObject next = new GameObject("BuildWeaponMenu");
			next.AddComponent("BuildWeaponMenu");
			next.transform.parent = transform;
			BuildWeaponMenu nextBWM = next.GetComponent<BuildWeaponMenu>();
			nextBWM.click = click;
			nextBWM.squareStyle = squareStyle;
			Rect materialButtonRect = new Rect(100, 100, buttonSize.x, buttonSize.y);
			for(int i=0; i< Faction.NUM_MATERIALS; i++) {
				materialButtonRect = new Rect(795, (435) + ( ( i * ((150/(Faction.NUM_MATERIALS))+5))  ), 160, 150/(Faction.NUM_MATERIALS));
				//materialButtonRect = FindPos (numButtons, materialButtonRect);
				GameObject item = MakeButton("", materialButtonRect);
				item.AddComponent("MaterialCostLabelUpdate");
				MaterialCostLabelUpdate itemMCLU = item.GetComponent<MaterialCostLabelUpdate>();
				itemMCLU.materialName = "";
				//item.AddComponent("BuildWeaponMenu");
				item.transform.parent = transform;
				MenuItem m = item.GetComponent<MenuItem>();
				m.action = itemMCLU;
				m.tooltipSkin = squareStyle;
				m.SetTooltipLabel(false);
				m.tooltipHeight = 40;
				m.tooltipWidth = 100;
				m.tooltipLeftRel = -105;
				m.tooltipTopRel = 0;
				//m.guiSkin = squareStyle;
				m.action.click = click;
				itemMCLU.fromMenu = this.gameObject;
				itemMCLU.toMenu = next;
				//item.GetComponent<BuildWeaponMenu>().click = click;
				//item.GetComponent<BuildWeaponMenu>().squareStyle = squareStyle;
				menuItems.Add(m);
				materialButtons[i] = m;
				numButtons++;
			}
			Rect backButtonRect = new Rect(730,435,60,160);
			//backButtonRect = FindPos(numButtons, backButtonRect);
			GameObject back = MakeButton("Back",backButtonRect);
			back.AddComponent("SwitchMenu");
			SwitchMenu backSM = back.GetComponent<SwitchMenu>();
			back.transform.parent = transform;
			m1 = back.GetComponent<MenuItem>();
			m1.action = backSM;
			/*m1.tooltipSkin = squareStyle;
			m1.SetTooltipLabel(false);
			m1.tooltip = "Go back to the previous screen.";
			m1.tooltipHeight = 75;
			m1.tooltipWidth = 100;
			m1.tooltipLeftRel = -105;
			m1.tooltipTopRel = 0;*/
			backSM.fromMenu = this.gameObject;
			backSM.toMenu = this.transform.parent.gameObject;
			m1.action.click = click;
			//m1.guiSkin = squareStyle;
			menuItems.Add(m1);
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
		m.width = ""+boxSize.x;
		m.height = ""+boxSize.y;
		m.type = MenuItem.MenuItemType.Box;
		m.visible = true;
		m.text = "";
		return item;
	}

	/*private Rect FindPos(int i, Rect rect) {
		if(i == 1 || i == 4 || i == 7) {
			rect.x = 810;
		}
		else if(i == 0 || i == 3 || i == 6) {
			rect.x = 735;
		}
		else if(i == 2 || i == 5 || i == 8) {
			rect.x = 885;
		}
		else {
			Debug.Log ("Too many buttons!");	
		}
		
		if(i <= 2) {
			rect.y = 435;
		}
		else if(i > 2 && i <= 5) {
			rect.y = 490;
		}
		else if(i > 5 && i <= 8) {
			rect.y = 545;
		}
		
		return rect;
	}*/
}
