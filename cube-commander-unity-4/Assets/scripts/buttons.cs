using UnityEngine;
using System.Collections;

public class buttons : MonoBehaviour {

	// Use this for initialization
	void Start () {
		MenuBox=GameObject.Find("MenuBox");
		MenuBox.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public GameObject MenuBox;

	//Remember to add scripts to EventSystem to be able to use them with buttons
	public void MenuBtn() {
		MenuBox.SetActive(true);
		Time.timeScale = 0;
	}
	public void ResumeeBtn() {
		MenuBox.SetActive(false);
		Time.timeScale = 1;
	}
	/* Yet to be implemented
	public void SaveBtn() {
	save game
	}*/

	public void QuitBtn() {
		Application.LoadLevel ("Openingscreen");
	}
}
