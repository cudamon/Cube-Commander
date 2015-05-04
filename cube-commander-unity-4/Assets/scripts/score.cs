using UnityEngine;
using System.Collections;
//Remember to have UI library
using UnityEngine.UI;

public class score : MonoBehaviour {
	
	//The int value that holds the score
	public int Score;
	//The text field in the scene that displays the score
	public Text ScoreCounter;
	//The int value the holds the target score
	public int ScoreGoal;
	//The text field in the scene that displays the timer
	public Text Timer;
	//The float value indicating how much time is left
	public float timeLeft;



	// Use this for initialization
	void Start () {
		//Sets the score counter to defualt state once game starts
		ScoreCounter.text = "0000\n-------\n"+ScoreGoal.ToString().PadLeft(4,'0');
	}
	
	// Update is called once per frame
	void Update () {
		//Timer code
		timeLeft -= Time.deltaTime;
		//Show the time player has left
		if (timeLeft > 0)
		{
			//Timer.text =  Mathf.RoundToInt(timeLeft).ToString;
			int secs = Mathf.RoundToInt(timeLeft);
			//string minutes = Mathf.Floor(secs / 60).ToString("00");
			//string seconds = Mathf.Floor(secs % 60).ToString("00");
			Timer.text =  Mathf.Floor(secs / 60).ToString("00") + ":" + Mathf.Floor(secs % 60).ToString("00");
		}
		//Run stuff when timer runs out
		if(timeLeft < 0)
		{
			//GameOver
			Application.LoadLevel ("Heredity");
		}
	}
}
			// String.Format("It is now {0:d} at {0:t}", DateTime.Now);Mathf.RoundToInt().ToString()