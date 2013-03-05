using UnityEngine;
using System.Collections;

public class Levitation : MonoBehaviour {

	float oscSpeed;
	float amplitude;

	// Use this for initialization
	void Start () {
		oscSpeed = Random.Range(1.0f, 5.0f);
		amplitude = Random.Range(1.0f, 3.0f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0, Mathf.Sin(oscSpeed * Time.time) * Time.deltaTime * amplitude, 0);
	}
}
