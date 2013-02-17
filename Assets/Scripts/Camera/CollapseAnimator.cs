using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollapseAnimator : MonoBehaviour {

	public static bool animate = false;
	private static CollapseAnimator instance;
	
	public float pauseTime = 1.6f;
	public float collapseSpeed = 0.4f;
	public GameObject collapseParticle;
	public AudioClip soundEffect;
	
	private Section collapsingSection;
	private Tower t;
	private float timer = 0.0f;
	private GameObject particleInstance;
	private bool collapseSection = false;	

	private void BeginAnimation(Tower t) {
		collapsingSection = t.StressCheck();
		if(collapsingSection != null) {
			this.t = t;
			animate = true;
			StartCoroutine(CollapseAnimation());
		}
	}
	
	private IEnumerator CollapseAnimation() {
		while(collapsingSection != null) {
			Pre();
			yield return new WaitForSeconds(0.01f); //I hate coroutines.
			Pause();
			Collapse();
			Pause();
			t.Collapse(collapsingSection.attributes.height);
			collapsingSection = t.StressCheck();
		}
		GameObject.FindWithTag("MainMenu").GetComponent<Menu>().on = true;
		GameObject.FindWithTag("Help").GetComponent<Menu>().on = true;
		GameObject.FindWithTag("MainCamera").camera.enabled = true;
		animate = false;
	}
	
	private void Pre() {
		GameObject.FindWithTag("MainMenu").GetComponent<Menu>().on = false;
		GameObject.FindWithTag("Help").GetComponent<Menu>().on = false;
		GameObject.FindWithTag("MainCamera").camera.enabled = false;
		Camera c = collapsingSection.transform.Find("CollapseCam").camera;
		c.enabled = true;
	}
	
	private IEnumerator Pause() {
		yield return new WaitForSeconds(pauseTime);
	}
	
	private IEnumerator Collapse() {
		AudioSource.PlayClipAtPoint(soundEffect, new Vector3(0,0,0));
		GameObject.Instantiate(collapseParticle, collapsingSection.transform.position, collapsingSection.transform.rotation);
		collapseSection = true;
		do {
			yield return new WaitForSeconds(0.3f);
		} while(collapseSection);
	}
	
	public static void Animate(Tower t) {
		instance.BeginAnimation(t);
	}
	
	void Start() {
		instance = this;
	}
	
	void Update () {
		if(collapseSection) {
			Vector3 scale = collapsingSection.transform.localScale;
			scale.y -= collapseSpeed;
			collapsingSection.transform.localScale = scale;
			if(collapsingSection.transform.localScale.y <= 0.0f) {
				collapseSection = false;
			}
		}
	}
}
