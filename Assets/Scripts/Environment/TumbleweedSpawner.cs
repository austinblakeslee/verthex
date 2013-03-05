using UnityEngine;
using System.Collections;

public class TumbleweedSpawner : MonoBehaviour {

	public Transform[] tumbleweedLocations;
	public GameObject tumbleweed;

	// Use this for initialization
	void Start () {
		StartCoroutine(SpawnTumbleweed());
	}
	
	IEnumerator SpawnTumbleweed() {
		while(true) {
			yield return new WaitForSeconds(Random.value * 60.0f);
			Transform spot = tumbleweedLocations[(int)Random.Range(0, tumbleweedLocations.Length)];
			(Instantiate(tumbleweed, spot.position, spot.rotation) as GameObject).AddComponent("Tumbleweed");
		}
	}
}
