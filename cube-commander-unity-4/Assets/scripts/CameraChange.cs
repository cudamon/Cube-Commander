using UnityEngine;
using System.Collections;

public class CameraChange : MonoBehaviour {
	public Camera camera;
	public Camera camera2;
	public Camera camera3;
	public Camera camera4;
	public Camera camera5;


	void Start() {
		camera.enabled = true;
		camera2.enabled = false;
		camera3.enabled = false;
		camera4.enabled = false;
		camera5.enabled = false;

	}
	
	void Update() {
		//This will toggle the enabled state of the two cameras between true and false each time
		int currentCameraVar = 0; 

			if (Input.GetKeyUp(KeyCode.Return)) {
				camera.enabled = !camera.enabled;
				camera2.enabled = !camera2.enabled;
			}

		}


}