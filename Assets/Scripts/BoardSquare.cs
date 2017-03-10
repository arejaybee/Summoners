using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSquare : MonoBehaviour {

    public int playerNum = 0;
    public bool redWin = false;
    public bool blueWin = false;
	// Use this for initialization
	void Start ()
    {
        playerNum = 0;
        GetComponent<MeshRenderer>().material.color = new Color(0.5f,0,0);
        redWin = false;
        blueWin = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //loop to see if board needs to change color, also checks if game is over
        var changeColor = false;

        //if enter we auto end turn
        if (Input.GetKeyDown(KeyCode.E) && !FindObjectOfType<Cursor2>().onSummonScreen && !FindObjectOfType<Cursor>().isMoving)
        {
            changeColor = true;
        }
        //otherwise see if they can still move
        else
        {
            checkGameOver();
        }

        //Ends a turn, and changes the color of the board
        if(changeColor && !redWin && !blueWin)
        {
            changeTurn();   
        }
		
	}

    /*
     * Checks to see if either summoner is dead
     * if so, the game is over, and we no longer change board colors
     * The line "Application.Quit" should also end the game, but does not....
     */
    void checkGameOver()
    {
        if (GameObject.Find("Summoner2") == null)
        {
            redWin = true;
        }
        else if (GameObject.Find("Summoner1") == null)
        {
            blueWin = true;
        }
        if (redWin && blueWin)
        {
            Application.Quit();
        }
    }

    /*
     * Changes turns from blue to red and back
     */
    void changeTurn()
    {
        //update the player turn 
        playerNum = (playerNum + 1) % 2;
        Mana[] m = FindObjectsOfType<Mana>();
        int manaNum = 1;
        string sumName = "";

        //get the correct values for summoner name, color, and mana number
        if (playerNum == 0)
        {
            manaNum = 1;
            sumName = "Summoner1";
            GetComponent<MeshRenderer>().material.color = new Color(0.5f, 0, 0);
        }
        else
        {
            manaNum = 0;
            sumName = "Summoner2";
            GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0.5f);
        }

        //add mana to the new player's pool
        m[manaNum].manaValue = m[manaNum].manaValue + Mathf.Max(1,(int)(m[manaNum].manaValue/3));

        //move the cursor over to the new summoner
        GameObject.Find("Cursor").transform.position = new Vector3(GameObject.Find(sumName).transform.localPosition.x, GameObject.Find("Cursor").transform.position.y, GameObject.Find(sumName).transform.localPosition.z);


        Character[] chars = FindObjectsOfType<Character>();
        //let every unit that player controls do their special thing
        for (int i = 0; i < chars.Length; i++)
        {
            if (chars[i].playerNumber - 1 == playerNum)
            {
                chars[i].EndTurn();
            }
        }
        
        //set canMove to true
        for (int i = 0; i < chars.Length; i++)
        {
            if (chars[i].stun > 1)
            {
                chars[i].stun = chars[i].stun - 1;
            }
            else
            {
                chars[i].canMove = true;
                chars[i].stun = 0;
            }
        }
    }
}
