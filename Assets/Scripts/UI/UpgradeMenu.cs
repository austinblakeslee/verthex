using UnityEngine;
using System.Collections;

public class UpgradeMenu : Menu {
	private bool hasLoaded = false;
	public Vector2 buttonSize;
	public AudioClip click;
	private int numButtons;

	public override void Update() {
		if(!hasLoaded) {
			Faction f = TurnOrder.myPlayer.faction;
			
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
			rect.x = Screen.width - (Screen.width * (150/960));
		}
		else if(i%3 == 0) {
			rect.x = Screen.width - (Screen.width * (225/960));
		}
		else if(i%2 == 0 && i > 0) {
			rect.x = Screen.width - (Screen.width * (75/960));
		}
		
		if(i <= 2) {
			rect.y = Screen.height - (Screen.height*(165/600));
		}
		else if(i > 2 && i <= 5) {
			rect.y = Screen.height - (Screen.height*(110/600));
		}
		else if(i > 5 && i <= 8) {
			rect.y = Screen.height - (Screen.height*(55/600));
		}
		
		return rect;
	}
}
