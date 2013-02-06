using UnityEngine;
using System.Collections;

public class TempDisableIfClientScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (Network.isClient)	
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
