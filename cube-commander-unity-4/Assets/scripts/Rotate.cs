using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	public Transform CenterPieces;
	// Use this for initialization
	void Start () {
	}

	int spinspeed = 200;

	public bool rotateLeft = false;
	public bool rotateRight= false;

	// Update is called once per frame
	void Update () {

		foreach (Touch touch in Input.touches) {
			if (touch.position.x < Screen.width/2) {
				if (touch.phase == TouchPhase.Began){
					rotateLeft = true;
				}
				else if (touch.phase == TouchPhase.Ended){
					rotateLeft = false;
				}
			}
			else if (touch.position.x > Screen.width/2) {
					if (touch.phase == TouchPhase.Began){
						rotateRight = true;
					}
					else if (touch.phase == TouchPhase.Ended){
						rotateRight = false;
					}
				}
			}

		if (rotateLeft) {
			transform.Rotate (0, 0, spinspeed * Time.deltaTime);
		}
		if (rotateRight) {
			transform.Rotate (0, 0, -spinspeed * Time.deltaTime);
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
