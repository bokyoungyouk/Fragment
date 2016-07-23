using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Field_CardCtrl : MonoBehaviour {

	//Sorting sort;

	public GameObject f_Card;
	public Rigidbody cardRb; //물리 체크 
	//public int cardId;
	Vector3 f_cardPos; //생성된 필드카드 좌표

	private float force;	//카드의 운동 에너지 

	void Awake(){
		//sort = GameObject.Find ("Field_Manager").GetComponent<Sorting> ();
	}	

	void Start () {
		force = 100.0f;
		f_Card.transform.rotation = new Quaternion(0f,0f,360f,0f);
		f_cardPos = f_Card.GetComponent<Transform> ().transform.position;
		cardRb = GetComponent<Rigidbody> ();
		//Debug.Log ("Hi");
		//sort.insertObj (f_cardPos.x, f_Card);
		GameObject.Find ("FieldManager").GetComponent<Sorting> ().insertObj(this.transform.position.x, f_Card);
		//Debug.Log ("Bye");
		//Debug.Log (GameObject.Find ("FieldManager").GetComponent<Sorting> ().fieldCard.Count);
	}
	

	void Update () {
		f_Card.transform.DOMoveY(0.1f,1.0f);
	}

	void FixedUpdate(){
		cardRb.AddForce (transform.up * force);	//물리 운동 
		//Debug.Log (f_cardPos);
	}
}
