using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Character
{

	// Use this for initialization
	void Start ()
	{
		name = "Knight";
		maxHp = 20;
		hp = 20;
		move = 4;
		attkRange = 1;
		attk = 6;
		defense = 1;
		cost = 20;
		canMove = true;
		counterAttack = true;
	}
	
}
