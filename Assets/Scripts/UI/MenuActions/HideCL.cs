using UnityEngine;
using System.Collections;

public class HideCL : MonoBehaviour {
	private static GameObject cl;
	
	void Start () {
		cl = this.gameObject;
		cl.GetComponent<MenuItem>().visible = false;
	}
	
	public static void setCombatLog() {
		cl.GetComponent<MenuItem>().visible = !cl.GetComponent<MenuItem>().visible;
	}
}
