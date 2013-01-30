using UnityEngine;
using System.Collections;

public class BuildAction : DefaultMenuAction,MenuAction {
	public GameObject steelBlock;
	public GameObject woodBlock;
	public GameObject brickBlock;
	public GameObject spawnPoint;
	public GameObject ballista;
	public GameObject catapult;
	public GameObject cannon;
	public Menu myMenu;
	
	public override void Action() {
		SectionMaterial m = ValueStore.selectedMaterial;
		SectionWeapon w = ValueStore.selectedWeapon;
		if(m != null && w != null) {
			Player currentPlayer = TurnOrder.currentPlayer;
			m = makeMaterial(ValueStore.selectedMaterial.mtype);
			w = makeWeapon(ValueStore.selectedWeapon.wtype);
			Section s = new Section(m, w);
			if(currentPlayer.GetResources() >= s.GetCost()) {
				GameObject section = this.BuildSection(m,w);
				SectionController c = section.GetComponent<SectionController>();
				c.SetSection(s);
				c.SetPlayer(currentPlayer);
				currentPlayer.Build(section);
				TowerSelection.LocalSelectSection(currentPlayer.playerNumber, c.GetHeight()-1);
				GetComponent<CollapseAnimator>().BeginAnimation(currentPlayer.GetTower());
				TurnOrder.ActionTaken();
			} else {
				ValueStore.helpMessage = "You don't have enough RP to do that!";
			}
		} else {
			ValueStore.helpMessage = "You must select a material and a weapon type!";
		}
	}
	
	private SectionMaterial makeMaterial(string m) {
		if(m == "Wood") {
			return new Wood();
		} else if(m == "Stone") {
			return new Stone();
		} else if(m == "Steel") {
			return new Steel();
		} else {
			return null;
		}
	}
	
	private SectionWeapon makeWeapon(string w) {
		if(w == "Nothing") {
			return new Nothing();
		} else if(w == "Ballista") {
			return new Ballista();
		} else if(w == "Catapult") {
			return new Catapult();
		} else if(w == "Cannon") {
			return new Cannon();
		} else {
			return null;
		}
	}
	
	public GameObject BuildSection(SectionMaterial m, SectionWeapon w) {
		GameObject block = null;
		GameObject weapon = null;
		Player currentPlayer = TurnOrder.currentPlayer;
		GameObject topOfTower = currentPlayer.GetTower().GetTopSection();
		GameObject playerSpot = currentPlayer.towerBase.towerPoint;
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
			block = Instantiate(brickBlock,spawnPoint.transform.position,Quaternion.identity) as GameObject;
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
		return block;
	}
	
	void Update() {
		if(myMenu.on) {
			SectionMaterial m = ValueStore.selectedMaterial;
			SectionWeapon w = ValueStore.selectedWeapon;
			if(m == null || w == null) {
				ValueStore.helpMessage = "Select both a material and a weapon.";
			} else if(m.GetCost() + w.GetCost() > TurnOrder.currentPlayer.GetResources()) {
				ValueStore.helpMessage = "You do not have enough resources to build that. Choose different options.";
			} else {
				int weight = (int)(m.GetWeightPerSP() * m.GetInitialSP()) + w.GetWeight();
				string help = "Weight: " + weight;
				help += "\nDamage: " + w.GetDamage();
				ValueStore.helpMessage = help;
			}
		}
	}
}