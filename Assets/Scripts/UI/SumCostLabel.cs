using UnityEngine;
using System.Collections;

public class SumCostLabel : MonoBehaviour {

	void Update() {
		SectionMaterial m = ValueStore.selectedMaterial;
		SectionWeapon w = ValueStore.selectedWeapon;
		int mCost = m == null ? 0 : m.GetCost();
		int wCost = w == null ? 0 : w.GetCost();
		GetComponent<MenuItem>().text = "Total: " + (mCost + wCost)+"";
	}
}
