using UnityEngine;
using System.Collections;

public class TopMenu : Menu {
	
	private bool hasLoaded = false;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(!hasLoaded) {	
			Rect playerButtonRect = new Rect(5, 5, 250, 50);
			GameObject player = MakeBox("",playerButtonRect);
			player.transform.parent = transform;
			MenuItem m1 = player.GetComponent<MenuItem>();
			menuItems.Add(m1);
			this.gameObject.GetComponent<TurnOrder>().playerText = m1;
			Rect resourceButtonRect = new Rect(35,40,90,30);
			GameObject resource = MakeBox("",resourceButtonRect);
			resource.transform.parent = transform;
			MenuItem m2 = resource.GetComponent<MenuItem>();
			menuItems.Add(m2);
			this.gameObject.GetComponent<TurnOrder>().resources = m2;
			Rect actionButtonRect = new Rect(145,40,90,30);
			GameObject actions = MakeBox("",actionButtonRect);
			actions.transform.parent = transform;
			MenuItem m3 = actions.GetComponent<MenuItem>();
			menuItems.Add(m3);
			this.gameObject.GetComponent<TurnOrder>().actionsLeft = m3;
			Rect queueButtonRect = new Rect(Screen.width - 230,5,225,75);
			GameObject queue = MakeBox("",queueButtonRect);
			queue.transform.parent = transform;
			MenuItem m4 = queue.GetComponent<MenuItem>();
			menuItems.Add(m4);
			this.gameObject.GetComponent<TurnOrder>().actionQueue = m4;
			Rect helpButtonRect = new Rect(Screen.width/2 - 125,5,250,75);
			GameObject help = MakeBox("",helpButtonRect);
			help.transform.parent = transform;
			MenuItem m5 = help.GetComponent<MenuItem>();
			menuItems.Add(m5);
			this.gameObject.GetComponent<TurnOrder>().helpText = m5;
			hasLoaded = true;
		}
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

}
