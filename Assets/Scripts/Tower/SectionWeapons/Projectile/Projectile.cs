using UnityEngine;
using System.Collections;

public abstract class Projectile : MonoBehaviour {

	public Section target;
	public bool inProgress = false;
	
	public void SetTarget(Section s) {
		target = s;
		inProgress = true;
	}
	
	public abstract IEnumerator ImpactTarget();
	
}
