using UnityEngine;
using System.Collections;

public class LoadGameScreen : MonoBehaviour {


	// Update is called once per frame
	public void GameSceneLoad (string sceneToChangeTo) {
		Application.LoadLevel (sceneToChangeTo); 

	
	}
	}

