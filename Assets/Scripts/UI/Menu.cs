using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Menu : MonoBehaviour {
	public List<MenuItem> menuItems = new List<MenuItem>();
	public Texture2D background;
	public bool on;
	public bool disabled;
	public int yOffset, xOffset = 0;
	public GUISkin guiSkin = null;
	
	private bool hotKey = false;
	private int hotMenuItem;
	
	
	void Update() {
		//Checks if any MenuItem's hotKey has been pressed
		int i=0;
		foreach (MenuItem m in menuItems) {
			if (Input.GetKeyDown(m.hotKey)){
				hotKey = true;
				hotMenuItem = i;
			}
			i++;
		}
	}
	// draw menu components
	void OnGUI() {
		foreach (MenuItem m in menuItems) {
			GUI.skin = m.guiSkin ? m.guiSkin : guiSkin;
		
			m.SetVisible(on);
			if(on && m.visible) {
				DrawBackground();
				DrawMenuItem(m);
				m.setTooltipOn(true);
				
				// draw tooltip box for box style tooltips
				/*if (new Rect(m.getLeftI()+xOffset, m.getTopI()+yOffset, m.getWidthI(), m.getHeightI()).Contains(Input.mousePosition) && m.ttType == MenuItem.TooltipType.Box){
					GUI.skin = m.tooltipSkin;
					GUI.Box(new Rect (m.getLeftI()+m.tooltipLeftRel, m.getTopI()+m.tooltipTopRel, m.tooltipWidth, m.tooltipHeight), GUI.tooltip);
				}*/
			}
			else{
				m.setTooltipOn(false);
			}
		}
	}
	
	protected void DrawBackground() {
		if(background) {
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);
		}
	}
	
	protected void DrawMenuItem(MenuItem m) {
		switch(m.type){
			case MenuItem.MenuItemType.Button:
				DrawButton(m);
				break;
			case MenuItem.MenuItemType.Box:
				DrawBox(m);
				break;
			case MenuItem.MenuItemType.Label:
				DrawLabel(m);
				break;
			case MenuItem.MenuItemType.HorizontalSlider:
				DrawHorizontalSlider(m);
				break;
			case MenuItem.MenuItemType.VerticalSlider:
				DrawVerticalSlider(m);
				break;
			case MenuItem.MenuItemType.VerticalScrollBox:
				DrawVerticalScrollBox(m);
				break;
			default:
				return;
		}
	}
	
	[RPC]
	protected void PerformAction(int index) {
		menuItems[index].action.Action();
	}
	
	protected void DrawButton(MenuItem m) {
		//Set hotKey to true (equivalent to pressing MenuItems's hotKey
		if(GUI.Button(new Rect(m.getLeftI()+xOffset, m.getTopI()+yOffset, m.getWidthI(), m.getHeightI()), m.text)){
			hotKey = true;
			hotMenuItem = menuItems.IndexOf(m);
		}
		//If the hotKey or GUI Button was pressed....
		if (hotKey){
			hotKey = false;
			ValueStore.buttonWasClicked = true;
			if (menuItems[hotMenuItem].action && !disabled){
				if(Network.isServer || Network.isClient) {
					networkView.RPC("PerformAction", RPCMode.All, hotMenuItem);
				} else {
					PerformAction(hotMenuItem);
				}
			} else if(disabled) {
				ValueStore.helpMessage = "You must be viewing yourself\nto use the menu!";
			}
		}
	}
	
	protected void DrawBox(MenuItem m) {
		GUI.Box(new Rect(m.getLeftI()+xOffset, m.getTopI()+yOffset, m.getWidthI(), m.getHeightI()), m.text);
	}
	
	protected void DrawLabel(MenuItem m) {
		GUI.Label(new Rect(m.getLeftI()+xOffset, m.getTopI()+yOffset, m.getWidthI(), m.getHeightI()), m.text);
	}
	
	protected void DrawHorizontalSlider(MenuItem m) {
		if (GameValues.intValues.ContainsKey(m.sliderGameValue)){
			GameValues.intValues[m.sliderGameValue] = (int)GUI.HorizontalSlider(new Rect(m.getLeftI()+xOffset, m.getTopI()+yOffset, m.getWidthI(), m.getHeightI()),GameValues.intValues[m.sliderGameValue],m.sliderMin,m.sliderMax);
			if (GameObject.FindWithTag(m.sliderGameValue)){
				GameObject.FindWithTag(m.sliderGameValue).GetComponent<MenuItem>().text = "" + GameValues.intValues[m.sliderGameValue];
			}
		}
		else if (GameValues.floatValues.ContainsKey(m.sliderGameValue)){
			GameValues.floatValues[m.sliderGameValue] = (float)GUI.HorizontalSlider(new Rect(m.getLeftI()+xOffset, m.getTopI()+yOffset, m.getWidthI(), m.getHeightI()),GameValues.floatValues[m.sliderGameValue],m.sliderMin,m.sliderMax);
			if (GameObject.FindWithTag(m.sliderGameValue)){
				GameObject.FindWithTag(m.sliderGameValue).GetComponent<MenuItem>().text = "" + GameValues.floatValues[m.sliderGameValue];
			}
		}
	}
		
	protected void DrawVerticalSlider(MenuItem m) {
		if (GameValues.intValues.ContainsKey(m.sliderGameValue)){
			GameValues.intValues[m.sliderGameValue] = (int)GUI.VerticalSlider(new Rect(m.getLeftI()+xOffset, m.getTopI()+yOffset, m.getWidthI(), m.getHeightI()),GameValues.intValues[m.sliderGameValue],m.sliderMin,m.sliderMax);
			if (GameObject.FindWithTag(m.sliderGameValue)){
				GameObject.FindWithTag(m.sliderGameValue).GetComponent<MenuItem>().text = "" + GameValues.intValues[m.sliderGameValue];
			}
		}
		else if (GameValues.floatValues.ContainsKey(m.sliderGameValue)){
			GameValues.floatValues[m.sliderGameValue] = (float)GUI.VerticalSlider(new Rect(m.getLeftI()+xOffset, m.getTopI()+yOffset, m.getWidthI(), m.getHeightI()),GameValues.floatValues[m.sliderGameValue],m.sliderMin,m.sliderMax);
			if (GameObject.FindWithTag(m.sliderGameValue)){
				GameObject.FindWithTag(m.sliderGameValue).GetComponent<MenuItem>().text = "" + GameValues.floatValues[m.sliderGameValue];
			}
		}
	}
	
	protected void DrawVerticalScrollBox(MenuItem m) {
		m.scrollPosition = GUI.BeginScrollView(new Rect(m.getLeftI()+xOffset, m.getTopI()+yOffset, m.getWidthI()+18, m.getHeightI()), m.scrollPosition, new Rect(0, 0, m.getWidthI(), m.scrollBoxHeight));
		
		GUI.Box(new Rect(0, 0, m.getWidthI(), m.scrollBoxHeight), m.text);
		
		GUI.EndScrollView();
	}
}
