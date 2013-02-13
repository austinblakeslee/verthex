using UnityEngine;
using System.Collections;

public class FortifyCostLabel : MonoBehaviour {
	void Update() {
		if(ValueStore.selectedMaterial != null) {
			SectionMaterial m = ValueStore.selectedMaterial;
			GetComponent<MenuItem>().text = "Cost: $" + m.GetCostPerRepair();
		}
	}
}
