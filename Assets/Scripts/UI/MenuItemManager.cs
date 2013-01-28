using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuItemManager : MonoBehaviour {

	private static List<Rect> registeredRects = new List<Rect>();
	
	public static void RegisterRect(Rect r) {
		registeredRects.Add(r);
	}
	
	public static void UnregisterRect(Rect r) {
		registeredRects.Remove(r);
	}
	
	public static void ClearRects() {
		registeredRects.RemoveAll(All);
	}
	
	public static bool MouseIsInGUI() {
		Vector3 mp = Input.mousePosition;
		Vector2 xy = new Vector2(mp.x, Screen.height - mp.y); //mouse and GUI screen coords are inversed vertically
		foreach (Rect r in registeredRects) {
			if(r.Contains(xy)) {
				return true;
			}
		}
		return false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private static bool All(Rect r) {
		return true;
	}
}
