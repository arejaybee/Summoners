using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        //offset = transform.position - player.transform.position;
        transform.position = new Vector3(0, 21.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
         Mana[] m = FindObjectsOfType<Mana>();
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) && !FindObjectOfType<Cursor2>().tryingToSummon && !FindObjectOfType<Cursor>().characterSelected)
        {
            moveToSummonScreen();
        }
        
        else if (!FindObjectOfType<Cursor2>().onSummonScreen)
        {
            transform.position = new Vector3(0, 21.5f, 0);
            for(int i = 0; i < m.Length; i++)
            {
                if(m[i].playerNumber == 1)
                {
                    m[i].GetComponent<TextMesh>().color = Color.red;
                    m[i].transform.position = new Vector3(-16, 0, 0);
                }
                else
                {
                    m[i].GetComponent<TextMesh>().color = Color.blue;
                    m[i].transform.position = new Vector3(9, 0, 0);
                }
            }
        }
    }
    void moveToSummonScreen()
    {
        Mana[] m = FindObjectsOfType<Mana>();
        //move cursor to top of select screen
        GameObject.Find("Cursor2").transform.position = new Vector3(-91f, 0, 0f);

        //set background to be the same as player turn
        GameObject.Find("Background2").GetComponent<MeshRenderer>().material.color = GameObject.Find("BoardSquare").GetComponent<MeshRenderer>().material.color;

        //move the appropriate mana number to the select screen
        for (int i = 0; i < m.Length; i++)
        {
            if (m[i].playerNumber -1 == FindObjectOfType<BoardSquare>().playerNum)
            {
                m[i].transform.position = new Vector3(-79, 0, 0);
                m[i].GetComponent<TextMesh>().color = Color.white;
            }
        }
        //move camera to select screen
        transform.position = new Vector3(-91, 35, -8);
        FindObjectOfType<Cursor2>().onSummonScreen = true;
    }

}
