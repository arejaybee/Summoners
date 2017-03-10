using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : Character {

	// Use this for initialization
	void Start ()
	{
		name = "Summoner";
		maxHp = 25;
		hp = 25;
		move = 2;
		attkRange = 2;
		attk = 5;
		defense = 1;
		cost = 0;
		canMove = true;
	}
	
}
