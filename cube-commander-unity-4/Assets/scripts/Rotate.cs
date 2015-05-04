using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	public Transform CenterPieces;
	// Use this for initialization
	void Start () {
	}

	//rate at which planet spins at
	public int spinspeed;
	//rate at which planet slows down at
	public int slowdownrate;

	//used for touch input on mlbile device
	public bool rotateLeft = false;
	public bool rotateRight= false;

	// Update is called once per frame
	void Update () {

		//#### MOBILE CONTROLS ####///

		foreach (Touch touch in Input.touches) {

			//Rotate planet left if left side of screen is touched
			if (touch.position.x < Screen.width/2) {
				if (touch.phase == TouchPhase.Began){
					rotateLeft = true;
				}
				else if (touch.phase == TouchPhase.Ended){
					rotateLeft = false;
				}
			}

			//Rotate planet left if left side of screen is touched
			else if (touch.position.x > Screen.width/2) {
					if (touch.phase == TouchPhase.Began){
						rotateRight = true;
					}
					else if (touch.phase == TouchPhase.Ended){
						rotateRight = false;
					}
				}
			}

		//#### PC CONTROLS & touch execution####///

		//Rotates center piece left
		if (Input.GetKey ("a") || rotateLeft) {
			rigidbody2D.AddTorque (spinspeed);
		} else if (rigidbody2D.angularVelocity > 0) {
			rigidbody2D.angularVelocity = Mathf.Max(0, rigidbody2D.angularVelocity - slowdownrate);
		}

		//Rotates center piece right
		if (Input.GetKey ("d") || rotateRight) {
			rigidbody2D.AddTorque (-spinspeed);
		} else if (rigidbody2D.angularVelocity < 0) {
			print ("angular velocity = " +  rigidbody2D.angularVelocity);
			rigidbody2D.angularVelocity = Mathf.Min(0, rigidbody2D.angularVelocity + slowdownrate);
		}
	
	}
}
