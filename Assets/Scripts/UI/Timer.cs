using UnityEngine;
using System.Collections;

public class TurnTimer : MonoBehaviour {
	
	public float maxTime = 10;
	private float currentTime;
	private float elapsedTime;
	
	// Use this for initialization
	void Start () {
		currentTime = maxTime;
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime = currentTime - Time.time;
		if(elapsedTime <= 0)
		{
			CombatLog.addLineNoPlayer("Time End");
			TurnOrder.SendAction(new Pass()); 
			currentTime = maxTime + Time.time; 
		}
		//Debug.Log("Time: " + ((int)elapsedTime).ToString());
		
	}
	
	void OnGUI()
	{
		GUI.Label(new Rect(Screen.width/2-50, 50, 100, 100), ((int)elapsedTime).ToString());	
	}
	
}
