using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CardCtrl : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{

	FieldScript fieldManager;

	public GameObject field_Card;
	public Transform field_CardTr;
	public GameObject ui_Card;
	public Transform ui_CardTr;
	public Vector3 mousePos;

	public static int chkOn = 4;

	Vector3 ui_CardPos;
	Vector3 ui_LocP;
    MakeGame mg;

	Vector3 scaletemp;
	Quaternion rotationtemp;
	Vector3 postemp;

	RectTransform recttemp;
    //private bool showChk;

    //연결시키기  7월14일
    /*public CardCtrl(MakeGame mg)
    {
        this.mg = mg;
    }*/
    //7월14일 끝
    #region IEndDragHandler implementation

    public void OnEndDrag (PointerEventData eventData)
	{
		if (fieldManager.FieldCardCount < 5) {
			System.Threading.Thread.Sleep (200);
			ui_Card.SetActive (false);
			chkOn--;

			CreateCard (transform.position, new Quaternion (90f, 0f, 0f, 0f));
			fieldManager.FieldCardCount++;
			ui_Card = null;
			transform.localPosition = ui_CardPos;

			// 19, July new Update
			recttemp.transform.localScale = scaletemp;
			recttemp.transform.localPosition = postemp;
			recttemp.transform.localRotation = rotationtemp;
			//end
		} else {
			recttemp.transform.localScale = scaletemp;
			recttemp.transform.localPosition = postemp;
			recttemp.transform.localRotation = rotationtemp;
		}
	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		transform.localPosition = (new Vector3(Input.mousePosition.x - 320.0f, Input.mousePosition.y, Input.mousePosition.z)) ;
		Debug.Log (Input.mousePosition);
	}

	#endregion

	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData eventData)
	{
		ui_Card = gameObject;
		ui_CardPos = transform.localPosition;// transform.position;
	}
	#endregion

	void CreateCard(Vector3 v, Quaternion q)
	{
		var temp = Instantiate (field_Card, v, q) as GameObject;
		temp.transform.parent = GameObject.Find ("FieldManager").transform;
	}


	void Start()
	{
		fieldManager = GameObject.Find ("FieldManager").GetComponent<FieldScript> ();

		Screen.SetResolution (Screen.width, Screen.width * 16 / 9, true);
		scaletemp = ui_Card.transform.localScale;
		postemp = ui_Card.transform.localPosition;
		rotationtemp = ui_Card.transform.localRotation;

		recttemp = ui_Card.transform.GetComponent<RectTransform> ();
	}

	void Update(){
		//mousePos = 
	}
	/*
	public void OnClickCard()
	{
		//카드의 생성(Field)과 숨김 (UI In Hands)
		Invoke("CreateCard", 0.6f);
		//showChk = false;
		//Invoke ("HideCardUI", 0.6f);
	}
	
	void HideCardUI()
	{
		if (showChk == false) 
		{
			cardUI.SetActive (showChk);
		}
	}
	*/
}
