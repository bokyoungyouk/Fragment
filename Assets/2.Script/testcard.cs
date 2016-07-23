using UnityEngine;
using System.Collections;


public class testcard : MonoBehaviour {
    //public GameObject OCard;
    public Material tempMat;
    GameObject useObj;
    // Use this for initialization
    void Start () {
        for (int num = 0; num < 60; num++)
            CreateCard(num);
        useObj = GameObject.Find("CARD_earth_spirit");
        useObj.GetComponent<MeshRenderer>().material = null;

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void CreateCard(int num)
    {
        Material m= GetComponent<Material>();
        GameObject OCard = Resources.Load("CARD_Cube") as GameObject;
            //GameObject original = Resources.Load("hwatoo") as GameObject;
        
        
            
        Instantiate(OCard, new Vector3(75f, (0.002f * num), -16f), Quaternion.identity);
    }
}
