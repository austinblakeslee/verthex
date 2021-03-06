using UnityEngine;
using System.Collections;

public class PowerBar : MonoBehaviour {

	private bool isDisplayed = false;
	private float fill = 0.5f;
	private bool hasStartedCharge = false;
	private bool hasFinishedCharge  = false;
	private float range = 1;
	private float fillRate;
	private int targetSectionNumber; //0 is straight across, -1 is 1 down, 2 is 2 above.
	public Texture2D emptyTexture;
	public Texture2D fillTexture;
	
	[RPC]
	private void DecreaseFill() {
		if(Mathf.Abs(targetSectionNumber - 1) <= range)
		{
			fill -= fillRate;
			targetSectionNumber --;
		}
	}
	
	[RPC]
	private void IncreaseFill() {
		if(Mathf.Abs(targetSectionNumber + 1) <= range){
			fill += fillRate;
			targetSectionNumber ++;
		}
	}
	
	[RPC]
	private void SpaceBarPressed() {
		if(TowerSelection.GetSelectedTower().towerBase.playerNumber != TurnOrder.myPlayer.playerNumber) {
			hasFinishedCharge = true;
			isDisplayed = false;
		} else {
			ValueStore.helpMessage = "You must select an opponent's tower to fire at.";
		}
	}
	
	void Update () {
		fillRate = .5f/range;
		if (!hasStartedCharge && isDisplayed)
		{
			targetSectionNumber = 0;
			hasStartedCharge = true;
		}
		else if(Input.GetKeyDown(KeyCode.UpArrow) && isDisplayed && !hasFinishedCharge)  {
			//fill += GameValues.floatValues["powerBarSpeed"];
			IncreaseFill();
		} 
		else if(Input.GetKeyDown(KeyCode.DownArrow) && isDisplayed && !hasFinishedCharge)  {
			DecreaseFill();
		}
		else if(Input.GetKeyDown(KeyCode.Space) && hasStartedCharge) {
			SpaceBarPressed();
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
	
	public void setRange(int weaponRange)
	{
		range = (float) 1.0 * weaponRange;
	}
	
	public void Hide() {
		this.isDisplayed = false;
		this.fill = 0.0f;
	}
	
	public void Reset() {
		this.fill = 0.5f;
		this.isDisplayed = false;
		this.hasFinishedCharge = false;
		this.hasStartedCharge = false;
	}
	
	public void Show() {
		this.fill = 0.5f;
		targetSectionNumber = 0;
		this.isDisplayed = true;
	}
	
	public float GetFillAmount() {
		return this.fill;
	}
	
	public bool Complete() {
		return this.hasFinishedCharge;
	}
	public int GetTargetSectionNumber()
	{
		return targetSectionNumber;
	}
}
