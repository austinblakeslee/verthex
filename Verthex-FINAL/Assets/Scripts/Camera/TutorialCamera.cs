using UnityEngine;
using System.Collections;

public class TutorialCamera : MonoBehaviour {
	private int slideNum;
	public MenuItem prev;
	public MenuItem next;
	
	// Use this for initialization
	void Start () {
		slideNum = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(slideNum > 0 && Input.GetKeyDown(KeyCode.LeftArrow)) {
			this.transform.Translate(-1600,0,0);
			slideNum--;
		}
		if(slideNum < 17 && Input.GetKeyDown(KeyCode.RightArrow)) {
			this.transform.Translate(1600,0,0);
			slideNum++;
		}
		if(slideNum != 0){
			prev.visible = true;
		}
		else {
			prev.visible = false;
		}
		if(slideNum != 17) {
			next.visible = true;
		}
		else {
			next.visible = false;
		}
		Debug.Log (slideNum);
	}
	
	void OnGUI() {
		//if (GUI.Button(new Rect((Screen.width/2)-250, Screen.height-68, 200, 50), menuStyle, btnStyle))
          //  Application.LoadLevel(0);
		//if (GUI.Button(new Rect((Screen.width/2)+50, Screen.height-68, 200, 50), playStyle, btnStyle))
            //Application.LoadLevel(1);
		
		
	}
	
	public void slideNumUp() {
		slideNum++;
	}
	
	public void slideNumDown() {
		slideNum--;
	}
}
