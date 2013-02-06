using UnityEngine;
using System.Collections;

public class Builder : MonoBehaviour {

	private static Builder instance;
	public GameObject woodBlock;
	public GameObject steelBlock;
	public GameObject stoneBlock;
	public GameObject ballista;
	public GameObject catapult;
	public GameObject cannon;
	private GameObject spawnPoint;
	
	void Start() {
		instance = this;
		spawnPoint = new GameObject();
	}
	
	private void StartBuild(Player player, SectionMaterial m, SectionWeapon w) {
		GameObject block = null;
		GameObject weapon = null;
		GameObject topOfTower = player.GetTower().GetTopSection();
		GameObject playerSpot = player.towerBase.towerPoint;
		if(topOfTower == null) {
			spawnPoint.transform.position = playerSpot.transform.position;
			spawnPoint.transform.Translate(0,25,0);
		} else {
			Vector3 old = playerSpot.transform.position;
			spawnPoint.transform.position = new Vector3(old.x, topOfTower.collider.bounds.max.y, old.z);
			spawnPoint.transform.Translate(0,25,0);
		}
		if(m.mtype == "Wood") {
			block = Instantiate(woodBlock,spawnPoint.transform.position,Quaternion.identity) as GameObject;
		}
		else if(m.mtype == "Steel") {
			block = Instantiate(steelBlock,spawnPoint.transform.position,Quaternion.identity) as GameObject;
		}
		else if(m.mtype == "Stone") {
			block = Instantiate(stoneBlock,spawnPoint.transform.position,Quaternion.identity) as GameObject;
		}
		block.transform.Find("FireCam").camera.enabled = false;
		block.transform.Find("HitCam").camera.enabled = false;
		block.transform.Find("CollapseCam").camera.enabled = false;
		if(w.wtype == "Ballista") {
			weapon = Instantiate(ballista) as GameObject;
		} else if(w.wtype == "Catapult") {
			weapon = Instantiate(catapult) as GameObject;
		} else if(w.wtype == "Cannon") {
			weapon = Instantiate(cannon) as GameObject;
		}
		if(weapon != null) {
			Vector3 localPos = weapon.transform.localPosition;
			Vector3 localScale = weapon.transform.localScale;
			weapon.transform.parent = block.transform;
			weapon.transform.localPosition = localPos;
			weapon.transform.localScale = localScale;
		}
		Vector3 positionToLookAt = TurnOrder.otherPlayer.towerBase.towerPoint.transform.position;
		positionToLookAt.y = block.transform.position.y;
		block.transform.LookAt(positionToLookAt);
		
		SectionController sc = block.GetComponent<SectionController>();
		Section s = new Section(m, w);
		sc.SetSection(s);
		sc.SetPlayer(player);
		player.Build(block);
		TowerSelection.LocalSelectSection(player.playerNumber, sc.GetHeight()-1);
	}

	public static void BuildSection(Player player, SectionMaterial material, SectionWeapon weapon) {
		instance.StartBuild(player, material, weapon);
	}
}
