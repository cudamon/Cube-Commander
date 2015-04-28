using UnityEngine;
using System.Collections;

public class CountdownTimer : MonoBehaviour {
	float timeRemaining = 60;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timeRemaining -= Time.deltaTime;
	
	}

	void OnGUI(){
		if(timeRemaining > 0){
			GUI.Label(new Rect(20, 20, 200, 100), "Timer : "+(int)timeRemaining);

		}
		else{
			GUI.Label(new Rect(20, 20, 100, 100), "Round Over");
		}
		
}
}
