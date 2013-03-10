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
			Tower t = TurnOrder.myPlayer.GetTower(TurnOrder.actionNum);
			for(int i=0; i<Faction.NUM_MATERIALS; i++) {
				string text = t.faction.materials[i];
				materialButtons[i].text = text + ": $" + SectionComponentFactory.GetMaterial(text).cost;
				materialButtons[i].GetComponent<MaterialCostLabelUpdate>().materialName = text;
			}
		}
		if(!hasLoaded) {
			GameObject values = new GameObject("BuildValues");
			values.AddComponent("GameValues");
			values.transform.parent = transform;
			numButtons=0;
			
			Rect materialButtonRect = new Rect(100, 100, buttonSize.x, buttonSize.y);
			for(int i=0; i< Faction.NUM_MATERIALS; i++) {
				materialButtonRect = new Rect(795, (435) + ( ( i * ((150/(Faction.NUM_MATERIALS))+5))  ), 160, 150/(Faction.NUM_MATERIALS));
				//materialButtonRect = FindPos (numButtons, materialButtonRect);
				GameObject item = MakeButton("", materialButtonRect);
				item.AddComponent("MaterialCostLabelUpdate");
				item.GetComponent<MaterialCostLabelUpdate>().materialName = "";
				item.AddComponent("BuildWeaponMenu");
				item.transform.parent = transform;
				MenuItem m = item.GetComponent<MenuItem>();
				m.action = item.GetComponent<MaterialCostLabelUpdate>();
				m.action.click = click;
				item.GetComponent<MaterialCostLabelUpdate>().fromMenu = this.gameObject;
				item.GetComponent<BuildWeaponMenu>().click = click;
				menuItems.Add(m);
				materialButtons[i] = m;
				numButtons++;
			}
			Rect backButtonRect = new Rect(730,435,60,160);
			//backButtonRect = FindPos(numButtons, backButtonRect);
			GameObject back = MakeButton("Back",backButtonRect);
			back.AddComponent("SwitchMenu");
			back.transform.parent = transform;
			MenuItem m1 = back.GetComponent<MenuItem>();
			m1.action = back.GetComponent<SwitchMenu>();
			back.GetComponent<SwitchMenu>().fromMenu = this.gameObject;
			back.GetComponent<SwitchMenu>().toMenu = this.transform.parent.gameObject;
			m1.action.click = click;
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

	private Rect FindPos(int i, Rect rect) {
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
	}
}
