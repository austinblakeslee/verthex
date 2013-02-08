using UnityEngine;
using System.Collections;

public class BuildMenu : Menu {

	private bool hasLoaded = false;
	public Vector2 buttonSize;
	public AudioClip click;

	public override void Update() {
		if(!hasLoaded) {
			Faction f = TurnOrder.myPlayer.faction;
			Rect materialButtonRect = new Rect(200, Screen.height - 300, buttonSize.x, buttonSize.y);
			for(int i=0; i< Faction.NUM_MATERIALS; i++) {
				SectionMaterial material = f.GetSectionMaterial(i);
				GameObject item = MakeButton(material.mtype, materialButtonRect);
				item.AddComponent("MaterialCostLabelUpdate");
				item.GetComponent<MaterialCostLabelUpdate>().materialName = material.mtype;
				item.transform.parent = transform;
				MenuItem m = item.GetComponent<MenuItem>();
				m.action = item.GetComponent<DefaultMenuAction>();
				m.action.click = click;
				menuItems.Add(m);
				materialButtonRect.xMin += buttonSize.x;
			}
			Rect weaponButtonRect = new Rect(200, Screen.height - 200, buttonSize.x, buttonSize.y);
			for(int i=0; i< Faction.NUM_WEAPONS; i++) {
				SectionWeapon weapon = f.GetSectionWeapon(i);
				GameObject item = MakeButton(weapon.wtype, weaponButtonRect);
				item.AddComponent("WeaponCostLabelUpdate");
				item.GetComponent<WeaponCostLabelUpdate>().weaponName = weapon.wtype;
				item.transform.parent = transform;
				MenuItem m = item.GetComponent<MenuItem>();
				m.action = item.GetComponent<DefaultMenuAction>();
				m.action.click = click;
				menuItems.Add(m);
				weaponButtonRect.xMin += buttonSize.x;
			}
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

	
}
