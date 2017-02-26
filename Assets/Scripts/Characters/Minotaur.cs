using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : Character {


    // Use this for initialization
    void Start()
    {
        name = "Minotaur";
        maxHp = 12;
        hp = 12;
        move = 4;
        attkRange = 1;
        attk = 6;
        defense = 0;
        cost = 15;
        canMove = true;
    }
}
