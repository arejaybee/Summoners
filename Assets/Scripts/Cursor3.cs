using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor3 : MonoBehaviour {
	Vector3 idlePos = new Vector3(0, -1, 0);
	bool isActive;
	GameObject summonUnit;
	Vector3 startPos;
	float orgX;
	float orgZ;
	// Use this for initialization
	void Start ()
	{
		transform.position = idlePos;
		isActive = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(isActive)
		{
			//move cursor around the grid
			tryToMoveCharacter();

			//select space to put unit
			if(Input.GetKeyDown(KeyCode.Z))
			{
				tryToConfirm();
			}

			//cancel entirely
			if (Input.GetKeyDown(KeyCode.X))
			{
				Destroy(summonUnit);
				GameObject.Find("Cursor2").GetComponent<Cursor2>().onSummonScreen = true;
				isActive = false;
				GameObject.Find("Cursor").transform.position = new Vector3(transform.position.x, 0.05f, transform.position.z);
				transform.position = idlePos;
				deleteMoveTiles();
			}
		}
	}

	public void tryToConfirm()
	{
		Character[] c = GameObject.FindObjectsOfType<Character>();
		bool canConfirm = true;

		for(int i = 0; i < c.Length; i++)
		{
			if(!c[i].gameObject.Equals(summonUnit) && c[i].gameObject.transform.position == summonUnit.transform.position)
			{
				canConfirm = false;
			}
		}

		if(canConfirm)
		{
			GameObject.Find("Cursor").transform.position = new Vector3(transform.position.x,0.05f,transform.position.z);
			transform.position = idlePos;
			GameObject.Find("Cursor2").GetComponent<Cursor2>().tryingToSummon = false;
			isActive = false;
			deleteMoveTiles();
			summonUnit.GetComponent<Character>().canMove = GameObject.Find("Summoner" + summonUnit.GetComponent<Character>().playerNumber).GetComponent<Character>().canMove;
			GameObject.Find("Summoner" + summonUnit.GetComponent<Character>().playerNumber).transform.FindChild("Mana").GetComponent<Mana>().manaValue -= summonUnit.GetComponent<Character>().cost;
		}
	}

	void deleteMoveTiles()
	{
		//delete all of the movement tiles
		GameObject[] movement = GameObject.FindGameObjectsWithTag("Movable");
		for (int i = 0; i < movement.Length; i++)
		{
			if (movement[i].transform.position.x != -150)
			{
				Destroy(movement[i]);
			}
		}
	}

	public void placeUnit(ref GameObject unit)
	{

		summonUnit = unit;
		isActive = true;

		//Locate the appropriate summoner
		GameObject summoner = GameObject.Find("Summoner" + unit.GetComponent<Character>().playerNumber);
		print(summoner.transform.position);

		startPos = summoner.transform.position;

		//designate the area they can summon in, based on the summoner's attk range
		int maxX = (int)(startPos.x + summoner.GetComponent<Character>().attkRange);
		int minX = (int)(startPos.x - summoner.GetComponent<Character>().attkRange);

		int maxZ = (int)(startPos.z + summoner.GetComponent<Character>().attkRange);
		int minZ = (int)(startPos.z - summoner.GetComponent<Character>().attkRange);

		//can't go outside the board
		maxX = Mathf.Min(maxX, 7);
		minX = Mathf.Max(minX, -7);
		maxZ = Mathf.Min(maxZ, 7);
		minZ = Mathf.Max(minZ, -7);

		placeUnit(unit, maxX, maxZ, minX, minZ, summoner.GetComponent<Character>().attkRange);
	}

	//This is here because I am bad at coding
	private void placeUnit(GameObject unit,int maxX, int maxZ, int minX, int minZ, float range)
	{

		GameObject.Find("Cursor").transform.position -= new Vector3(0, 1, 0);

		transform.position = startPos;

		orgX = startPos.x;
		orgZ = startPos.z;

		//displays all of the possible spaces that character can move to
		for (int k = (int)(0 - range); k <= range; k++)
		{
			for (int j = (int)(0 - range); j <= range; j++)
			{
				if (orgX + k >= minX && orgX + k <= maxX && orgZ + j <= maxZ && orgZ + j >= minZ)
				{
					GameObject newPiece = (GameObject)Instantiate(GameObject.Find("moveTile"), new Vector3(orgX + k, 0 , orgZ + j), transform.rotation);
					newPiece.tag = "Movable";
				}
			}
		}
	}

	/*
   * Precondition: Character is selected
   * Description: Moves the character around, with the cursor
   * Note: This is a separate function from the one above because of
   * the added checks that are needed if there is a character.
   */
	void tryToMoveCharacter()
	{
		//set limits on movement
		var maxPosX = (GameObject.Find("BoardSquare").transform.localScale.x - 1) / 2;
		var minPosX = (GameObject.Find("BoardSquare").transform.localScale.x - 1) / 2 * -1;

		var maxPosZ = (GameObject.Find("BoardSquare").transform.localScale.z - 1) / 2;
		var minPosZ = (GameObject.Find("BoardSquare").transform.localScale.z - 1) / 2 * -1;

		//get current position
		var x = transform.position.x;
		var y = transform.position.y;
		var z = transform.position.z;


		float range = GameObject.Find("Summoner" + summonUnit.GetComponent<Character>().playerNumber).GetComponent<Character>().attkRange;
		//the mins and maxes are further limited by the character's movement
		if (maxPosX > orgX + range)
		{
			maxPosX = orgX + range;
		}
		if (minPosX < orgX - range)
		{
			minPosX = orgX - range;
		}
		if (maxPosZ > orgZ + range)
		{
			maxPosZ = orgZ + range;
		}
		if (minPosZ < orgZ - range)
		{
			minPosZ = orgZ - range;
		}

		//input a direction to move the cursor
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			z = z - 1;
		}
		else if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			z = z + 1;
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			x = x - 1;
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			x = x + 1;
		}

		//only move around the map
		if (transform.position.x >= minPosX && transform.position.x <= maxPosX && transform.position.z >= minPosZ && transform.position.z <= maxPosZ)
		{
			transform.position = new Vector3(x, y, z);
		}

		//puts the cursor back within the map
		if (transform.position.x > maxPosX)
		{
			transform.position = new Vector3(maxPosX, transform.position.y, transform.position.z);
		}
		else if (transform.position.x < minPosX)
		{
			transform.position = new Vector3(minPosX, transform.position.y, transform.position.z);
		}

		if (transform.position.z > maxPosZ)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, maxPosZ);
		}
		else if (transform.position.z < minPosZ)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, minPosZ);
		}
		summonUnit.transform.localPosition = new Vector3(transform.position.x, summonUnit.transform.position.y, transform.position.z);
	}

}
