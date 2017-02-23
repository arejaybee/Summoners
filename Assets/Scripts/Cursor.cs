using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {

    public float mSpeed;
    public bool characterSelected; //true if there is a selected Character
    private Character selectedChar; //a Character selected by the cursor
    public bool isMoving = false;
    private float orgX, orgY, orgZ;
    private int MIN_X = -7;
    private int MAX_X = 7;
    private int MAX_Z = 7;
    private int MIN_Z = -7;

	// Use this for initialization
	void Start ()
    { 
        transform.position = new Vector3(0, 0.1f, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {

        isMoving = false;
        //only move this cursor if we are not on the summoning screen
        if (!FindObjectOfType<Cursor2>().onSummonScreen)
        {
            //if a player hits z, try to select a character (only if you havent already)
            if (Input.GetKeyDown(KeyCode.Z) && characterSelected == false)
            {
                tryCharSelect();
            }

            //if a character is selected, deselect them (ends their movement)
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                tryDeselect();
            }

            //X is a way to cancel/deselect a character without using up their movement
            if (Input.GetKeyDown(KeyCode.X) && characterSelected == true)
            {
                //move the cahracter back to where they were and note we no longer have a selected character
                selectedChar.transform.localPosition = new Vector3(orgX, orgY, orgZ);
                characterSelected = false;
                
                deleteMovementTiles();
            }
            //if there is no character selected, see if the cursor can move in a given direction
            if (!characterSelected)
            {
                tryToMove();
            }
            //otherwise try to mvoe character, and cursor in a given direction(has some added stipulations)
            else
            {
                tryToMoveCharacter();
            }
        }
    }

    /*
     * Preconditons:There is currently a selected character
     * Postconditions: That character's "canMove" will be set false, and there will no longer be a selected character
     * Description: Ends the movement of a character in a selected square, also lets the deselected character fight all
     * of the characters within its attack range
     */ 
    void tryDeselect()
    {
        characterSelected = false;

        Character[] chars = FindObjectsOfType<Character>();
        //look at all characters
        for (int i = 0; i < chars.Length; i++)
        {
            //excluding the selected one's team
            if (chars[i].playerNumber != (selectedChar.playerNumber))
            {
                //if they are within range of the selected character
                if ((chars[i].transform.localPosition.x == transform.position.x) && Mathf.Abs(chars[i].transform.localPosition.z - transform.position.z) <= selectedChar.attkRange)
                {
                    //deal damage
                    selectedChar.fight(chars[i]);
                }
                if ((chars[i].transform.localPosition.z == transform.position.z) && Mathf.Abs(chars[i].transform.localPosition.x - transform.position.x) <= selectedChar.attkRange)
                {
                    //deal damage
                    selectedChar.fight(chars[i]);
                }
            }
        }
        deleteMovementTiles();
        selectedChar.setCanMove(false);
    }

    /*
     * Called whenever the player is done moving to delete any 
     * movement tiles on the screen
     */
    void deleteMovementTiles()
    {
        //delete all of the movement tiles
        GameObject[] movement = GameObject.FindGameObjectsWithTag("Movable");
        for (int i = 0; i < movement.Length; i++)
        {
            if (movement[i].transform.position.x != -150)
            {
                Destroy(movement[i]);
            }
        }
    }

    /*
     * Preconditions: There are no characters currently selected
     * Postconditions: There will be a character binded to the cursor
     * Description: Allows players to select one of their units for moving or attacking.
     */ 
    void tryCharSelect()
    {
        isMoving = true;
        //find the position of all characters
        Character[] chars = FindObjectsOfType<Character>();
        //print(chars.Length);

        //check to see if there is a character above the cursor
        for (int i = 0; i < chars.Length; i++)
        {
            //if the character can move on this turn
            if (correctPlayerColor(chars[i]))
            {
                //finds the character in chars[i] that matches the character currently being selected
                if (transform.position.x.Equals(chars[i].transform.position.x) && transform.position.z.Equals(chars[i].transform.position.z))
                {
                    //make that the selected character
                    selectedChar = chars[i];
                    characterSelected = true;

                    //save the original cooridinates incase we cancel the movement
                    orgX = transform.localPosition.x;
                    orgY = chars[i].transform.localPosition.y;
                    orgZ = transform.localPosition.z;

                    //displays all of the possible spaces that character can move to
                    for (int k = (int)(0 - selectedChar.move); k <= selectedChar.move; k++)
                    {
                        for (int j = (int)(0 - selectedChar.move); j <= selectedChar.move; j++)
                        {
                            if (orgX + k >= MIN_X && orgX + k <= MAX_X && orgZ + j <= MAX_Z && orgZ + j >= MIN_Z)
                            {
                                GameObject newPiece = (GameObject)Instantiate(GameObject.Find("moveTile"), new Vector3(orgX + k, (transform.position.y - 0.05f), orgZ + j), transform.rotation);
                                newPiece.tag = "Movable";
                            }
                        }
                    }
                }
            }
        }

       
    }

    /*
     * Will tell if a Character c is able to be selected/moved this turn
     * True if the character matches the current player's turn and can move
     */ 
    bool correctPlayerColor(Character c)
    {
        BoardSquare b = FindObjectOfType<BoardSquare>();
        //if board is red, character is red, and can move
        if(b.playerNum == 0)
        {
            if(c.playerNumber == 1 && c.canMove)
            {
                return true;
            }
        }

        //if board is blue, character is blue, and character can move
        else if (b.playerNum == 1)
        {
            if (c.playerNumber == 2 && c.canMove)
            {
                return true;
            }
        }
        return false;
    }

    /*
     * Precondition: No character selected
     * Description: Moves cursor in direction given by arrow key input (if any)
     */ 
    void tryToMove()
    {
        //stores the current x,y, and z coordinates
        var x = transform.position.x;
        var y = transform.position.y;
        var z = transform.position.z;

        //get input and change variables
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            z = z - 1;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            z = z + 1;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            x = x - 1;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            x = x + 1;
        }


        //only move around the map
        if (transform.position.x >= MIN_X && transform.position.x <= MAX_X && transform.position.z >= MIN_Z && transform.position.z <= MAX_Z)
        {
            transform.position = new Vector3(x, y, z);
        }

        //puts the cursor back within the map
        else if (transform.position.x > MAX_X)
        {
            transform.position = new Vector3(MAX_X, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < MIN_X)
        {
            transform.position = new Vector3(MIN_X, transform.position.y, transform.position.z);
        }

        else if (transform.position.z > MAX_Z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, MAX_Z);
        }
        else if (transform.position.z < MIN_Z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, MIN_Z);
        }
        
    }

    /*
     * Precondition: Character is selected
     * Description: Moves the character around, with the cursor
     * Note: This is a separate function from the one above because of
     * the added checks that are needed if there is a character.
     */
    void tryToMoveCharacter()
    {

        isMoving = true;
        //set limits on movement
        var maxPosX = (GameObject.Find("BoardSquare").transform.localScale.x - 1) / 2;
        var minPosX = (GameObject.Find("BoardSquare").transform.localScale.x - 1) / 2 * -1;

        var maxPosZ = (GameObject.Find("BoardSquare").transform.localScale.z - 1) / 2;
        var minPosZ = (GameObject.Find("BoardSquare").transform.localScale.z - 1) / 2 * -1;
        
        //get current position
        var x = transform.position.x;
        var y = transform.position.y;
        var z = transform.position.z;

        //the mins and maxes are further limited by the character's movement
        if (maxPosX > orgX + selectedChar.move)
        {
            maxPosX = orgX + selectedChar.move;
        }
        if (minPosX < orgX - selectedChar.move)
        {
            minPosX = orgX - selectedChar.move;
        }
        if (maxPosZ > orgZ + selectedChar.move)
        {
            maxPosZ = orgZ + selectedChar.move;
        }
        if (minPosZ < orgZ - selectedChar.move)
        {
            minPosZ = orgZ - selectedChar.move;
        }

        var movedZUp = false;
        var movedZDown = false;
        var movedXUp = false;
        var movedXDown = false;

        //input a direction to move the cursor
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            z = z - 1;
            movedZDown = true;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            z = z + 1;
            movedZUp = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            x = x - 1;
            movedXDown = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            x = x + 1;
            movedXUp = true;
        }
        
        //if the character would move into another character
        //push them back to their original xyz position
        Character[] chars = FindObjectsOfType<Character>();
        for (int i = 0; i < chars.Length; i++)
        {
            if (!chars[i].Equals(selectedChar))
            {
                if (chars[i].transform.position.Equals(new Vector3(x,chars[i].transform.position.y,z)))
                {
                    if(movedXDown)
                    {
                        x = x + 1;
                    }
                    if(movedXUp)
                    {
                        x = x - 1;
                    }
                    if(movedZDown)
                    {
                        z = z + 1;
                    }
                    if(movedZUp)
                    {
                        z = z - 1;
                    }
                }
            }
        }

        //only move around the map
        if (transform.position.x >= minPosX && transform.position.x <= maxPosX && transform.position.z >= minPosZ && transform.position.z <= maxPosZ)
        {
            transform.position = new Vector3(x, y, z);
        }
        //puts the cursor back within the map
        if (transform.position.x > maxPosX)
        {
            transform.position = new Vector3(maxPosX, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < minPosX)
        {
            transform.position = new Vector3(minPosX, transform.position.y, transform.position.z);
        }

        if (transform.position.z > maxPosZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, maxPosZ);
        }
        else if (transform.position.z < minPosZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, minPosZ);
        }
        //Moves character to wherever the cursor was moved to
        if (characterSelected)
        {
            selectedChar.transform.localPosition = new Vector3(transform.position.x, selectedChar.transform.position.y, transform.position.z);
        }
    }

}
