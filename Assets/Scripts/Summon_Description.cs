using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon_Description : MonoBehaviour
{
   
	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        GameObject cursor = GameObject.Find("Cursor2");
        Character chara = cursor.GetComponent<Cursor2>().getSelectedCharacter();
        GetComponent<TextMesh>().text = chara.description;
        GetComponent<TextMesh>().color = Color.white;
    }
}
