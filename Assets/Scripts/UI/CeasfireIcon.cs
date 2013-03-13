using UnityEngine;
using System.Collections;

public class CeasfireIcon : MonoBehaviour {
	
	public Texture2D cfIcon;
	public Texture2D nocfIcon;
	private Vector3 scale;
	private float ow;
	private float oh;
	
	// Use this for initialization
	void Start () {
		ow = 960;
		oh = 600;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		scale.y = Screen.height/oh;
		scale.x = Screen.width/ow;
		scale.z = 1;
		float scaleX = Screen.width/ow;
		GUI.matrix = Matrix4x4.TRS(new Vector3((scaleX - scale.y)/2 * ow,0,0),Quaternion.identity,scale);
		if(TurnOrder.ceasefire > TurnOrder.turnNum) {
			GUI.Label (new Rect(Screen.width/2-130,20,200,200),cfIcon);
		}
		else if (TurnOrder.ceasefire == TurnOrder.turnNum)  {
			GUI.Label (new Rect(Screen.width/2-130,20,200,200),nocfIcon);
		}
	}
}
