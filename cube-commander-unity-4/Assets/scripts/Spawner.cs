using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	
public Transform block;
public int SpawnTime;
public GameObject[] groups;
bool quickstart = true;
public float acceleration;
public bool anydirection;
int i = 0;

// Use this for initialization
void Start () {
}
	
	// Update is called once per frame
void Update () {

	i++;
	float x = 0.0F;
	float y = 0.0F;
//Only spawns a new block in every when i > SpawnTime. i Increases once every frame.
	if (i > SpawnTime || quickstart) {
		quickstart = false;
		// randomly choose one of the four spawn locations (right, left, top or bottom)
		int r = Random.Range (1, 5); // random never get the max number
		i = 0;
		if(anydirection)
		{
			if (r == 1){
				x = Random.Range (-13, 14); 
				y = 13.0F;
			}
			else if (r == 2)
			{
				x = Random.Range (-13, 14); 
				y = -13.0F;
			}
			else if (r == 3){
				x = -13.0F; 
				y = Random.Range (-13, 14);
			}
			else if (r == 4){
				x = 13.0F; 
				y = Random.Range (-13, 14);
			}
		}
		else{
			switch (r)
				{
				case 1:
					x=-13F;
					break;
				case 2:
					y=13F;
					break;
				case 3:
					x=13F;
					break;
				case 4:
					y=-13F;
					break;
				default:
					x=13F;
					y=13F;
					break;
				}
			}
			int j = Random.Range (0, groups.Length);
			//Spawn in a new block
			var obj = (GameObject)Instantiate (groups [j], new Vector2 (x,y), this.transform.rotation);
			obj.rigidbody2D.AddForce ((block.position - obj.transform.position) * acceleration);
			obj.name = groups [j].name;
		}
	}
}
