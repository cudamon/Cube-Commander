using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Heredity : MonoBehaviour {
	public Sprite[] VillageLvl;
	public GameObject[] Cultures;
//	public Text cultureText;

	private const int TYPES = 4, START_POP = 3, START_SPEC_POP = 0, START_LVL = 1, RED = 0, YELLOW = 1, BLUE = 2, GREEN = 3;
//	private enum blockTypes {Red, Yellow, Blue, Green};
	private string[] block_types = {"Red", "Yellow", "Blue", "Green"};
	private int[] population = new int[4];
	private int[] specPop = new int[4];
	private int[] cultureLvl = new int[4];
	private int[] currentPop = new int[4];
	private int totalPopulation, maxPopIndex, minLvlIndex;
	private double[,] heredity = { {100, 75, 25, 50}, {25, 100, 50, 75}, {75, 50, 100, 25}, {50, 25, 75, 100} };
	private double[] specHeredity = {0, 0, 10, 20, 30};
	private int randInt;
	private Text[] textValue;

	// Use this for initialization
	void Start () {
		GameObject canvas = GameObject.Find("Canvas");
		textValue = canvas.GetComponentsInChildren<Text> ();

		for(int index = 0; index < TYPES; index++)
		{
			population[index] = START_POP;
			specPop[index] = START_SPEC_POP;
			cultureLvl[index] = START_LVL;
			totalPopulation += population[index];
			SetCultureText(index);
//			Cultures[index].GetComponent<SpriteRenderer>().sprite = VillageLvl[cultureLvl[index]];
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			for (int i = 0; i < 4; i++) {
				maxPopIndex = 0;
				GetCurrentPop();
				for (int index = 0; index < TYPES; index++) {
					if (currentPop[index] > currentPop[maxPopIndex]){
						maxPopIndex = index;
					}
				}
				if (population[maxPopIndex] < 1){
					specPop[maxPopIndex]--;
				} else {
					population[maxPopIndex]--;
				}
				totalPopulation--;
			}
			Repopulate (Matchmaking ());
			for(int index = 0; index < TYPES; index++)
			{
				SetCultureText(index);
			}
		}

		if (Input.GetMouseButtonDown (1)) {
			minLvlIndex = 0;
			for (int index = 0; index < TYPES; index++) {
				if (cultureLvl[index] < cultureLvl[minLvlIndex]){
					minLvlIndex = index;
				}
			}
			cultureLvl[minLvlIndex]++;
			for(int index = 0; index < TYPES; index++)
			{
				SetCultureText(index);
//				if (!VillageLvl[index].Equals(null)){
//					Cultures[index].GetComponent<SpriteRenderer>().sprite = VillageLvl[cultureLvl[index]];
//				}
			}
		}
	}

	void SetCultureText (int index) {
		textValue [index].text = block_types [index] + "\nPopulation:\t" + population[index] +
			"\nSpec Pop:\t" + specPop[index] + "\nLevel:\t" + cultureLvl[index];
	}

	int[] Matchmaking () {
		bool aMatch = false;
		int[] matchTypes = new int[totalPopulation];
		int[] matched = new int[totalPopulation];
		GetCurrentPop();
		for (int i = 0; i < totalPopulation; i++){
			randInt = Random.Range(1, totalPopulation + 1);
			do {
//				Debug.Log("randInt is " + randInt);
				if (ifExists(matched, randInt, i)) {
					randInt = Random.Range(1, totalPopulation + 1);
					aMatch = false;
				} else {
					if (randInt <= currentPop[RED]){
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
				
		return matchTypes;
	}

	bool ifExists (int[] array, int value, int currentInt) {
		for (int index = 0; index <=  currentInt; index++){
			if (array[index] == randInt){
				return true;
			}
		}
		return false;
	}

	void Repopulate (int[] matchTypes) {
		int parent1, parent2;
		for (int index = 0; index < (matchTypes.Length / 2); index++){
			randInt = Random.Range(1, 101);
//			Debug.Log("parent1 is " + matchTypes[(index * 2)]);
			parent1 = matchTypes[(index * 2)];
//			Debug.Log("parent2 is " + matchTypes[(index * 2) + 1]);
			parent2 = matchTypes[((index * 2) + 1)];
			if (randInt <= heredity[parent1, parent2]) {
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
		}
	}

	bool SpecChance(int dominantParent){
		randInt = Random.Range(1, 101);
		if (randInt < specHeredity [cultureLvl [dominantParent] - 1]) {
			return true;
		}
		return false;
	}

	void GetCurrentPop () {
		for (int i = 0; i < TYPES; i++){
			currentPop[i] = population[i] + specPop[i];
		}
	}
}
