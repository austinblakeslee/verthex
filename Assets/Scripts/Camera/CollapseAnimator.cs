using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollapseAnimator : MonoBehaviour {

	private static CollapseAnimator instance;
	private SectionController collapsingSection;
	public static bool animate = false;
	private string animationStage;
	private Tower t;
	
	private float timer = 0.0f;
	public float pauseTime = 1.6f;
	public float collapseSpeed = 0.4f;
	public GameObject collapseParticle;
	private GameObject particleInstance;
	
	public AudioClip soundEffect;

	public void BeginAnimation(Tower t) {
		collapsingSection = t.StressCheck();
		if(collapsingSection != null) {
			this.t = t;
			animate = true;
			this.animationStage = "pre";
			this.timer = 0.0f;
		}
	}
	
	public static void Animate(Tower t) {
		instance.BeginAnimation(t);
	}
	
	void Start() {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if(animate) {
			if(animationStage == "pre") {
				GameObject.FindWithTag("MainMenu").GetComponent<Menu>().on = false;
				GameObject.FindWithTag("Help").GetComponent<Menu>().on = false;
				GameObject.FindWithTag("MainCamera").camera.enabled = false;
				Camera c = collapsingSection.transform.Find("CollapseCam").camera;
				c.enabled = true;
				c.rect = new Rect(0, 0, 1, 1);
				animationStage = "pause";
			} else if(animationStage == "pause") {
				if(timer >= pauseTime) {
					timer = 0.0f;
					animationStage = "collapse";
					AudioSource.PlayClipAtPoint(soundEffect, new Vector3(0,0,0));
					GameObject.Instantiate(collapseParticle, collapsingSection.transform.position, collapsingSection.transform.rotation);
				} else {
					timer += Time.deltaTime;
				}
			} else if(animationStage == "collapse") {
				Vector3 scale = collapsingSection.transform.localScale;
				scale.y -= collapseSpeed;
				collapsingSection.transform.localScale = scale;
				if(collapsingSection.transform.localScale.y <= 0.0f) {
					animationStage = "pause2";
				}
			} else if(animationStage == "pause2") {
				if(timer >= pauseTime) {
					timer = 0.0f;
					t.Collapse(collapsingSection.GetHeight()-1);
					collapsingSection = t.StressCheck();
					if(collapsingSection != null) {
						animationStage = "pre";
					} else {
						animationStage = "end";
					}
				} else {
					timer += Time.deltaTime;
				}
			} else if(animationStage == "end") {
				//try{
				GameObject.FindWithTag("MainMenu").GetComponent<Menu>().on = true;
				GameObject.FindWithTag("Help").GetComponent<Menu>().on = true;
				GameObject.FindWithTag("MainCamera").camera.enabled = true;
			//	}
			//	catch(Exception e)
		//		{}
				t = null;
				collapsingSection = null;
				animate = false;
			}
		}
	}
}
