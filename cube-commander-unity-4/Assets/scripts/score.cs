using UnityEngine;
using System.Collections;
//Remember to have UI library
using UnityEngine.UI;

public class score : MonoBehaviour {

	public int Score;
	public Text ScoreCounter;
	public int ScoreGoal;

	// Use this for initialization
	void Start () {
		ScoreCounter.text = "0000\n-------\n"+ScoreGoal.ToString().PadLeft(4,'0');
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
