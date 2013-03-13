using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuItemManager : MonoBehaviour {
	
	private static float scaleX;
	private static float scaleY;
	private static float ow = 960.0f;
	private static float oh = 600.0f;
	
	private static List<Rect> registeredRects = new List<Rect>();
	
	public static void RegisterRect(Rect r) {
		scaleX = ((float)Screen.width)/ow;
		scaleY = ((float)Screen.height)/oh;
		//Debug.Log ("oldRect: " + r.x+","+r.y+","+r.width+","+r.height);
		//Debug.Log ("ScaleX: " + scaleX + ", ScaleY: " + scaleY);
		//Debug.Log ("OW: " + ow + ", OH: " + oh + ", NW: " + Screen.width + ", NH: " + Screen.height);
		Rect r1 = new Rect(r.x*scaleX,r.y*scaleY,r.width*scaleX,r.height*scaleY);
		//Debug.Log ("newRect: " + r1.x+","+r1.y+","+r1.width+","+r1.height);
		registeredRects.Add(r1);
	}
	
	public static void UnregisterRect(Rect r) {
		//Debug.Log (r.x+","+r.y+","+r.width+","+r.height);
		Rect r1 = new Rect(r.x*scaleX,r.y*scaleY,r.width*scaleX,r.height*scaleY);
		registeredRects.Remove(r1);
	}
	
	public static void ClearRects() {
		registeredRects.RemoveAll(All);
	}
	
	public static bool MouseIsInGUI() {
		Vector3 mp = Input.mousePosition;
		//Debug.Log (Input.mousePosition.x + " = " + (Screen.height - Input.mousePosition.y));
		Vector2 xy = new Vector2(mp.x,  Screen.height - mp.y); //mouse and GUI screen coords are inversed vertically: Screen.height -
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
