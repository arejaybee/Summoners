using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Character
{

    // Use this for initialization
    void Start()
    {
        name = "Dragon";
        maxHp = 10;
        hp = 10;
        move = 6;
        attkRange = 2;
        attk = 15;
        defense = 3;
        cost = 50;
        description = name + "\n HP: " + hp + "/" + maxHp + "\n Attk: " + attk + " Def: " + defense + "\n Attk Range: " + attkRange + "\n Move: " + move+ "\nSpecial: -10 mana per turn\nDies at 0 mana";

    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0f)
        {
            Destroy(this.gameObject);
        }
        description = name + "\n HP: " + hp + "/" + maxHp + "\n Attk: " + attk + " Def: " + defense + "\n Attk Range: " + attkRange + "\n Move: " + move+"\nSpecial: -10 mana per turn\nDies at 0 mana";

        //if a player has 0 mana, dragons die
        Mana[] m = FindObjectsOfType<Mana>();
        for (int i = 0; i < m.Length; i++)
        {
            if (m[i].playerNumber == playerNumber)
            {
                if(m[i].manaValue <= 0)
                {
                    Destroy(this.gameObject);
                }
            }
        }
        checkColorOfPlayer();
    }

    //dragons drain a player's mana at the end of a turn
    public override void EndTurn()
    {
        Mana[] m = FindObjectsOfType<Mana>();
        for (int i = 0; i < m.Length; i++)
        {
            if (m[i].playerNumber == playerNumber)
            {
                m[i].manaValue = m[i].manaValue - 10;
            }
        }
    }
}
