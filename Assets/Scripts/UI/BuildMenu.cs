using UnityEngine;
using System.Collections;

public class BuildMenu : Menu {

	private bool hasLoaded = false;
	public Vector2 buttonSize;
	public AudioClip click;
	private int numButtons;

	public override void Update() {
		if(!hasLoaded) {
			Faction f = TurnOrder.myPlayer.faction;
			Rect materialButtonRect = new Rect(100, 100, buttonSize.x, buttonSize.y);
			for(int i=0; i< Faction.NUM_MATERIALS; i++) {
				SectionMaterial material = f.GetSectionMaterial(i);
				materialButtonRect = FindPos (numButtons, materialButtonRect);
				GameObject item = MakeButton(material.mtype, materialButtonRect);
				item.AddComponent("MaterialCostLabelUpdate");
				item.GetComponent<MaterialCostLabelUpdate>().materialName = material.mtype;
				item.transform.parent = transform;
				MenuItem m = item.GetComponent<MenuItem>();
				m.action = item.GetComponent<DefaultMenuAction>();
				m.action.click = click;
				menuItems.Add(m);
				//materialButtonRect.xMin += buttonSize.x + 15;
				numButtons++;
			}
			Rect weaponButtonRect = new Rect(100, 100, buttonSize.x, buttonSize.y);
			for(int i=0; i< Faction.NUM_WEAPONS; i++) {
				SectionWeapon weapon = f.GetSectionWeapon(i);
				weaponButtonRect = FindPos(numButtons, weaponButtonRect);
				GameObject item = MakeButton(weapon.wtype, weaponButtonRect);
				item.AddComponent("WeaponCostLabelUpdate");
				item.GetComponent<WeaponCostLabelUpdate>().weaponName = weapon.wtype;
				item.transform.parent = transform;
				MenuItem m = item.GetComponent<MenuItem>();
				m.action = item.GetComponent<DefaultMenuAction>();
				m.action.click = click;
				menuItems.Add(m);
				//weaponButtonRect.xMin += buttonSize.x + 15;
				//if(i%2 == 0 && i > 0) {
					//weaponButtonRect.yMin += buttonSize.y + 5;
					//weaponButtonRect.xMin -= 3*(buttonSize.x + 15);
				//}
				numButtons++;
			}
			Rect confirmButtonRect = new Rect(100,100,buttonSize.x, buttonSize.y);
			confirmButtonRect = FindPos(numButtons, confirmButtonRect);
			GameObject confirm = MakeButton("Confirm",confirmButtonRect);
			confirm.transform.parent = transform;
			MenuItem m0 = confirm.GetComponent<MenuItem>();
			m0.action = confirm.GetComponent<DefaultMenuAction>();
			//m0.action.click = click;
			menuItems.Add(m0);
			numButtons++;
			Rect backButtonRect = new Rect(200,200,buttonSize.x, buttonSize.y);
			backButtonRect = FindPos(numButtons, backButtonRect);
			GameObject back = MakeButton("Back",backButtonRect);
			back.transform.parent = transform;
			MenuItem m1 = back.GetComponent<MenuItem>();
			m1.action = back.GetComponent<DefaultMenuAction>();
			//m1.action.click = click;
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
		m.width = ""+buttonSize.x;
		m.height = ""+buttonSize.y;
		m.type = MenuItem.MenuItemType.Button;
		m.visible = true;
		m.text = text;
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
}
