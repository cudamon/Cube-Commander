﻿using UnityEngine;
using System.Collections;
//Remember to have UI library
using UnityEngine.UI;

public class CollisionLeft : MonoBehaviour {

	public Transform block;
	public Transform CenterPieces;
	public Text ScoreCounter;
	public int ScoreInc;
	public AudioClip chime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		print ("Collision Left");
		Destroy(col.gameObject);

		// calculate location of new block, adjusting for angle of centerpiece
		float degree = transform.eulerAngles[2] ;
		
		// add 90 degrees for this quadrant
		float angle = (degree+90) * Mathf.PI / 180F;
		//print ("Angle = " + degree + " degrees " + angle + " radians");
		
		float x = transform.position.x;
		float y = transform.position.y;
		float xx = x + 0.5F * Mathf.Cos (angle);
		float yy = y + 0.5F * Mathf.Sin (angle);
		
		// On collision clone a new block
		Transform objcol = (Transform)Instantiate(block, new Vector2(xx, yy), CenterPieces.transform.rotation);
		
		
		// Find block and set parent to Centerpieces object
		//GameObject block2 = GameObject.Find("block(Clone)");
		objcol.transform.parent = CenterPieces.transform;
		
		//name of cloned block must be block not block(clone) so this fixes it
		objcol.name = block.name;
		
		//change sprite of cloned block to one that collided
		objcol.GetComponent<SpriteRenderer>().sprite = col.gameObject.GetComponent<SpriteRenderer>().sprite;

		// calculate position in grid for block
		// xx yy is current position - need to de-rotate
		//x' = x cos f - y sin f
		//y' = y cos f + x sin f
		float delta = (90F - degree) * Mathf.PI / 180F;
		int xd = Mathf.RoundToInt(xx * Mathf.Cos (delta) - yy * Mathf.Sin (delta));
		int yd = Mathf.RoundToInt(yy * Mathf.Cos (delta) + xx * Mathf.Sin (delta));
		//print ("rotated x = " + xx + " y = " + yy);
		//print ("derotated x = " + xd + " y = " + yd);
		int i = xd + GridArray.s;
		int j = yd + GridArray.s;
		// if i or j are outside of the grid - end game
		if (i<=0 || i >= 2*GridArray.s+1 || j <= 0 || j >= 2*GridArray.s+1) {
			Application.LoadLevel ("Heredity");
		}
		GridArray.grid [i, j] = objcol;
		// check if 4 in a row 
		// check to the right -
		bool same = true;
		//print ("i,j=" + i + ","+j);
		int k = 1;
		//print("sprite  0:"+GridArray.grid [i,j].GetComponent<SpriteRenderer>().sprite);
		// go 4 spaces or until null spot or different spot is found => not same 
		while (same==true & k<4) {
			if(GridArray.grid [i+k,j] == null) {
		//		print("null "+k);
		//		print ("i+k,j=" + (i+k) + ","+j);
				same = false;
			}
			else {
		//		print ("i+k,j=" + (i+k) + ","+j);
		//		print("sprite  "+k+":"+GridArray.grid [i+k,j].GetComponent<SpriteRenderer>().sprite);
				// compare to see if the next piece is same sprite
				if(GridArray.grid [i,j].GetComponent<SpriteRenderer>().sprite != GridArray.grid [i+k,j].GetComponent<SpriteRenderer>().sprite) {
					same = false;
				}
			}
			k++;
		}
//		print ("Blocks = " + (k-1));
//		print ("same = " + same);
		// if same hasnt been changed to false the nwe have 4 in a row!
		if (same==true) {
			CenterPieces.GetComponent<sound>().chimePlay();
			print ("four in a row!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
			//score which is a script component of the score sounter holds the score all other collision scripts simply set / change the public score int on it
			ScoreCounter.GetComponent<score>().Score = ScoreCounter.GetComponent<score>().Score + ScoreInc;
			ScoreCounter.text = ScoreCounter.GetComponent<score>().Score.ToString().PadLeft(4,'0')+"\n-------\n"+ScoreCounter.GetComponent<score>().ScoreGoal;
			for(k=0;k<4;k++){
				Destroy(GridArray.grid[i+k,j].gameObject);
				GridArray.grid[i+k,j]=null;}
		}
	}
	
}
