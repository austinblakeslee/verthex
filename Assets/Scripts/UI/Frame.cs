using UnityEngine;
using System.Collections;

public class Frame : MonoBehaviour {
	
	private Vector3 scale;
	private float ow = 960;
	private float oh = 600;
	public GUIStyle backStyle;
	
	// Use this for initialization
	void Start () {
	
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
		GUI.depth = 1;
		GUI.Label (new Rect(0,420,230,180),"",backStyle);
		GUI.Label (new Rect(730,420,230,180),"",backStyle);
		GUI.Label (new Rect(230,450,500,150),"",backStyle);
	}
}
