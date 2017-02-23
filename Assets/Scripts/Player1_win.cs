using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1_win : MonoBehaviour
{

    private BoardSquare b;
    // Use this for initialization
    void Start()
    {
        transform.position = new Vector3(-30, 0, 0);
        b = FindObjectOfType<BoardSquare>();
    }

    // Update is called once per frame
    void Update()
    {
        if (b.redWin == true)
        {
            transform.position = new Vector3(-15f, 0, -6.5f);
        }
    }
}
