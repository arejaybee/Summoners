using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Werewolf : Character {


    // Use this for initialization
    void Start()
    {
        name = "Werewolf";
        maxHp = 16;
        hp = 16;
        move = 4;
        attkRange = 1;
        attk = 9;
        defense = 1;
        cost = 20;
        description = name + "\n HP: " + hp + "/" + maxHp + "\n Attk: " + attk + " Def: " + defense + "\n Attk Range: " + attkRange + "\n Move: " + move;
        canMove = true;
    }
}
