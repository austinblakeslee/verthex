using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	/*public GameObject target;
	public int rotationSpeed = 30;
	//public double scrollSpeed = 10000.0;
	public float scrollAmountY = 99.0f;
	public float scrollAmountZ = 133.0f;
	public float moveSpeed = 100.0f;
	
	//These values are used when the camera needs to pan from current location to a given location
	public static bool panning = false;
	//Vector3 ArrayList
	public static ArrayList panPos;
	public static Vector3 panInterval;
	public float panSpeed = 0.015f;
	public static ArrayList waitIntervals;
	//Float ArrayList
	private int panIndex = 0;
	
	public const int ZOOMS_IN = 4;
	public const int ZOOMS_OUT = 0;
	public int zoom = 0;
	private float maxY, minY;
	public GameObject projectedCamera;
	
	
	//Map basic bounds for Map1 (Hardcode for all maps for dynamically pull bounds somehow and pass them to camera on start)
	//Max z
	public const int UP = 925;
	//Min z
	public const int DOWN = -400;
	//Max x
	public const int RIGHT = 1500;
	//Min x
	public const int LEFT = 500;
	
	
	
	public static bool control = true;
	
	//FOR THE DISABLED PAN VERSION
	private Transform focus = null;
	private Transform oldFocus = null;
	
	public void Start() {
		//Debug.Log(transform.position.y);
		minY = transform.position.y - (ZOOMS_IN * scrollAmountY);
		maxY = transform.position.y + (ZOOMS_OUT * scrollAmountY);
	
		panPos = new ArrayList();
		waitIntervals = new ArrayList();
		panning = false;
		
		
		//Zooms in from base camera settings
		transform.position = new Vector3(transform.position.x, transform.position.y - scrollAmountY, transform.position.z + 3*scrollAmountZ);
		zoom += 3;
	}

	public void Update () {
		if (panning) {
			
			if (panPos != null && panPos.Count > 0)
			{
				panning = false;
				control = true;
				transform.position = (Vector3) panPos[0];
			}
		}
		if(control) {
			
			if(Input.GetAxis("Mouse ScrollWheel") != 0) {
				if (Input.GetAxis("Mouse ScrollWheel") > 0 && transform.position.y > minY){
					transform.position = new Vector3(transform.position.x, transform.position.y - scrollAmountY, transform.position.z + scrollAmountZ);
					zoom++;
				}
				else if (Input.GetAxis("Mouse ScrollWheel") < 0 && transform.position.y < maxY){
					transform.position = new Vector3(transform.position.x, transform.position.y + scrollAmountY, transform.position.z - scrollAmountZ);
					zoom--;
				}
				
			}
			if (ValueStore.selectedSection != null)
			{
				focus = ValueStore.selectedSection.transform;
				if(hasFocusChanged())
				{
					PanningOn(new Vector3(ValueStore.selectedSection.transform.position.x, transform.position.y, ValueStore.selectedSection.transform.position.z - (833 - (scrollAmountZ * zoom))));
				}
				transform.LookAt(ValueStore.selectedSection.transform);
        		transform.Translate(Vector3.right * Time.deltaTime * rotationSpeed);
			}
			else
			{
				if (TurnOrder.play1turn)
				{
					focus = TurnOrder.player1Spot.transform;
					if(hasFocusChanged())
					{
						PanningOn(new Vector3(TurnOrder.player1Spot.transform.position.x, transform.position.y, TurnOrder.player1Spot.transform.position.z - (833 - (scrollAmountZ * zoom))));
					}
					transform.LookAt(TurnOrder.player1Spot.transform);
        			transform.Translate(Vector3.right * Time.deltaTime * rotationSpeed);
				}
				else
				{
					focus = TurnOrder.player2Spot.transform;
					if(hasFocusChanged())
					{
						PanningOn(new Vector3(TurnOrder.player2Spot.transform.position.x, transform.position.y, TurnOrder.player2Spot.transform.position.z - (833 - (scrollAmountZ * zoom))));
					}
					transform.LookAt(TurnOrder.player2Spot.transform);
        			transform.Translate(Vector3.right * Time.deltaTime * rotationSpeed);
				}
			}
		}
	}
	
	//For simple Pans (1 pan Position)
	public void PanningOn(Vector3 newPanPos) {
		panning = true;
		panPos = new ArrayList();
		waitIntervals = new ArrayList();
		panIndex = 0;
		panPos.Add(newPanPos);
		panInterval = (this.transform.position - newPanPos) * panSpeed;
	}
	
	//For multiple pan positions in a row but no wait intervals
	public void PanningOn(ArrayList newPanPos) {
		panning = true;
		panPos = newPanPos;
		waitIntervals = new ArrayList();
		panIndex = 0;
		
		//Assumes that the ArrayList pasted in are not empty/null... duh
		SetPanInterval((Vector3) panPos[0]);
	}
	
	//For multiple pans with varying wait intervals in between pans
	//Assumes that newWaitIntervals and newPanPos have a equal size/count <-----------
	public void PanningOn(ArrayList newPanPos, ArrayList newWaitIntervals) {
		panning = true;
		panPos = newPanPos;
		waitIntervals = newWaitIntervals;
		panIndex = 0;
		
		//Assumes that the ArrayLists pasted in are not empty/null... duh
		SetPanInterval((Vector3) panPos[0]);
	}
	
	public IEnumerator ResetPan() {
		this.transform.position = (Vector3) panPos[panIndex];
		if (waitIntervals.Count > panIndex) {
			panning = false;
			control = false;
			yield return new WaitForSeconds((float) waitIntervals[panIndex]);
			control = true;
		}
		if (panPos.Count > (panIndex + 1)) {
			panning = true;
			panIndex++;
			SetPanInterval((Vector3) panPos[panIndex]);
		}
		else {
			panning = false;
			panIndex = 0;
		}
	}
	
	public void SetPanInterval(Vector3 v) {
		panInterval = (this.transform.position - v) * panSpeed;
	}
	
	
	public bool hasFocusChanged()
	{
		if (focus == oldFocus)
		{
			return false;
		}
		else
		{
			oldFocus = focus;
			return true;
		}
	}*/
}
