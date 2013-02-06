using UnityEngine;
using System.Collections;

public class MaterialCostLabelUpdate : DefaultMenuAction,MenuAction {

	public string materialName;
	
	public override void Action() {
		ValueStore.selectedMaterial = SectionComponentFactory.GetMaterial(materialName);
		PlayClickSound();
	}
}
