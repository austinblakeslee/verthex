using UnityEngine;
using System.Collections;

public class BuildWeaponMenu : Menu {

	private bool hasLoaded = false;
	public Vector2 buttonSize;
	public Vector2 boxSize;
	public AudioClip click;
	public MenuItem[] weaponButtons;
	private int numButtons;
	public SectionMaterial sm;
	public GUISkin squareStyle;
	MenuItem m;
	MenuItem m1;
	
	void Start() {
		buttonSize.x = 60;
		buttonSize.y = 50;
		boxSize.x = 150;
		boxSize.y = 50;
		weaponButtons = new MenuItem[Faction.NUM_WEAPONS];
	}
	
	public override void Update() {
		ValueStore.selectedMaterial = sm;
		foreach(BuildConfirmMenu b in this.gameObject.GetComponentsInChildren<BuildConfirmMenu>()) {
			b.sm = ValueStore.selectedMaterial;
			b.sw = ValueStore.selectedWeapon;
		}
		if(hasLoaded) {
			/*m1.tooltipLeftRel = (Input.mousePosition.x * (960f/Screen.width));
			m1.tooltipTopRel = (600) - ((Input.mousePosition.y) * (600f/Screen.height));*/
			Tower t = TowerSelection.GetSelectedTower();
			for(int i=0; i<Faction.NUM_WEAPONS; i++) {
				string text = t.faction.weapons[i];
				weaponButtons[i].text = text + ": $" + SectionComponentFactory.GetWeapon(text).cost;
				weaponButtons[i].GetComponent<WeaponCostLabelUpdate>().weaponName = text;
				weaponButtons[i].tooltip = "Damage: " + SectionComponentFactory.GetWeapon(text).GetDamage() + "\nRange: " + SectionComponentFactory.GetWeapon(text).GetRange() + "\nWeight: " + SectionComponentFactory.GetWeapon(text).GetWeight();
				weaponButtons[i].tooltipLeftRel = (Input.mousePosition.x * (960f/Screen.width));
				weaponButtons[i].tooltipTopRel = (600) - ((Input.mousePosition.y) * (600f/Screen.height));
			}
		}
		if(!hasLoaded) {
			this.guiSkin = squareStyle;
			GameObject values = new GameObject("BuildValues");
			values.AddComponent("GameValues");
			values.transform.parent = transform;
			numButtons=0;
			GameObject next = new GameObject("BuildConfirmMenu");
			next.AddComponent("BuildConfirmMenu");
			next.transform.parent = transform;
			BuildConfirmMenu nextBCM = next.GetComponent<BuildConfirmMenu>();
			nextBCM.click = click;
			nextBCM.squareStyle = squareStyle;
			Rect weaponButtonRect = new Rect(100, 100, buttonSize.x, buttonSize.y);
			for(int i=0; i< Faction.NUM_WEAPONS; i++) {
				weaponButtonRect = new Rect(795, (435) + ( ( i * ((150/(Faction.NUM_WEAPONS))+5))  ), 160, 150/(Faction.NUM_WEAPONS));
				//weaponButtonRect = FindPos(numButtons, weaponButtonRect);
				GameObject item = MakeButton("", weaponButtonRect);
				item.AddComponent("WeaponCostLabelUpdate");
				WeaponCostLabelUpdate itemWCLU = item.GetComponent<WeaponCostLabelUpdate>();
				itemWCLU.weaponName = "";
				item.AddComponent("BuildConfirmMenu");
				item.transform.parent = transform;
				m = item.GetComponent<MenuItem>();
				m.action = itemWCLU;
				m.tooltipSkin = squareStyle;
				m.SetTooltipLabel(false);
				m.tooltipHeight = 60;
				m.tooltipWidth = 100;
				m.tooltipLeftRel = -105;
				m.tooltipTopRel = 0;
				//m.guiSkin = squareStyle;
				itemWCLU.fromMenu = this.gameObject;
				itemWCLU.toMenu = next;
				//item.GetComponent<BuildConfirmMenu>().click = click;
				//item.GetComponent<BuildConfirmMenu>().squareStyle = squareStyle;
				menuItems.Add(m);
				weaponButtons[i] = m;
				//weaponButtonRect.xMin += buttonSize.x + 15;
				//if(i%2 == 0 && i > 0) {
					//weaponButtonRect.yMin += buttonSize.y + 5;
					//weaponButtonRect.xMin -= 3*(buttonSize.x + 15);
				//}
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
