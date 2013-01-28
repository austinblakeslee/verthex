using UnityEngine;
using System.Collections;

public class SumLabel : MenuItem {
	public SectionMaterial m;
	public SectionWeapon w;
	public SumLabel() : base() {
		this.type = MenuItemType.Button;
	}
}
