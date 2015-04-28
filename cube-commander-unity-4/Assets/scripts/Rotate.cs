using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	public Transform CenterPieces;
	// Use this for initialization
	void Start () {
	}

	int spinspeed = 200;

	// Update is called once per frame
	void Update () {

		if (Time.timeScale != 0){

		}

		if (Input.GetKey ("a")) {
			//print ("LEFT");
			//print ("center rotation = " + CenterPieces.transform.eulerAngles);
			transform.Rotate (0, 0, spinspeed * Time.deltaTime);
		}

		if (Input.GetKey ("d")) {
			//print ("RIGHT");
			//print ("center rotation = " + CenterPieces.transform.eulerAngles);
			transform.Rotate (0, 0, -spinspeed * Time.deltaTime);
		}



		/*if (Input.GetKey ("/") && CenterPieces.transform.eulerAngles == 90) {
			//print ("RIGHT");
			//print ("center rotation = " + CenterPieces.transform.eulerAngles);
			transform.Rotate (0, 0, 1);
		}*/
	
	}
}
