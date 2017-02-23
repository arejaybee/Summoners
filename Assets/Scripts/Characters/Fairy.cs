using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fairy : Character {
    public const int FAIRY_MANA = 2;
	// Use this for initialization
	void Start ()
    {
        name = "Fairy";
        maxHp = 1;
        hp = 1;
        move = 5;
        attkRange = 1;
        attk = 1;
        defense = 0;
        cost = 2;
        description = name + "\n HP: " + hp + "/" + maxHp + "\n Attk: " + attk + " Def: " + defense + "\n Attk Range: " + attkRange + "\n Move: " + move+"\nSpecial: +3 mana per turn";
        canMove = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (hp <= 0f)
        {
            Destroy(this.gameObject);
        }
        description = name + "\n HP: " + hp + "/" + maxHp + "\n Attk: " + attk + " Def: " + defense + "\n Attk Range: " + attkRange + "\n Move: " + move + "\nSpecial: +3 mana per turn";
        if (stun > 0)
        {
            description = description + "\nStun for: " + Mathf.CeilToInt((float)(stun - 1) / 2) + " rounds";
        }
        checkColorOfPlayer();
    }

    //Fairies add 3 to the player's mana pool each turn
    public override void EndTurn()
    {
        Mana[] m = FindObjectsOfType<Mana>();
        for(int i = 0; i < m.Length; i++)
        {
            if(m[i].playerNumber == playerNumber)
            {
                m[i].manaValue = m[i].manaValue + FAIRY_MANA;
            }
        }
    }
}
