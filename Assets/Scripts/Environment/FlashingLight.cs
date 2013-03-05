using UnityEngine;
using System.Collections;

public class FlashingLight : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Light>().intensity = Mathf.Sin(Time.time);
	}
}
