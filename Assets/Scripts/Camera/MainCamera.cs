using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {
	public Transform target;
	public Vector3 relativePosition;
	public float rotationSpeed;
	public float zoomSpeed;
	public int zoomLevel;
	public int maxZoomLevel;
	public MenuItem quit;
	public Tower currentTower;
	
	// Use this for initialization
	void Start () {
		transform.localPosition = relativePosition;
		ChangeTarget(target);
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(target);
		Vector3 translation = Vector3.right * Time.deltaTime * rotationSpeed;
		float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
		if(scrollWheel > 0 && zoomLevel < maxZoomLevel) {
			zoomLevel++;
			translation.z = zoomSpeed;
		} else if(scrollWheel < 0 && zoomLevel > -maxZoomLevel) {
			zoomLevel--;
			translation.z = -zoomSpeed;
		}
		transform.Translate(translation);
		if(Application.loadedLevel == 0) {
			if( Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.WindowsPlayer ) {
				quit.visible = true;
			}
			else {
				quit.visible = false;
			}
		}
	}
	
	public void ChangeTarget(Transform newTarget) {
		Quaternion rotation = transform.rotation;
		Vector3 localPos = transform.localPosition;
		target = newTarget;
		transform.parent = newTarget;
		transform.localPosition = localPos;
		transform.localScale = Vector3.one;
		transform.rotation = rotation;
	}
	
	public void SetCurrentTower(Tower t){
		currentTower = t;
	}
	
	public Tower GetCurrentTower(){
		return currentTower;
	}
}
