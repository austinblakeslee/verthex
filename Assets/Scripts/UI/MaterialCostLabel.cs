using UnityEngine;
using System.Collections;

public class MaterialCostLabel : MonoBehaviour {

	void Update() {
		MenuItem item = GetComponent<MenuItem>();
		SectionMaterial m = ValueStore.selectedMaterial;
		if(m == null) {
			item.text = "";
		} else {
			item.text = m.GetMaterialType() + ": " + m.GetCost();
		}
	}
}
