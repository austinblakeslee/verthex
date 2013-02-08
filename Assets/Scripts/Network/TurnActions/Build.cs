using UnityEngine;
using System.Collections;

public class Build : TurnAction {

	public int material;
	public int weapon;
	
	public Build(string actionMessage) : base("Build") {
		ParseActionMessage(actionMessage);
	}

	public Build(SectionMaterial m, SectionWeapon w) : base("Build") {
		this.playerNumber = TurnOrder.myPlayer.playerNumber;
		this.material = EncodeMaterial(m);
		this.weapon = EncodeWeapon(w);
	}
	
	public override void Perform() {
		Player p = TurnOrder.GetPlayerByNumber(playerNumber);
		SectionMaterial m = DecodeMaterial();
		SectionWeapon w = DecodeWeapon();
		ValueStore.helpMessage = "Building section";
		Builder.BuildSection(p, m, w);
	}
	
	public override string GetActionMessage() {
		return FormatActionMessage(this.material+"", this.weapon+"");
	}
	
	protected override void ParseActionMessage(string actionMessage) {
		string[] tokens = actionMessage.Split(TOKEN_SEPARATOR);
		this.playerNumber = int.Parse(tokens[0]);
		this.material = int.Parse(tokens[2]);
		this.weapon = int.Parse(tokens[3]);
	}
	
	private int EncodeMaterial(SectionMaterial m) {
		return TurnOrder.GetPlayerByNumber(playerNumber).faction.EncodeSectionMaterial(m.mtype);
	}
	
	private int EncodeWeapon(SectionWeapon w) {
		return TurnOrder.GetPlayerByNumber(playerNumber).faction.EncodeSectionWeapon(w.wtype);
	}
	
	private SectionMaterial DecodeMaterial() {
		return TurnOrder.GetPlayerByNumber(playerNumber).faction.GetSectionMaterial(material);
	}
	
	private SectionWeapon DecodeWeapon() {
		return TurnOrder.GetPlayerByNumber(playerNumber).faction.GetSectionWeapon(weapon);
	}
}
