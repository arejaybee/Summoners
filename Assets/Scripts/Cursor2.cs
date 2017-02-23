using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor2 : MonoBehaviour {
    public bool onSummonScreen;
    private int playerTurn = 1;
    private int MIN_Z = -15;
    private int MAX_Z = 0;
    public string[] summonNames = {"Fairy", "Griffon", "Minotaur", "Gorgon", "Centaur","Pegasus", "Werewolf", "Dragon"};
    public int summonOffset = 0;

    // Use this for initialization
    void Start()
    {

        transform.position = new Vector3(-91f, 0.1f, 0f);
        onSummonScreen = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if the camera is on this screen
        if (onSummonScreen)
        {

            //see if the user moved up or down
            tryToMove();

            //if the user hit x, go back to the game screen
            if (Input.GetKeyDown(KeyCode.X))
            {
                checkCancel();
            }
            //if the player hits z, try to summon a thing
            //added enter as confirm here as well because people seem to instictively hit enter to pick a thing
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
            {
                //this check is just incase the player somehow hits down and z fast enough
                //to go out of bounds and try to confirm. Lets avoid that error
                if (transform.position.z <= MAX_Z && transform.position.z >= MIN_Z)
                {
                    checkConfirm();
                }
            }
        }
        else
        {
        }
        //find out whos turn it is
        playerTurn = FindObjectOfType<BoardSquare>().playerNum + 1;

    }

    //checks if you can cancel
    void checkCancel()
    {
        onSummonScreen = false;
    }


    //checks if you can confirm an option
    void checkConfirm()
    {
        var afford = false;
        var sumName = "Summoner" + playerTurn;

        //find out who summoned it
        var pieceName = "Background2";

        //make the new unit, based on selection
        int index = ( -1* (int)transform.position.z / 3 ) + summonOffset;
        pieceName = summonNames[index];
       

        //create a new object
        GameObject newPiece = (GameObject)Instantiate(GameObject.Find(pieceName), GameObject.Find(sumName).transform.position, transform.rotation);
        //make it the same material as the summoner
        newPiece.GetComponent<Character>().setMaterial(GameObject.Find("Player" + playerTurn).GetComponent<MeshRenderer>().material);
        //give it a player number
        newPiece.GetComponent<Character>().setPlayerNum(playerTurn);
        if (playerTurn == 2)
        {
            newPiece.transform.eulerAngles = new Vector3(0, 180, 0);
        }


        //if so try to spawn the piece
        var spawned = tryToSpawn(newPiece);
        if (spawned)
        {
            //check if the summoner can afford the piece
            afford = canAfford(newPiece);
            if (afford)
            {
                onSummonScreen = false;
            }
            else
            {
                Destroy(newPiece);
            }
        }
        else
        {
            Destroy(newPiece);
        }
    }


    /*
     * Preconditions: A character was selected to be summoned
     * Postconditions: true if the player has enough mana to summon
     */
    bool canAfford(GameObject chara)
    {
        //Find out how much mana that player has
        Mana[] m = FindObjectsOfType<Mana>();
        Mana theirMana = m[0];
        for (int i = 0; i < m.Length; i++)
        {
            //check if you can afford it
            if (m[i].playerNumber == playerTurn)
            {
                theirMana = m[i];
            }
        }

        //check if they have enough mana here and subtract that mana
        if (theirMana.manaValue >= chara.GetComponent<Character>().getCost())
        {
            theirMana.manaValue = theirMana.manaValue - chara.GetComponent<Character>().getCost();
        }
        else
        {
            return false;
        }
        return true;
    }
    

    /*
     * Precondition: There is an affordable unit selected
     * Postcondition: True if there is a spot adjacent to summoner to spawn that unit
     */ 
    bool tryToSpawn(GameObject chara)
    {
        Character[] chars = FindObjectsOfType<Character>();
        var right = true;
        var left = true;
        var up = true;
        var down = true;

        var summonerName = "Summoner"+playerTurn;

        //stores position of the summoner
        float x = GameObject.Find(summonerName).transform.position.x;
        float y = GameObject.Find(summonerName).transform.position.y;
        float z = GameObject.Find(summonerName).transform.position.z;
        //print("The summoner is at position: " + x + " , " + y + " , " + z);

        //try to summon unit adjacent to the summoner
        for (int i = 0; i < chars.Length; i++)
        {
            //print("Character "+i+" is at: " + chars[i].transform.position);
            if (x + 1 > 7 || (chars[i].transform.position.x == (x+1) && chars[i].transform.position.z == z))
                {
                    right = false;
                }
                else if (x - 1 < -7 || (chars[i].transform.position.x == (x - 1) && chars[i].transform.position.z == z))
                {
                    left = false;
                }
                else if (z + 1 > 7 || (chars[i].transform.position.x == x && chars[i].transform.position.z == (z + 1)))
                {
                    up = false;
                }
                else if (z - 1 < -7 || (chars[i].transform.position.x == x && chars[i].transform.position.z == (z - 1)))
                {
                    down = false;
                }
        }

        if(!right && !up && !left && !down)
        {
            return false;
        }
        //afterwards see if/where you can spawn
        if(right)
            {
                chara.transform.position = new Vector3(x + 1, y, z);
                return true;
            }
        else if(left)
            {
                chara.transform.position = new Vector3(x - 1, y, z);
                return true;
            }
        else if(up)
            {
                chara.transform.position = new Vector3(x, y, z + 1);
                return true;
            }
        else if(down)
            {
            //print("Down was fine!");
                 chara.transform.position = new Vector3(x, y, z - 1);
                 return true;
            }
        return false;
    }

    /*
     * Moves the cursor up and down the select menue
     */
    void tryToMove()
    {

        var x = transform.position.x;
        var y = transform.position.y;
        var z = transform.position.z;

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            z = z - 3;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            z = z + 3;
        }


        //do not let the cursor move out of bounds
        if (transform.position.z >= MIN_Z && transform.position.z <= MAX_Z)
        {
            transform.position = new Vector3(x, y, z);
        }

        //if you move up, and you cannot adjust the list
        if (transform.position.z > MAX_Z && summonOffset == 0)
        {
            transform.position = new Vector3(x, y, MAX_Z);
        }
        else if(transform.position.z > MAX_Z)
        {
            summonOffset = summonOffset - 1;
            transform.position = new Vector3(x, y, MAX_Z);
        }
        //if you move down but you cannot adjust the list
        if (transform.position.z < MIN_Z && summonOffset + 6 == summonNames.Length)
        {
            transform.position = new Vector3(x, y, MIN_Z);
        }
        else if(transform.position.z < MIN_Z)
        {
            summonOffset = summonOffset + 1;
            transform.position = new Vector3(x, y, MIN_Z);
        }

    }
    public Character getSelectedCharacter()
    {
        int index = (-1 * (int)transform.position.z / 3) + summonOffset;
        string pieceName = summonNames[index];
        //print(pieceName);
        Character chara = GameObject.Find(pieceName).GetComponent<Character>();
        return chara;
    }
    public string[] getSummonNames()
    {
        return summonNames;
    }
    public int getOffsetNum()
    {
        return summonOffset;
    }

}
