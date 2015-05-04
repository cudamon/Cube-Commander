using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Heredity : MonoBehaviour {
	public Sprite[] villageLvl;
	public GameObject[] Cultures;
//	public Text cultureText;

	private const int TYPES = 4, START_POP = 3, START_SPEC_POP = 0, START_LVL = 1, START_EXP = 0, RED = 0, YELLOW = 1, BLUE = 2, GREEN = 3;
//	private enum blockTypes {Red, Yellow, Blue, Green};
	private string[] block_types = {"Red", "Yellow", "Blue", "Green"};
	private int[] cultureLvlBreaks = {0, 300, 1000, 2500}; 
	private int[] population = new int[TYPES];
	private int[] specPop = new int[TYPES];
	private int[] cultureLvl = new int[TYPES];
	private int[] cultureExp = new int[TYPES];
	private int[] currentPop = new int[TYPES];
	private int totalPopulation, maxPopIndex, minLvlIndex;
	private double[,] heredity = { {100, 75, 25, 50}, {25, 100, 50, 75}, {75, 50, 100, 25}, {50, 25, 75, 100} };
//	private double[,] heredity = { {100, 75, 25}, {25, 100, 75}, {75, 25, 100}};	// To be used when changing to 3 TYPES
	private double[] specHeredity = {0, 0, 10, 20, 30};
	private int sacrifice, clickCount;
	private int randInt;
	private Text[] textValue;
	private Vector3 scale = new Vector3 (0.45f, 0.45f, 1f);
	private Stack sacrificeBlocks = new Stack();

	// Use this for initialization
	void Start () {
		GameObject canvas = GameObject.Find("Canvas");
		textValue = canvas.GetComponentsInChildren<Text> ();

		for(int index = 0; index < TYPES; index++)
		{
			population[index] = START_POP;
			specPop[index] = START_SPEC_POP;
			cultureLvl[index] = START_LVL;
			cultureExp[index] = START_EXP;
			totalPopulation += population[index];
		}
		
		SetCultureText();
		SetCultureSprite();
		sacrifice = TYPES;
	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetMouseButtonDown (0)) {	// For test purposes when pressing left mouse button
//			for (int i = 0; i < sacrifice; i++) {	// Remove the number of sacrifices needed from the 
//				maxPopIndex = 0;					// cultures with the largest population
//				GetCurrentPop();
//				for (int index = 0; index < TYPES; index++) {
//					if (currentPop[index] > currentPop[maxPopIndex]){
//						maxPopIndex = index;
//					}
//				}
//				if (population[maxPopIndex] < 1){
//					specPop[maxPopIndex]--;
//				} else {
//					population[maxPopIndex]--;
//				}
//				totalPopulation--;
//			}
//			Repopulate (Matchmaking ());	// Randomly match entire population then determine repopulation through heredity
//			SetCultureText();	// Update in game text display
//
////			clickCount++;
////			for (int i = 0; i < cultureLvlBreaks.Length; i++) {
////				if (clickCount > cultureLvlBreaks[i] / 100) {
////					sacrifice = TYPES + i;				
////				}
////			}
//		}
//
		if (Input.GetMouseButtonDown (1)) { // For test purposes when pressing right mouse button
			minLvlIndex = 0;
			for (int index = 0; index < TYPES; index++) {	// Check for culture with the lowest level
				if (cultureLvl[index] < cultureLvl[minLvlIndex]){
					minLvlIndex = index;
				}
			}
			cultureLvl[minLvlIndex]++;	// Increase culture's level
			SetCultureText();		// Update in game text display
			SetCultureSprite();	// Update in game visual display
			
		}
//		 Button presses to determine sacrifice blocks P.S. make an array to store these to give to the game
//		if (Input.GetButtonDown (Button_Red)) {
//			// If not enough blocks selected yet
//			if (sacrificeBlocks.Count < sacrifice) {
//				// Add block to list
//				if (population[RED] < 1){
//					population[RED]--;
//					totalPopulation--;
//					sacrificeBlocks.Push(RED);
//				}
//				//Update display of sacrifices
//
//			}
//		}
//
//		if (Input.GetButtonDown (Button_YELLOW)) {
//			// If not enough blocks selected yet
//			if (sacrificeBlocks.Count < sacrifice) {
//				// Add block to list
//				if (population[YELLOW] < 1){
//					population[YELLOW]--;
//					totalPopulation--;
//					sacrificeBlocks.Push(YELLOW);
//				}
//				//Update display of sacrifices
//
//			}
//		}
//
//		if (Input.GetButtonDown(Button_Sacrifices)){
//			if (sacrificeBlocks.Count > 0) {
//				switch (sacrificeBlocks.Pop()) {
//				case 0: population[RED]++;
//					break;
//				case 1: population[YELLOW]++;
//					break;
//				case 2: population[BLUE]++;
//					break;
//				case 3: population[GREEN]++;
//					break;
//				}
//				totalPopulation++;
//				//Update display of sacrifices
//			}
//		}
	}

	// On skill game completion
	void a(int score) {
		// Take score from game, take amounts for special use split (score gained through special blocks)

		// Devide score between cultures, extra for cultures of special block types that performed well
		for (int index = 0; index < TYPES; index++) {
//			cultureExp[index] -= cultureLvlBreaks[cultureLvl[index] - 1] / 10;	// Upkeep to maintain culture
			cultureExp[index] += score / TYPES;
			for (int level = 0; level < cultureLvlBreaks.Length; level++) {	// Determine culture levels/ visual display of progress (bricks beside culture sprites)
				if (cultureExp[index] > cultureLvlBreaks[level])  {
					cultureLvl[index] = level + 1;
				}
			}
		}
		// Randomly match entire population then determine repopulation through heredity
		Repopulate (Matchmaking ());

		// Update screen
		SetCultureText();
		SetCultureSprite();
	}

	// Update in game text display
	void SetCultureText () {
		for (int index = 0; index < TYPES; index ++) {
			textValue [index].text = block_types [index] + "\nPopulation:\t" + population [index] +
				"\nSpec Pop:\t" + specPop [index] + "\nLevel:\t" + cultureLvl [index];
		}
	}

	// Update in game visual display
	void SetCultureSprite () {
		for (int index = 0; index < TYPES; index ++) {
			Cultures [index].GetComponent<SpriteRenderer> ().sprite = villageLvl [cultureLvl [index] - 1];
//		switch (cultureLvl[index]) {
//		case 1: scale = new Vector3 (1, 1, 1f);
//			break;
//		case 2: scale = new Vector3 (0.6f, 0.6f, 1f);
//			break;
//		case 3: scale = new Vector3 (0.45f, 0.45f, 1f);
//			break;
//		}
			Cultures [index].transform.localScale = scale;
		}
	}

	// Randomly match entire population
	int[] Matchmaking () {
		bool aMatch = false;
		int[] matchTypes = new int[totalPopulation];
		int[] matched = new int[totalPopulation];
		GetCurrentPop();
		for (int i = 0; i < totalPopulation; i++){
			randInt = Random.Range(1, totalPopulation + 1);
			do {
//				Debug.Log("randInt is " + randInt);
				if (ifExists(matched, randInt, i)) {	// If member of population hasn't been matched yet
					randInt = Random.Range(1, totalPopulation + 1);
					aMatch = false;
				} else {
					if (randInt <= currentPop[RED]){	// Add member to match array
						matchTypes[i] = RED; 
					} else if (randInt <= (currentPop[RED] + currentPop[YELLOW])) {
						matchTypes[i] = YELLOW;
					} else if (randInt <= (currentPop[RED] + currentPop[YELLOW] + currentPop[BLUE])) {
						matchTypes[i] = BLUE;
					} else {
						matchTypes[i] = GREEN;
					}
					matched[i] = randInt;
					aMatch = true;
				}
			} while (!aMatch);
		}
				
		return matchTypes;	// Return list of matches
	}

	// Checks if member of population has been matched yet
	bool ifExists (int[] array, int value, int currentInt) {
		for (int index = 0; index <=  currentInt; index++){
			if (array[index] == randInt){
				return true;
			}
		}
		return false;
	}

	// Determine repopulation through heredity
	void Repopulate (int[] matchTypes) {
		int parent1, parent2;
		for (int index = 0; index < (matchTypes.Length / 2); index++){
			randInt = Random.Range(1, 101);
//			Debug.Log("parent1 is " + matchTypes[(index * 2)]);
			parent1 = matchTypes[(index * 2)];
//			Debug.Log("parent2 is " + matchTypes[(index * 2) + 1]);
			parent2 = matchTypes[((index * 2) + 1)];
//			for (int noOfNew = randInt % 3; noOfNew > 0; noOfNew--) {	//Determine how many new Blocks for the matched blocks
				if (randInt <= heredity[parent1, parent2]) {	// Determine new block type and if block is a special block
					if (SpecChance(parent1)) {
						specPop[parent1]++;
					} else {
						population[parent1]++;
					}
					totalPopulation++;
				} else {
					if (SpecChance(parent2)){
						specPop[parent2]++;
					} else {
						population[parent2]++;
					}
					totalPopulation++;
				}
				randInt = Random.Range(1, 101);
//			}
		}
	}

	// Determine if block is a special block
	bool SpecChance(int dominantParent){
		randInt = Random.Range(1, 101);
		if (randInt < specHeredity [cultureLvl [dominantParent] - 1]) {
			return true;
		}
		return false;
	}

	// Calculates current total population (normal and special simplified together for heredity)
	void GetCurrentPop () {
		for (int i = 0; i < TYPES; i++){
			currentPop[i] = population[i] + specPop[i];
		}
	}
}
