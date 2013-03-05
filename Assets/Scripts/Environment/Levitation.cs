using UnityEngine;
using System.Collections;

public class Levitation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0, Mathf.Sin(Time.time) * Time.deltaTime, 0);
	}
}
