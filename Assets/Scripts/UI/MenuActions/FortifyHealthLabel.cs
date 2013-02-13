using UnityEngine;
using System.Collections;

public class FortifyHealthLabel : MonoBehaviour {

	void Update() {
		if(ValueStore.selectedMaterial != null) {
			SectionMaterial m = ValueStore.selectedMaterial;
			GetComponent<MenuItem>().text = "Added Health: " + m.GetSPPerRepair();
		}
	}
}
