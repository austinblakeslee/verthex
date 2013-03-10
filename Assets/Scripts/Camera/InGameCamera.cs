using UnityEngine;
using System.Collections;

public class InGameCamera : MonoBehaviour {
	GameObject cam1Pos;
	GameObject cam2Pos;
	GameObject cam;
	
	// Use this for initialization
	void Start () {
		cam1Pos = GameObject.Find("player1View");
		cam2Pos = GameObject.Find("player2View");
		cam = GameObject.FindWithTag("MainCamera");
		if(TurnOrder.myPlayer == TurnOrder.player1) {
			cam.transform.position = cam1Pos.transform.position;
			cam.transform.rotation = cam1Pos.transform.rotation;
		}
		else if(TurnOrder.myPlayer == TurnOrder.player2) {
			cam.transform.position = cam2Pos.transform.position;
			cam.transform.rotation = cam2Pos.transform.rotation;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void returnPostion() {
		if(TurnOrder.myPlayer == TurnOrder.player1) {
			cam.transform.position = cam1Pos.transform.position;
			cam.transform.rotation = cam1Pos.transform.rotation;
		}
		else if(TurnOrder.myPlayer == TurnOrder.player2) {
			cam.transform.position = cam2Pos.transform.position;
			cam.transform.rotation = cam2Pos.transform.rotation;
		}
	}
}
