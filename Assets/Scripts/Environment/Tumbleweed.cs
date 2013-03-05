using UnityEngine;
using System.Collections;

public class Tumbleweed : MonoBehaviour {

	public float duration = 6.0f;
	public float speed = 15.0f;
	
	// Update is called once per frame
	void Update () {
		if(duration > 0.0f) {
			transform.Translate(0, Mathf.Sin(6.0f * duration) * Time.deltaTime * 4.0f, speed * Time.deltaTime);
			duration -= Time.deltaTime;
		} else {
			Destroy(gameObject);
		}
	}
}
