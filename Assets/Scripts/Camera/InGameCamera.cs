using UnityEngine;
using System.Collections;

public class InGameCamera : MonoBehaviour {
	public Transform cam1Pos;
	public Transform cam2Pos;
	public Vector3 focusPos;
	public Quaternion focusRot;
	public Vector3 targetPos;
	public Quaternion targetRot;
	
	Vector3 initialPos;
	Quaternion initialRot;
	
	public bool viewingSection = false;
	
	GameObject cam;
	
	// Use this for initialization
	void Start () {
		cam = GameObject.FindWithTag("MainCamera");
		if(TurnOrder.myPlayer == TurnOrder.player1) {
			focusPos = cam2Pos.position;
			targetPos = transform.position;
			targetRot = transform.rotation;
			ChangeTarget (cam1Pos);
		}
		else if(TurnOrder.myPlayer == TurnOrder.player2) {
			focusPos = cam2Pos.position;
			initialPos = transform.position;
			initialRot = transform.rotation;
			returnPosition ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(viewingSection) {
			transform.position = Vector3.Lerp(transform.position,targetPos,Time.deltaTime);
			transform.rotation = Quaternion.Lerp (transform.rotation, targetRot, Time.deltaTime);
			if(transform.position == targetPos && transform.rotation == targetRot) {
				viewingSection = false;
				if(initialPos == null) {
					initialPos = transform.position;
					initialRot = transform.rotation;
				}
			}
		}
	}
	
	public void returnPosition() {
		viewingSection = true;
		targetPos = initialPos;
		targetRot = initialRot;
	}
	
	public void ChangeTarget(Transform target) {
		//Debug.Log (target.GetComponent<TowerBase>().playerNumber);
		viewingSection = true;
		targetPos -= focusPos - target.position;
		focusPos = target.position;
	}
}
