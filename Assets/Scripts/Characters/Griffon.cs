using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Griffon : Character {
    // Use this for initialization
    void Start()
    {
        name = "Griffon";
        maxHp = 7;
        hp = 7;
        move = 5;
        attkRange = 1;
        attk = 3;
        defense = 0;
        cost = 3;
        canMove = true;

    }
}
