using UnityEngine;
using System.Collections;

public class Build : TurnAction {

	public int material;
	public int weapon;
	
	public Build(string actionMessage) : base("Build") {
		ParseActionMessage(actionMessage);
	}

	public Build(int t, SectionMaterial m, SectionWeapon w) : base("Build") {
		this.playerNumber = TurnOrder.myPlayer.playerNumber;
		this.towerNumber = t;
		this.material = EncodeMaterial(m);
		this.weapon = EncodeWeapon(w);
	}
	
	public override void Perform() {
		SectionMaterial m = DecodeMaterial();
		SectionWeapon w = DecodeWeapon();
		ValueStore.helpMessage = "Building section";
		Builder.BuildSection(TurnOrder.GetPlayerByNumber(playerNumber), GetTower(), m, w);
	}
	
	public override string GetActionMessage() {
		return FormatActionMessage(this.material+"", this.weapon+"");
	}
	
	protected override void ParseActionMessage(string actionMessage) {
		string[] tokens = actionMessage.Split(TOKEN_SEPARATOR);
		this.playerNumber = int.Parse(tokens[0]);
		this.towerNumber = int.Parse(tokens[1]);
		this.material = int.Parse(tokens[FIRST_AVAILABLE_INDEX]);
		this.weapon = int.Parse(tokens[FIRST_AVAILABLE_INDEX+1]);
	}
	
	private int EncodeMaterial(SectionMaterial m) {
		return GetTower().faction.EncodeSectionMaterial(m.mtype);
	}
	
	private int EncodeWeapon(SectionWeapon w) {
		return GetTower().faction.EncodeSectionWeapon(w.wtype);
	}
	
	private SectionMaterial DecodeMaterial() {
		return GetTower().faction.GetSectionMaterial(material);
	}
	
	private SectionWeapon DecodeWeapon() {
		return GetTower().faction.GetSectionWeapon(weapon);
	}
	
	private Tower GetTower() {
		return TurnOrder.GetPlayerByNumber(playerNumber).GetTower(towerNumber);
	}
}
