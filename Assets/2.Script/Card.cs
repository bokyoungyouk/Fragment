using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum CARD_TYPE : byte
{
    FIRE,
    ICE,
    WIND,
    EARTH
}
public class Card : MonoBehaviour {

    // 0~49
    public byte number { get; private set; }
    // FIRE,ICE,WIND,EARTH
    public CARD_TYPE card_type { get; private set; }

    public Card(byte number,CARD_TYPE card_type)
    {
        this.number = number;
        this.card_type = card_type;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
