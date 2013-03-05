using UnityEngine;
using System.Collections;

public class Levitation : MonoBehaviour {

	public float oscSpeed = 5.0f;
	public float amplitude = 3.0f;

	// Use this for initialization
	void Start () {
		oscSpeed = Random.Range(1.0f, oscSpeed);
		amplitude = Random.Range(1.0f, amplitude);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0, Mathf.Sin(oscSpeed * Time.time) * Time.deltaTime * amplitude, 0);
	}
}
