using UnityEngine;
using System.Collections;

public class WeaponCostLabel : MonoBehaviour {

	void Update() {
		MenuItem item = GetComponent<MenuItem>();
		SectionWeapon m = ValueStore.selectedWeapon;
		if(m == null) {
			item.text = "";
		} else {
			item.text = m.GetWeaponType() + ": " + m.GetCost();
		}
	}
	
}
