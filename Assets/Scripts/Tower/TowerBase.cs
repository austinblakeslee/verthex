using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerBase : MonoBehaviour {
	public GameObject towerPoint;
	public List<GameObject> modules;
	public int playerNumber;
	public int towerNumber;
	
	// Update is called once per frame
	void Update () {
	}
	
	public void SetColor(Color c)
	{
		foreach(GameObject g in modules)
		{
			g.renderer.material.color = c;
		}
	}
	
	public void SetPlayerNumber(int n)
	{
		playerNumber = n;
	}
	
	public void SetTowerNumber(int n)
	{
		towerNumber = n;
	}
	
	public int GetPlayerNumber(){
		return this.playerNumber;
	}
	
	public int GetTowerNumber(){
		return this.towerNumber;
	}
	
	public void Highlight() {
		foreach(Renderer r in GetComponentsInChildren<Renderer>()) {
			if(r.material.shader.name == "Diffuse") {
				r.material.shader = Shader.Find("Custom/highlightShader");
			}
		}
	}
	
	public void Unhighlight() {
		foreach(Renderer r in GetComponentsInChildren<Renderer>()) {
			if(r.material.shader.name == "Custom/highlightShader") {
				r.material.shader = Shader.Find("Diffuse");
			}
		}
	}
} 