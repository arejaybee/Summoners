using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : Character {


    // Use this for initialization
    void Start()
    {
        name = "Minotaur";
        maxHp = 10;
        hp = 10;
        move = 5;
        attkRange = 1;
        attk = 5;
        defense = 0;
        cost = 15;
        canMove = true;
    }
}
