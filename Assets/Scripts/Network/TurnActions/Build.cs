using UnityEngine;
using System;
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
		return Array.IndexOf(SectionMaterial.materials, m.GetMaterialType());
	}
	
	private int EncodeWeapon(SectionWeapon w) {
		return Array.IndexOf(SectionWeapon.weapons, w.GetWeaponType());
	}
	
	private SectionMaterial DecodeMaterial() {
		return SectionComponentFactory.GetMaterial(SectionMaterial.materials[material]);
	}
	
	private SectionWeapon DecodeWeapon() {
		return SectionComponentFactory.GetWeapon(SectionWeapon.weapons[weapon]);
	}
}
