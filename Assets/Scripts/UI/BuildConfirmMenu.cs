using UnityEngine;
using System.Collections;

public class BuildConfirmMenu : Menu {

	private bool hasLoaded = false;
	public Vector2 buttonSize;
	public Vector2 boxSize;
	public AudioClip click;
	private MenuItem[] materialButtons;
	public MenuItem[] weaponButtons;
	private int numButtons;
	public SectionMaterial sm;
	public SectionWeapon sw;
	void Start() {
		buttonSize.x = 60;
		buttonSize.y = 50;
		boxSize.x = 160;
		boxSize.y = 30;
		materialButtons = new MenuItem[Faction.NUM_MATERIALS];
		weaponButtons = new MenuItem[Faction.NUM_WEAPONS];
	}
	
	public override void Update() {
		ValueStore.selectedMaterial = sm;
		ValueStore.selectedWeapon = sw;
		if(hasLoaded) {
			Tower t = TurnOrder.myPlayer.GetTower(TurnOrder.actionNum);
		}
		if(!hasLoaded) {
			GameObject values = new GameObject("BuildValues");
			values.AddComponent("GameValues");
			values.transform.parent = transform;
			numButtons=0;
			/*Rect materialButtonRect = new Rect(100, 100, buttonSize.x, buttonSize.y);
			for(int i=0; i< Faction.NUM_MATERIALS; i++) {
				materialButtonRect = new Rect(Screen.width - 165, (Screen.height - 165) + ( ( i * ((150/(Faction.NUM_MATERIALS))+5))  ), 160, 150/(Faction.NUM_MATERIALS));
				//materialButtonRect = FindPos (numButtons, materialButtonRect);
				GameObject item = MakeButton("", materialButtonRect);
				item.AddComponent("MaterialCostLabelUpdate");
				item.GetComponent<MaterialCostLabelUpdate>().materialName = "";
				item.AddComponent("SwitchMenu");
				item.GetComponent("BuildWeaponMenu");
				item.transform.parent = transform;
				MenuItem m = item.GetComponent<MenuItem>();
				m.action = item.GetComponent<SwitchMenu>();//item.GetComponent<DefaultMenuAction>();
				build.GetComponent<SwitchMenu>().fromMenu = this.gameObject;
				build.GetComponent<SwitchMenu>().toMenu = item;
				m.action.click = click;
				menuItems.Add(m);
				materialButtons[i] = m;
				//materialButtonRect.xMin += buttonSize.x + 15;
				numButtons++;
			}*/
			/*Rect weaponButtonRect = new Rect(100, 100, buttonSize.x, buttonSize.y);
			for(int i=0; i< Faction.NUM_WEAPONS; i++) {
				weaponButtonRect = new Rect(Screen.width - 165, (Screen.height - 165) + ( ( i * ((150/(Faction.NUM_WEAPONS))+5))  ), 160, 150/(Faction.NUM_WEAPONS));
				//weaponButtonRect = FindPos(numButtons, weaponButtonRect);
				GameObject item = MakeButton("", weaponButtonRect);
				item.AddComponent("WeaponCostLabelUpdate");
				item.GetComponent<WeaponCostLabelUpdate>().weaponName = "";
				item.AddComponent("SwitchMenu");
				item.GetComponent("BuildConfirmMenu");
				item.transform.parent = transform;
				MenuItem m = item.GetComponent<MenuItem>();
				m.action = item.GetComponent<SwitchMenu>();//item.GetComponent<DefaultMenuAction>();
				item.GetComponent<SwitchMenu>().fromMenu = this.gameObject;
				item.GetComponent<SwitchMenu>().toMenu = item;
				m.action.click = click;
				menuItems.Add(m);
				weaponButtons[i] = m;
				//weaponButtonRect.xMin += buttonSize.x + 15;
				//if(i%2 == 0 && i > 0) {
					//weaponButtonRect.yMin += buttonSize.y + 5;
					//weaponButtonRect.xMin -= 3*(buttonSize.x + 15);
				//}
				numButtons++;
			}*/
			Rect confirmButtonRect = new Rect(Screen.width - 165, (Screen.height - 39), 160, 150/(4));
			//confirmButtonRect = FindPos(numButtons, confirmButtonRect);
			GameObject confirm = MakeButton("Confirm",confirmButtonRect);
			confirm.AddComponent("BuildAction");
			confirm.transform.parent = transform;
			MenuItem m0 = confirm.GetComponent<MenuItem>();
			m0.action = confirm.GetComponent<BuildAction>();
			m0.action.click = click;
			confirm.GetComponent<BuildAction>().myMenu = this;
			menuItems.Add(m0);
			numButtons++;
			Rect backButtonRect = new Rect(Screen.width - 230,Screen.height - 165,60,160);
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
			Rect materialLabelRect = new Rect(Screen.width - 165, (Screen.height - 165), 160, 37);
			GameObject mlabel = MakeBox ("MaterialsCost",materialLabelRect);
			mlabel.AddComponent("MaterialCostLabel");
			mlabel.transform.parent = transform;
			MenuItem m2 = mlabel.GetComponent<MenuItem>();
			menuItems.Add(m2);
			Rect weaponsLabelRect = new Rect(Screen.width - 165, (Screen.height - 123), 160, 37);
			GameObject wlabel = MakeBox ("WeaponsCost",weaponsLabelRect);
			wlabel.AddComponent("WeaponCostLabel");
			wlabel.transform.parent = transform;
			MenuItem m3 = wlabel.GetComponent<MenuItem>();
			menuItems.Add(m3);
			Rect sumLabelRect = new Rect(Screen.width - 165, (Screen.height - 81), 160, 37);
			GameObject slabel = MakeBox ("SumCost",sumLabelRect);
			slabel.AddComponent("SumCostLabel");
			slabel.transform.parent = transform;
			MenuItem m4 = slabel.GetComponent<MenuItem>();
			menuItems.Add(m4);
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
		if(i == 1 || i == 4 || i == 7) {
			rect.x = Screen.width - 150;
		}
		else if(i == 0 || i == 3 || i == 6) {
			rect.x = Screen.width - 225;
		}
		else if(i == 2 || i == 5 || i == 8) {
			rect.x = Screen.width - 75;
		}
		else {
			Debug.Log ("Too many buttons!");	
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
}
