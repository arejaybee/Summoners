using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summons : MonoBehaviour {
    string[] summonNames;
    int summonOffset = 0;
    string show = "";
	// Use this for initialization
	void Start ()
    {
       summonNames = FindObjectOfType<Cursor2>().getSummonNames();
        show = "";
        for (int i = 0; i < 4; i++)
        {
            show = show + summonNames[i + summonOffset] + " - " + GameObject.Find(summonNames[i + summonOffset]).GetComponent<Character>().cost + "\n\n";
        }
        show = show + summonNames[5 + summonOffset] + " - " + GameObject.Find(summonNames[5 + summonOffset]).GetComponent<Character>().cost;
        GetComponent<TextMesh>().text = show;
    }
	
	// Update is called once per frame
	void Update ()
    {
        summonOffset = FindObjectOfType<Cursor2>().getOffsetNum();
        show = "";
        for (int i = 0; i < 5; i++)
        {
            show = show + summonNames[i + summonOffset] + " - " + GameObject.Find(summonNames[i + summonOffset]).GetComponent<Character>().cost + "\n\n";
        }
        show = show + summonNames[5 + summonOffset] + " - " + GameObject.Find(summonNames[5 + summonOffset]).GetComponent<Character>().cost;
        GetComponent<TextMesh>().text = show;
    }
}
