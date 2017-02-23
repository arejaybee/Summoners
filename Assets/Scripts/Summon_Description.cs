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
        //print(pos.z);
        /*if(pos.z == 0)
        {
            Fairy[] p = FindObjectsOfType<Fairy>();
            for (int i = 0; i < p.Length; i++)
            {
                if (p[i].playerNumber == 0)
                {
                    GetComponent<TextMesh>().text = p[i].description+"\nSpecial: +3 mana per turn";
                }
            }
        }
        else if (pos.z == -3)
        {
            Griffon[] p = FindObjectsOfType<Griffon>();
            for (int i = 0; i < p.Length; i++)
            {
                if (p[i].playerNumber == 0)
                {
                    GetComponent<TextMesh>().text = p[i].description;
                }
            }
        }
        
        else if (pos.z == -6f)
        {
            Minotaur[] p = FindObjectsOfType<Minotaur>();
            for (int i = 0; i < p.Length; i++)
            {
                if (p[i].playerNumber == 0)
                {
                    GetComponent<TextMesh>().text = p[i].description;
                }
            }
        }
        else if (pos.z == -9)
        {
            Pegasus[] p = FindObjectsOfType<Pegasus>();
            for(int i = 0; i < p.Length; i++)
            {
                if(p[i].playerNumber == 0)
                {
                    GetComponent<TextMesh>().text = p[i].description;
                }
            }
        }
        else if (pos.z == -12)
        {
            Werewolf[] p = FindObjectsOfType<Werewolf>();
            for (int i = 0; i < p.Length; i++)
            {
                if (p[i].playerNumber == 0)
                {
                    GetComponent<TextMesh>().text = p[i].description;
                }
            }
        }
        else if (pos.z <= -15)
        {
            Dragon[] p = FindObjectsOfType<Dragon>();
            for (int i = 0; i < p.Length; i++)
            {
                if (p[i].playerNumber == 0)
                {
                    GetComponent<TextMesh>().text = p[i].description+"\nSpecial: -10 mana per turn\nDies at 0 mana";
                }
            }
        }*/
        GetComponent<TextMesh>().color = Color.white;
    }
}
