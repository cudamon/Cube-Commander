using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public Transform blockfall;

	public int SpawnTime;

	public GameObject[] groups;

	int i = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		//Only spawns a new block in every when i > SpawnTime. i Increases once every frame.
		i++;
		if( i > SpawnTime){

			i = 0;

			int j = Random.Range(0, groups.Length);
			//Spawn in a new block
			var obj=Instantiate(groups[j], new Vector2(this.transform.position.x,this.transform.position.y), blockfall.transform.rotation);

			// Find block and set parent to Centerpieces object
			print (groups[j] + "(Clone)");

			//GameObject block2 = GameObject.Find(groups[j] + "(Clone)");

			//print ("block2 Spawner - " + block2);

			obj.name = groups[j].name;
		}
	}
}
