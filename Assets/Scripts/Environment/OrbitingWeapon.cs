using UnityEngine;
using System.Collections;

public class OrbitingWeapon : MonoBehaviour {

	public Transform orbitee;
	public float orbitSpeed = 20.0f;
	public float oscillateSpeed = 5.0f;
	
	public bool orbiting = true;
	
	private Quaternion originalRotation;
	private Vector3 originalPosition;
	
	void Start() {
		originalPosition = transform.position;
		originalRotation = transform.rotation;
		orbitee = transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
		if(orbiting) {
			transform.RotateAround(orbitee.position, orbitee.up, orbitSpeed * Time.deltaTime);
			transform.Translate(0, oscillateSpeed * Time.deltaTime * Mathf.Sin(3.0f + Time.time), 0);
		} else {
			transform.position = originalPosition;
			transform.rotation = originalRotation;
		}
	}
}
