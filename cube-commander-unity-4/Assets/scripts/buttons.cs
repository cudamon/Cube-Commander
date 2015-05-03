using UnityEngine;
using System.Collections;

public class buttons : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		MenuBox=GameObject.Find("MenuBox");
		MenuBox.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public GameObject MenuBox;

	//Remember to add scripts to EventSystem to be able to use them with buttons

	//Pause game when menu button is clicked
	public void MenuBtn() {
		MenuBox.SetActive(true);
		Time.timeScale = 0;
	}
	public void ResumeBtn() {
		MenuBox.SetActive(false);
		Time.timeScale = 1;
	}

	/*public void TouchRight() {
		transform.Rotate (0, 0, -spinspeed * Time.deltaTime);
	}
	
	public void TouchLeft() {
		transform.Rotate (0, 0, spinspeed * Time.deltaTime);
	}*/

	/* Yet to be implemented
	public void SaveBtn() {
	save game
	}*/

	public void QuitBtn() {
		Application.LoadLevel ("Openingscreen");
	}
}
