using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterList : MonoBehaviour {
    public int playerNumber;
    private BoardSquare b;
    // Use this for initialization
    void Start ()
    {

        b = FindObjectOfType<BoardSquare>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMesh>().text = "";
        if (playerNumber == b.playerNum+1)
        {
            Character[] chars = FindObjectsOfType<Character>();
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i].transform.position.x == GameObject.Find("Cursor").transform.position.x && chars[i].transform.position.z == GameObject.Find("Cursor").transform.position.z)
                {
                    GetComponent<TextMesh>().text = chars[i].description;
                }
            }
        }
    }
}
