using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class MakeGame : MonoBehaviour {

	public List<GameObject> ui_card;

    Sprite back_image;
    CardSuffle cardmaneger;
    Transform DECK;

	//Vector3[] ui_cardScale;	//old scale
	Vector3[] ui_cardPos;	//old position
	Quaternion[] ui_cardRot;	//old rotation

    // Use this for initialization
    void Start () {

        cardmaneger = new CardSuffle ();

        this.cardmaneger.cards = new List<Card>();
        this.cardmaneger.make_all_cards();
        this.cardmaneger.shuffle();
       // make_all_cards();
        //shuffle();
        for (int num = 0; num < 60; num++)
            CreateCard(num);

		foreach (Transform t in GameObject.Find("MyHand").transform) {
			ui_card.Add (t.gameObject);
		}

		for (int i = 0; i < 4; i++) {
			ui_cardPos [i].Set(ui_card [i].transform.position.x, ui_card[i].transform.position.y, ui_card[i].transform.position.z);
		}

    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			//Debug.Log (Physics.Raycast (ray, out hit));
			if (CardCtrl.chkOn < 4) {
				Debug.Log ("xx");
				if (Physics.Raycast (ray, out hit)) {
					//Debug.Log ("yy");
					//Debug.Log (hit.collider.name);
					if (hit.collider.gameObject.tag == "BACK") {
						//7월 14일 추가(수정 수정~~~~~)
						/*CardCtrl cc=new CardCtrl(this);
                    
                    //카드 액티브가 flase(없는상태)면 찾아서 true(있는상태)면  카드를 있는상태(드로우)로만듬
                    if(cc.ui_Card.name=="Card1"&&cc.ui_Card.active==false)
                    {
                        //레이캐스트된 카드의 이름중 번호만 따서 ex:/firespirit_/59/(clone)/{parse시킴} 리스트의 값 가져옴
                        int deckNum = int.Parse(hit.collider.transform.parent.gameObject.name.Substring(hit.collider.transform.parent.gameObject.name.IndexOf('_') + 1, 2));
                        Debug.Log(this.cardmaneger.cards[deckNum].card_type);
                        cc.ui_Card.SetActive(true);
                    }
                    */
						//7월 14일 끝
						//Debug.Log("zz");
						for (int i = 0; i < 4; i++) {
							if (!ui_card [i].activeSelf) {
								ui_card [i].SetActive (true);
								ui_card [i].transform.position = ui_cardPos [i];
								break;
							}
						}

						StartCoroutine (cardmaneger.GetCard (hit.collider.transform.parent));
						CardCtrl.chkOn++;
					}
				}
			}
		}
    }

    void ChangeMaterial (GameObject tempObj)
    {
        //tempObj.GetComponent<MeshRenderer>().material = tempMat;
    }

  
  /*  public void shuffle()
    {
        System.Random ran = new System.Random();

        int n = this.cardmaneger.cards.Count;
        while (n > 1)
        {
            n--;
            int num = ran.Next(n + 1);
            Card value = this.cardmaneger.cards[num];
            this.cardmaneger.cards[num] = this.cardmaneger.cards[n];
            this.cardmaneger.cards[n] = value;
        }
    }*/
    void CreateCard(int num)
    {
        
        string Snum = num.ToString();
        if (num < 10)
            Snum = '0' + Snum;
        // OCard;
        if (this.cardmaneger.cards[num].card_type==CARD_TYPE.FIRE)
        {
            GameObject OCard = Resources.Load("CARD_fire_spirit") as GameObject;
            OCard.name = "Fire_" + Snum;
            Instantiate(OCard, new Vector3(68f, (0.002f * (num+1))+1f, -10f), Quaternion.identity);
           // OCard.transform.parent = null;
        }
        else if(this.cardmaneger.cards[num].card_type == CARD_TYPE.ICE)
        {
            GameObject OCard = Resources.Load("CARD_ice_spirit") as GameObject;
            OCard.name = "Ice_" + Snum;
            Instantiate(OCard, new Vector3(68f, (0.002f * (num + 1))+1f, -10f), Quaternion.identity);
            
            //OCard.transform.parent = null;
        }
        else if(this.cardmaneger.cards[num].card_type == CARD_TYPE.WIND)
        {
            GameObject OCard = Resources.Load("CARD_wind_spirit") as GameObject;
            OCard.name = "Wind_" + Snum;
            Instantiate(OCard, new Vector3(68f, (0.002f * (num + 1))+1f, -10f), Quaternion.identity);
            //OCard.transform.parent = null;
        }
        else if (this.cardmaneger.cards[num].card_type == CARD_TYPE.EARTH)
        {
            GameObject OCard = Resources.Load("CARD_earth_spirit") as GameObject;
            OCard.name = "Earth_" + Snum;
            Instantiate(OCard, new Vector3(68f, (0.002f * (num + 1))+1f, -10f), Quaternion.identity);
            //OCard.transform.parent = null;
        }

        /*GameObject OCard_temp = Resources.Load("CARD_earth_spirit") as GameObject;
        Instantiate(OCard_temp, new Vector3(11f, (0.002f * num), -2f), Quaternion.identity);
        OCard_temp.GetComponent<MeshRenderer>().material = null;*/
        
    }
}
