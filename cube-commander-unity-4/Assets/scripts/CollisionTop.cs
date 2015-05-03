using UnityEngine;
using System.Collections;
//Remember to have UI library
using UnityEngine.UI;

	public class CollisionTop : MonoBehaviour {

	public Transform block;
	public Transform CenterPieces;
	public Text ScoreCounter;
	public AudioClip chime;
	public int ScoreInc;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		print ("Collision Top");
		Destroy(col.gameObject);

		// calculate location of new block, adjusting for angle of centerpiece
		float degree = transform.eulerAngles[2] ;

		// add 90 degrees for this quadrant
		float angle = (degree+90) * Mathf.PI / 180F;
		print ("Angle = " + degree + " degrees " + angle + " radians");

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
		int yd = -Mathf.RoundToInt(xx * Mathf.Cos (delta) - yy * Mathf.Sin (delta));
		int xd = Mathf.RoundToInt(yy * Mathf.Cos (delta) + xx * Mathf.Sin (delta));
		print ("rotated x = " + xx + " y = " + yy);
		print ("derotated x = " + xd + " y = " + yd);
		
		GridArray.grid [xd + GridArray.s, yd + GridArray.s] = objcol;
		// check if 4 in a row - turn code into a method, pass location xd,yd, return boolean true false
		// check up -
		bool same = true;
		int i = xd + GridArray.s;
		int j = yd + GridArray.s;
		print ("i,j=" + i + ","+j);
		int k = 1;
		print("sprite  0:"+GridArray.grid [i,j].GetComponent<SpriteRenderer>().sprite);
		while (same==true & k<4) {
			if(GridArray.grid [i,j-k] == null) {
				print("null "+k);
				print ("i,j+k=" + i + ","+(j-k));
				same = false;
			}
			else {
				print ("i,j+k=" + i + ","+(j-k));
				print("sprite  "+k+":"+GridArray.grid [i,j-k].GetComponent<SpriteRenderer>().sprite);
				if(GridArray.grid [i,j].GetComponent<SpriteRenderer>().sprite != GridArray.grid [i,j-k].GetComponent<SpriteRenderer>().sprite) {
					same = false;
				}
			}
			k++;
		}
		print ("Blocks = " + (k-1));
		print ("same = " + same);
		if (same) {
			CenterPieces.GetComponent<sound>().chimePlay();
			print ("four in a row!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
			//score which is a script component of the score sounter holds the score all other collision scripts simply set / change the public score int on it
			ScoreCounter.GetComponent<score>().Score = ScoreCounter.GetComponent<score>().Score + ScoreInc;
			ScoreCounter.text = ScoreCounter.GetComponent<score>().Score.ToString().PadLeft(4,'0')+"\n-------\n"+ScoreCounter.GetComponent<score>().ScoreGoal;
			for(k=0;k<4;k++){
				Destroy(GridArray.grid[i,j-k].gameObject);
				GridArray.grid[i,j-k]=null;
			}
		}
	}
}
