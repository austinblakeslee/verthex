using UnityEngine;
using System.Collections;

public class MaterialCostLabelUpdate : DefaultMenuAction,MenuAction {

	public string materialName;
	public GameObject fromMenu = null;
	public GameObject toMenu = null;
	
	public override void Action() {
		ValueStore.selectedMaterial = SectionComponentFactory.GetMaterial(materialName);
		fromMenu.GetComponent<Menu>().on = false;
		toMenu.GetComponent<Menu>().on = true;
		PlayClickSound();
	}
}
