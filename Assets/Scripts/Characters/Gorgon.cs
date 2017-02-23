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
        cost = 10;
        description = name + "\n HP: " + hp + "/" + maxHp + "\n Attk: " + attk + " Def: " + defense + "\n Attk Range: " + attkRange + "\n Move: " + move + "\nSpecial: Stun enemy units\nthat attack this\nfor 1 turn";
        canMove = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (hp <= 0f)
        {
            Destroy(this.gameObject);
        }
        description = name + "\n HP: " + hp + "/" + maxHp + "\n Attk: " + attk + " Def: " + defense + "\n Attk Range: " + attkRange + "\n Move: " + move + "\nSpecial: Stun enemy\nunits that attack this\nfor 1 turn";
        if (stun > 0)
        {
            description = description + "\nStun for: " + Mathf.CeilToInt((float)(stun - 1) / 2) + " rounds";
        }
        checkColorOfPlayer();
    }
}
