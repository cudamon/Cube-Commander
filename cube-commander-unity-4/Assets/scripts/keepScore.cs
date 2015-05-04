using UnityEngine;
using System.Collections;

public class keepScore : MonoBehaviour {
	public static keepScore i;
	//### The gameobject that this script is attatched to is used to keep the score values between scenes ####//

	//The int value that holds the score
	public int Score;
	//The int value the holds the target score
	public int ScoreGoal;
	//The float value indicating how much time is left
	public float timeLeft;


	//This keeps the gameobject around when the scene changes
	void Awake () {
		if(!i) {
			i = this;
			DontDestroyOnLoad(gameObject);
		}else 
			Destroy(gameObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
