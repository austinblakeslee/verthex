using UnityEngine;
using System.Collections;

public class Symbols : MonoBehaviour {
	public Texture2D buildTexture;
	public Texture2D upgradeTexture;
	public Texture2D fortifyTexture;
	public Texture2D fightTexture;
	public Texture2D passTexture;
	private Vector3 scale;
	private float ow = 960;
	private float oh = 600;
	
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
		GUI.depth = -1;
		
		if(this.transform.GetComponent<Menu>().on == true) {
			GUI.Label (new Rect(760,435,70,70),buildTexture);
			GUI.Label (new Rect(760,520,70,70),passTexture);
			if(TowerSelection.GetSelectedSection() != null) {
				if(!(TowerSelection.GetSelectedSection() != null && TowerSelection.GetSelectedSection().attributes.sp >= TowerSelection.GetSelectedSection().attributes.maxSP)) {
					GUI.Label (new Rect(760,255,50,50),fortifyTexture);
				}
				if (!(TowerSelection.GetSelectedSection() != null && (TowerSelection.GetSelectedSection().GetWeaponInfo() == "Nothing" || TurnOrder.ceasefire > TurnOrder.turnNum))) {	
					GUI.Label (new Rect(760,375,50,50),fightTexture);
				}
				if(!(TowerSelection.GetSelectedSection() != null && TowerSelection.GetSelectedSection().attributes.weapon.GetUpgradeLevel() >= TowerSelection.GetSelectedSection().attributes.weapon.maxUpgradeLevel)) {
					GUI.Label (new Rect(760,315,50,50),upgradeTexture);
				}
			}
		}
	}
}
