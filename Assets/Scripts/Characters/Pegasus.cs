using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pegasus : Character {


    // Use this for initialization
    void Start()
    {
        name = "Pegasus";
        maxHp = 14;
        hp = 14;
        move = 6;
        attkRange = 1;
        attk = 7;
        defense = 1;
        cost = 15;
        description = name + "\n HP: " + hp + "/" + maxHp + "\n Attk: " + attk + " Def: " + defense + "\n Attk Range: " + attkRange+"\n Move: "+move;
        canMove = true;
    }
}
