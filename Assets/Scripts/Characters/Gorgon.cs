using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gorgon : Character
{

    // Use this for initialization
    void Start()
    {
        name = "Gorgon";
        maxHp = 7;
        hp = 7;
        move = 3;
        attkRange = 1;
        attk = 2;
        defense = 0;
        cost = 15;
        extraDescription = "\nStuns units that\nattack it";
        canMove = true;
    }
}
