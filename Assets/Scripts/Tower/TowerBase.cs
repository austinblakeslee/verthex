using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerBase : MonoBehaviour {
	public GameObject towerPoint;
	public List<GameObject> modules;
	
	// Update is called once per frame
	void Update () {
	}
	
	public void SetColor(Color c) {
		foreach(GameObject g in modules) {
			g.renderer.material.color = c;
		}
	}
	
}
