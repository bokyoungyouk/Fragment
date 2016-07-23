using UnityEngine;
using System.Collections;

public class CubeScript : MonoBehaviour {
	private Ray ray;
	private RaycastHit hit;

	public int flag = 0;

	public int shock = 4;
	public int resistance = 2;
	public int stamina = 5;

	GameObject attackObj;
	GameObject deffenseObj;

	CubeScript attack;
	CubeScript deffense;

	private FieldScript fields;
	private LineScript lines;

	private Vector3 oldVector;

	// Use this for initialization
	void Start () {
		fields = GameObject.Find ("FieldManager").GetComponent ("FieldScript") as FieldScript;
		lines = GameObject.Find ("Line").GetComponent("LineScript") as LineScript;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetMouseButtonDown(0)){
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
				if(hit.transform.gameObject == gameObject){
					
					//flag = 1;
					//attack = hit.transform.gameObject.GetComponent<CubeScript>();
					//Debug.Log (hit.transform.gameObject.name);
					//fields.addBattleCard ();

					//lines.setStartPosition (oldVector = hit.transform.position + new Vector3 (0, 10, 0));


					FieldMouseDown (hit.transform.gameObject);
				}
			}
		}
		if( Input.GetMouseButtonUp(0) ) 
		{
			if (flag == 1) {
				ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				//Debug.Log ("aa");

				if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
					//if (hit.transform.gameObject.tag == "Field_Card") {
						
					//	defense = hit.transform.gameObject.GetComponent<CubeScript> ();
						//Debug.Log ("bb");

					//	if (defense.flag == 0) {
					//		defense.stamina = defense.stamina - attack.shock;
					//		attack.stamina = attack.stamina - defense.resistance;
					//		//Debug.Log ("cc");
					//	}
					//} else if (hit.transform.gameObject.tag == "Player") {

					//}
				
					FieldMouseUp (hit.transform.gameObject);
				}
			}

			lines.setStartPosition (new Vector3 (0, 10, 0));
			lines.setLastPosition (new Vector3 (0, 10, 0));
			flag = 0;
		}


		if (flag == 1) {
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			lines.setLastPosition (oldVector);
			if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
				
				//if(hit.transform.gameObject.tag == "Field_Card" || hit.transform.gameObject.tag == "Player")
				//	lines.setLastPosition (hit.transform.position + new Vector3 (0, 10, 0));
				
				FieldUpdate (hit.transform.gameObject);
			}
		}

	}

	public void FieldMouseDown(GameObject gameObject) {
		flag = 1;
		attack = gameObject.GetComponent<CubeScript>();
		attackObj = gameObject;
		//Debug.Log (hit.transform.gameObject.name);
		//fields.addBattleCard ();

		lines.setStartPosition (oldVector = gameObject.transform.position + new Vector3 (0, 10, 0));
	}

	public void FieldMouseUp(GameObject gameObject) {
		if (flag == 1) {
			if (gameObject.transform.gameObject.tag == "Field_Card") {

				deffense = gameObject.GetComponent<CubeScript> ();
				deffenseObj = gameObject;
				//Debug.Log ("bb");

				if (deffense.flag == 0) {
					CardToCard ();
					//Debug.Log ("cc");
				}
			} else if (gameObject.transform.gameObject.tag == "Player") {
				CardToPlayer ();
			}
			
		}

		lines.setStartPosition (new Vector3 (0, 10, 0));
		lines.setLastPosition (new Vector3 (0, 10, 0));
		flag = 0;
	}

	public void FieldUpdate(GameObject gameObject) {
		if (flag == 1) {
			lines.setLastPosition (oldVector);
			if(gameObject.transform.gameObject.tag == "Field_Card" || gameObject.transform.gameObject.tag == "Player")
				lines.setLastPosition (gameObject.transform.position + new Vector3 (0, 10, 0));
		}
	}

	void CardToCard() {
		deffense.stamina = deffense.stamina - attack.shock;
		attack.stamina = attack.stamina - deffense.resistance;
		Sorting tempSortScript;
		tempSortScript = GameObject.Find ("FieldManager").GetComponent ("Sorting") as Sorting;

		if (attack.stamina <= 0) {
			tempSortScript.removeObj (attackObj);
			Destroy (attackObj);
		}
		if (deffense.stamina <= 0) {
			tempSortScript.removeObj (deffenseObj);
			Destroy (deffenseObj);
		}
	}

	void CardToPlayer() {

	}
}
