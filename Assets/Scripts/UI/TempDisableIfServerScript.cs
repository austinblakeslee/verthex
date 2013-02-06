using UnityEngine;
using System.Collections;

public class TempDisableIfServerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (Network.isServer)	
		{
			gameObject.SetActive(false);
		}
		else{			
			Menu menu = GameObject.Find("Main").GetComponent("Menu") as Menu; 
			menu.menuItems[0]= gameObject.GetComponent("MenuItem") as MenuItem;
			Debug.Log (menu.menuItems[0]);

		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
