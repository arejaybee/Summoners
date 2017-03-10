using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    public int playerNumber;
    public int manaValue;
	// Use this for initialization
	void Start ()
    {
        manaValue = 5;
		if(playerNumber == 1)
		{
			manaValue += (int)(manaValue/3);
		}

	}
	
	// Update is called once per frame
	void Update ()
    {
        if(manaValue < 0)
        {
            manaValue = 0;
        }
        GetComponent<TextMesh>().text = "Mana: "+manaValue;
	}
}
