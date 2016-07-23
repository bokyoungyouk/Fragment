using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CardSuffle : MonoBehaviour {
    //카드 프리팹
    public GameObject OCard;
    public List<Card> cards;
    
    public CardSuffle()
    {
        this.cards = new List<Card>();
    }

    public void make_all_cards()
    {
        

        List<CARD_TYPE> cards_type = new List<CARD_TYPE>();
        for(int num=0;num<60;num++)
        {
            if(num<15)
                cards_type.Add(CARD_TYPE.FIRE);
            else if(num<30)
                cards_type.Add(CARD_TYPE.ICE);
            else if(num<45)
                cards_type.Add(CARD_TYPE.WIND);
            else
                cards_type.Add(CARD_TYPE.EARTH);
        }
        this.cards.Clear();
        for(byte number = 0; number<60;number++)
        {
            this.cards.Add(new Card(number, cards_type[(int)number]));
        }
          
    }
    public void shuffle()
    {
        System.Random ran = new System.Random();
        
        int n = cards.Count;
        while(n > 1)
        {
            n--;
            int num = ran.Next(n+1);
            Card value = cards[num];
            cards[num] = cards[n];
            cards[n] = value;
        }
    }

    // 마우스로 카드 터치시 발동
    public IEnumerator GetCard(Transform curCard)
    {
        //0.12  최정상
        Vector3 Temp_Position=curCard.localPosition;
        Temp_Position.x = 0;
        
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            // 카드가 다 뒤졉혀 질 때까지
            if (curCard.localEulerAngles.z < 180)
            {
                curCard.localEulerAngles += new Vector3(0, 0, 1);   // z축으로 카드 뒤집기
                curCard.localPosition -= new Vector3(-10f, 0, 0);  // x축으로 카드 센터로 옮기기

                // 
                if (curCard.localEulerAngles.z < 180)
                    curCard.localPosition += new Vector3(0, 10f, 0);
              /*  else
                    curCard.localPosition -= new Vector3(0, 0.5f, 0);*/
            }
            else
            {
                //카드 오브젝트의 숫자 ex)/firesprit_/59/(clone)/  컷팅 -> 59
                //Debug.Log(curCard.gameObject.name.Substring(curCard.gameObject.name.IndexOf('_')+1,2));
                break;
            }
                
        }
        Temp_Position.y = ((Temp_Position.y - 0.12f ) * -1)+ 0.01f;
        curCard.localPosition = Temp_Position;
        Destroy(curCard.gameObject);
    }

    /*// Use this for initialization
	void Start () {
        for (int num = 0; num < 60; num++)
            CreateCard(num);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void CreateCard(int num)
    {
        Instantiate(OCard, new Vector3(11f, (0.01f*num), -2f), Quaternion.identity);
    }*/
}
