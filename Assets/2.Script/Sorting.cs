using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sorting : MonoBehaviour {

    float[] posX = { -40, -30, -20, -10, 0, 10, 20, 30, 40 };
    int posY = 1;
    int posZ = -10;

    int DownY = 80;

    Vector3[] positions = new Vector3[9];


    public List<GameObject> fieldCard;

	// Use this for initialization
	void Start () {
        fieldCard = new List<GameObject>(5);
        for (int i = 0; i < 9; i++)
            positions[i] = new Vector3(posX[i], posY, posZ);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void insertObj(float mouseX, GameObject ins)
    {
        if (fieldCard.Count == 0)
            fieldCard.Add(ins);
        else
        {
            insert(mouseX, ins);
        }
        sort();
    }
    public void insert(float mouseX, GameObject ins)
    {
        
        switch (fieldCard.Count) // 카드 갯수
        { 
            case 1:
                if (mouseX < posX[4])
                    fieldCard.Insert(0, ins);
                else
                    fieldCard.Add(ins);
                break;
            case 2:
                if (mouseX < posX[3])
                {
                    fieldCard.Insert(0, ins);
                }
                else if (mouseX < posX[5])
                {
                    fieldCard.Insert(1, ins);
                }
                else
                    fieldCard.Add(ins);
                break;
            case 3:
                if (mouseX < posX[2])
                {
                    fieldCard.Insert(0, ins);
                }
                else if (mouseX < posX[4])
                {
                    fieldCard.Insert(1, ins);
                }
                else if (mouseX < posX[6])
                {
                    fieldCard.Insert(2, ins);
                }
                else
                    fieldCard.Add(ins);
                break;
            case 4:
                if (mouseX < posX[1])
                {
                    fieldCard.Insert(0, ins);
                }
                else if (mouseX < posX[3])
                {
                    fieldCard.Insert(1, ins);
                }
                else if (mouseX < posX[5])
                {
                    fieldCard.Insert(2, ins);
                }
                else if (mouseX < posX[7])
                {
                    fieldCard.Insert(3, ins);
                }
                else
                    fieldCard.Add(ins);
                break;
            case 5: // 생성 안해야지 이미 꽉참

                break;
        }
       
    }

    public void removeObj(GameObject obj)
    {
        for(int i=0;i<fieldCard.Count;i++){
            if (obj.transform == fieldCard[i].transform)
            {
                fieldCard.RemoveAt(i);
                sort();
            }
        }
    }
    void sort()
    {
        int even = -1;
        switch (fieldCard.Count) // 카드 갯수
        {
            case 1:
                fieldCard[0].transform.position = positions[4];
                break;
            case 2:

                for (int i = 0; i < 2; i++)
                {
                    fieldCard[i].transform.position = positions[4 + even];
                    even *= -1;
                }
                break;
            case 3:
                even *= 2;
                for (int i = 0; i < 3; i++)
                {
                    fieldCard[i].transform.position = positions[4 + even];
                    even += 2;
                }
                break;
            case 4:
                even *= 3;
                for (int i = 0; i < 4; i++)
                {
                    fieldCard[i].transform.position = positions[4 + even];
                    even += 2;
                }
                break;
            case 5:
                even *= 4;
                for (int i = 0; i < 5; i++)
                {
                    fieldCard[i].transform.position = positions[4 + even];
                    even += 2;
                }
                break;
        }
    }
}
