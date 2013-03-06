using UnityEngine;
using System.Collections;

public class Builder : MonoBehaviour {

	private static Builder instance;
	private GameObject spawnPoint;
	
	void Start() {
		instance = this;
		spawnPoint = new GameObject();
	}
	
	private void StartBuild(Player player, Tower t, SectionMaterial m, SectionWeapon w) {
		GameObject block = null;
		GameObject weapon = null;
		Section topOfTower = t.GetTopSection();
		GameObject playerSpot = t.towerBase.towerPoint;
		if(topOfTower == null) {
			spawnPoint.transform.position = playerSpot.transform.position;
			spawnPoint.transform.Translate(0,25,0);
		} else {
			Vector3 old = playerSpot.transform.position;
			spawnPoint.transform.position = new Vector3(old.x, topOfTower.collider.bounds.max.y, old.z);
			spawnPoint.transform.Translate(0,25,0);
		}
		block = Instantiate(m.GetPrefab(),spawnPoint.transform.position,Quaternion.identity) as GameObject;
		block.transform.Find("FireCam").camera.enabled = false;
		block.transform.Find("HitCam").camera.enabled = false;
		block.transform.Find("CollapseCam").camera.enabled = false;
		if (w.wtype != "Nothing"){
			weapon = Instantiate(w.GetPrefab()) as GameObject; //I believe the here lies the issue for why building Nothing doesn't work? maybe.
		//if(weapon != null) {
			Vector3 localScale = weapon.transform.localScale;
			weapon.transform.parent = block.transform;
			weapon.transform.localPosition = block.transform.Find("WeaponLocation").localPosition;
			weapon.transform.localScale = localScale;
		}
		Section sc = block.GetComponent<Section>();
		SectionAttributes s = new SectionAttributes(m, w);
		sc.attributes = s;
		player.Build(sc, t);
		TowerSelection.LocalSelectSection(t, sc.attributes.height);
	}

	public static void BuildSection(Player p, Tower t, SectionMaterial material, SectionWeapon weapon) {
		instance.StartBuild(p, t, material, weapon);
	}
}
