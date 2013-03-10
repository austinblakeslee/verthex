using UnityEngine;
using System.Collections;

public class InGameCamera : MonoBehaviour {
	public Transform cam1Pos;
	public Transform cam2Pos;
	public Transform cam1FocusPos;
	public Transform cam2FocusPos;
	private Transform myFocus;
	public Vector3 focusPos;
	public Quaternion focusRot;
	public Vector3 targetPos;
	public Quaternion targetRot;
	
	Vector3 initialPos = Vector3.zero;
	Quaternion initialRot = Quaternion.identity;
	
	public bool changingView = false;
	public float speed = 1.0f;
	
	private bool firstTransition = true;
	
	// Use this for initialization
	void Start () {
		if(TurnOrder.myPlayer == TurnOrder.player1) {
			ChangePosition(cam1Pos, 3.0f);
			initialPos = cam1Pos.position;
			initialRot = cam1Pos.rotation;
			focusPos = cam1FocusPos.position;
			myFocus = cam1FocusPos;
		}
		else if(TurnOrder.myPlayer == TurnOrder.player2) {
			ChangePosition(cam2Pos, 3.0f);
			initialPos = cam2Pos.position;
			initialRot = cam2Pos.rotation;
			focusPos = cam2FocusPos.position;
			myFocus = cam2FocusPos;
		}
		TowerSelection.disableCameraPan = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(changingView) {
			transform.position = Vector3.Lerp(transform.position,targetPos,Time.deltaTime * speed);
			transform.rotation = Quaternion.Lerp (transform.rotation, targetRot, Time.deltaTime * speed);
			if((transform.position - targetPos).magnitude <= 3.0f && Quaternion.Angle(transform.rotation, targetRot) <= 3.0f) {
				changingView = false;
				if(firstTransition) {
					firstTransition = false;
					TowerSelection.disableCameraPan = false;
					ChangeTarget(TurnOrder.myPlayer.GetTower(TurnOrder.actionNum).towerBase.transform);
				}
			}
		}
	}
	
	//instantly return to initial position
	public void instantReturnPosition() {
		changingView = false;
		transform.position = initialPos;
		transform.rotation = initialRot;
		targetPos = initialPos;
		targetRot = initialRot;
		focusPos = myFocus.position;
	}
	
	
	public void returnPosition() {
		returnPosition(1.0f);
	}
	
	//slowly return to initial position
	public void returnPosition(float speed) {
		this.speed = speed;
		changingView = true;
		targetPos = initialPos;
		targetRot = initialRot;
		focusPos = myFocus.position;
	}
	
	
	public void ChangeTarget(Transform target) {
		ChangeTarget(target, 1.0f);
	}
	
	//change what we're looking at
	public void ChangeTarget(Transform target, float speed) {
		this.speed = speed;
		changingView = true;
		targetPos -= focusPos - target.position;
		focusPos = target.position;
	}
	
	
	public void ChangePosition(Transform target) {
		ChangePosition(target, 1.0f);
	}
	
	//change position directly
	public void ChangePosition(Transform target, float speed) {
		this.speed = speed;
		changingView = true;
		targetPos = target.position;
		targetRot = target.rotation;
	}
}
