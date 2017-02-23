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
        move = 4;
        attkRange = 2;
        attk = 5;
        defense = 1;
        cost = 15;
        description = name + "\n HP: " + hp + "/" + maxHp + "\n Attk: " + attk + " Def: " + defense + "\n Attk Range: " + attkRange + "\n Move: " + move+"Special: Ignores defense";
        canMove = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (hp <= 0f)
        {
            Destroy(this.gameObject);
        }
        description = name + "\n HP: " + hp + "/" + maxHp + "\n Attk: " + attk + " Def: " + defense + "\n Attk Range: " + attkRange + "\n Move: " + move + "\nSpecial: Ignores defense";
    }

    public override void fight(Character char2)
    {
        char2.hp = char2.hp - attk;

        //When a Gorgan is hit by a creature, that creature is stunned for 1 round
        if (char2.name == "Gorgon" && name != "Gorgon")
        {
            stun = 3;
        }
    }
}
