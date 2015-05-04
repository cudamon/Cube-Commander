using UnityEngine;
using System.Collections;

public class GridArray : MonoBehaviour {
// THIS GRID STORES THE BLOCK THAT HAVE COLLIDED	
	public static int s = 10;
	public static Transform[,] grid = new Transform[2*s+1, 2*s+1];

	// Use this for initialization
	void Start () {
		print ("grid" + s);
		print ("Test");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}