using UnityEngine;
using System.Collections;

public class PowerBar : MonoBehaviour {

	private bool isDisplayed = false;
	private float fill = 0.0f;
	private bool hasStartedCharge = false;
	private bool hasFinishedCharge  = false;
	public Texture2D emptyTexture;
	public Texture2D fillTexture;
	
	void Update () {
		if(Input.GetKey(KeyCode.Space) && isDisplayed && !hasFinishedCharge)  {
			hasStartedCharge = true;
			fill += GameValues.floatValues["powerBarSpeed"];
			if(fill > 1.0f) {
				fill = 0.0f;
			}
		} 
		else if(Input.GetKeyUp(KeyCode.Space) && hasStartedCharge) {
			hasFinishedCharge = true;
			isDisplayed = false;
		}
	}
	
	void OnGUI() {
	    if(isDisplayed) {
	        int width = emptyTexture.width;
	        int height = emptyTexture.height;
			int top = Screen.height/2 - (height/2);
			int left = Screen.width * 5 / 16;
	        Rect emptyRect = new Rect(left, top, width, height);
	        Rect groupRect = new Rect(left, top, width, height - height * fill);
	        Rect barRect = new Rect(0, 0, width, height);
	        GUI.DrawTexture(emptyRect, fillTexture);
	        GUI.BeginGroup(groupRect);
	        GUI.DrawTexture(barRect, emptyTexture);
	        GUI.EndGroup();
	    }
	}
	
	public void Hide() {
		this.isDisplayed = false;
		this.fill = 0.0f;
	}
	
	public void Reset() {
		this.fill = 0.0f;
		this.isDisplayed = false;
		this.hasFinishedCharge = false;
		this.hasStartedCharge = false;
	}
	
	public void Show() {
		this.isDisplayed = true;
	}
	
	public float GetFillAmount() {
		return this.fill;
	}
	
	public bool Complete() {
		return this.hasFinishedCharge;
	}
}
