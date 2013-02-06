using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildAction : DefaultMenuAction,MenuAction {
	public GameObject steelBlock;
	public GameObject woodBlock;
	public GameObject brickBlock;
	public GameObject spawnPoint;
	public GameObject ballista;
	public GameObject catapult;
	public GameObject cannon;
	public GameObject sniper;
	public GameObject gattlingGun;
	public GameObject pistols;
	public GameObject blaster;
	public GameObject disintegrationBeam;
	public GameObject eyeBlaster;
	public GameObject drainBeam;
	public Dictionary<string, object> sectionObjects = new Dictionary<string, object>();

	public Menu myMenu;
	
	public void Start()
	{
		sectionObjects.Add("Wood", new Wood(woodBlock));
		sectionObjects.Add("Stone", new Stone(brickBlock));
		sectionObjects.Add ("Steel", new Steel(steelBlock));
		sectionObjects.Add ("Nothing", new Nothing());
		//if (Player.theme = "Cowboy"){
		sectionObjects.Add ("Ballista", new Ballista(ballista));
		sectionObjects.Add ("Catapult", new Catapult(catapult));
		sectionObjects.Add ("Cannon", new Cannon(cannon));
		sectionObjects.Add ("Pistols", new Pistols(pistols));
		sectionObjects.Add ("Gattling Gun", new GattlingGun(gattlingGun));
		sectionObjects.Add ("Sniper", new Sniper(sniper));
		//if (Player.theme == "Area 51"){
		sectionObjects.Add("Blaster", new Blaster(blaster));
		sectionObjects.Add("Disintegration Beam", new DisintegrationBeam(disintegrationBeam));
		sectionObjects.Add ("Eye Blaster", new EyeBlaster(eyeBlaster));
		sectionObjects.Add ("Drain Beam", new DrainBeam(drainBeam));
			
	}
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
		if(sectionObjects.ContainsKey (m)) {
			return sectionObjects[m] as SectionMaterial;
		} else {
			Debug.Log (m + ". Returning Null.");
			return null;
		}
	}
	
	private SectionWeapon makeWeapon(string w) {
		if(sectionObjects.ContainsKey(w)) {
			return sectionObjects[w] as SectionWeapon;
		}  else {
			Debug.Log(w + ". Returning null.");
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
		
	/*	
	 * if(m.mtype == "Wood") {
			block = Instantiate(woodBlock,spawnPoint.transform.position,Quaternion.identity) as GameObject;
		}
		else if(m.mtype == "Steel") {
			block = Instantiate(steelBlock,spawnPoint.transform.position,Quaternion.identity) as GameObject;
		}
		else if(m.mtype == "Stone") {
			block = Instantiate(brickBlock,spawnPoint.transform.position,Quaternion.identity) as GameObject;
		}
		*/
		if(sectionObjects.ContainsKey(m.mtype))
		{
			SectionMaterial mat = sectionObjects[m.mtype] as SectionMaterial;
			block = Instantiate(mat.GetModel(), spawnPoint.transform.position, Quaternion.identity) as GameObject;
		}
		
		block.transform.Find("FireCam").camera.enabled = false;
		block.transform.Find("HitCam").camera.enabled = false;
		block.transform.Find("CollapseCam").camera.enabled = false;
		/*if(w.wtype == "Ballista") {
			weapon = Instantiate(ballista) as GameObject;
		} else if(w.wtype == "Catapult") {
			weapon = Instantiate(catapult) as GameObject;
		} else if(w.wtype == "Cannon") {
			weapon = Instantiate(cannon) as GameObject;
		}*/
		
		if (sectionObjects.ContainsKey (w.wtype) && w.wtype != "Nothing")
		{
			SectionWeapon wep = sectionObjects[w.wtype] as SectionWeapon;
			weapon = Instantiate(wep.GetModel()) as GameObject;
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