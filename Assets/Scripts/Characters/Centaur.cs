using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centaur : Character {

	// Use this for initialization
	void Start ()
    {
        name = "Centaur";
        maxHp = 10;
        hp = 10;
        move = 5;
        attkRange = 2;
        attk = 5;
        defense = 0;
        cost = 20;
        extraDescription = "\nIgnores Defences";
        canMove = true;
    }
}
