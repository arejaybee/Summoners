using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Werewolf : Character {


    // Use this for initialization
    void Start()
    {
        name = "Werewolf";
        maxHp = 13;
        hp = 13;
        move = 4;
        attkRange = 1;
        attk = 7;
        defense = 1;
        cost = 50;
        extraDescription = "\nDamaging units\nmakes them join you";
        canMove = true;
    }

    public override void fight(Character char2)
    {
        char2.hp = char2.hp - Mathf.Max(attk-char2.defense, 0);
        //When a Gorgan is hit by a creature, that creature is stunned for 1 round
        if (char2.name == "Gorgon" && name != "Gorgon")
        {
            stun = 3;
        }
        if (char2.name != "Summoner")
        {
            //convert the unit, but you cant move it this turn.
            char2.playerNumber = playerNumber;
            char2.transform.eulerAngles = transform.eulerAngles;
            char2.canMove = false;
        }
    }
}
