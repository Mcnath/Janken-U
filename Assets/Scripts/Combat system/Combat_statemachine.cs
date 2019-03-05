using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat_statemachine : MonoBehaviour {

	//Initialized players(may not need)
	//public Player_base player;
	//public Enemy_base AI_1;
	//public Enemy_base AI_2;
	//public Enemy_base AI_3;

	//variable for timers
	private int seconds_current = 0;
	private int seconds_max = 60;

	//battle turn state
	public enum turnState{
		START,
		PLAYERCHOICE,
		ENEMYCHOICE,
		LOSE,
		WIN
	}
	public turnState currentState;

	//List for detecting players exists
	public List<HandleTurn> PerformList = new List<HandleTurn> ();
	public List<GameObject> PlayerInBattle = new List<GameObject>();

	//initialize player input
	public enum PlayerGUI{ ACTIVATE, WAITING, INPUT1, INPUT2, DONE}
	public PlayerGUI playerInput;
	private HandleTurn playerChoice;

	//initialization of state
	void Start () {
		currentState = turnState.START;
		PlayerInBattle.AddRange (GameObject.FindGameObjectsWithTag("Player"));
		PlayerInBattle.AddRange (GameObject.FindGameObjectsWithTag("AI"));
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (currentState);
		switch(currentState){
		case(turnState.START):
			break;
		case(turnState.PLAYERCHOICE):

			break;
		case(turnState.ENEMYCHOICE):
			
			break;
		case(turnState.LOSE):
			break;
		case(turnState.WIN):
			break;
		}
	}

	void timerPlayer(){
		//counting down the time and force skip player's turn if no action taken after time limit
	}

	public void CollectActions(HandleTurn input){
		PerformList.Add (input); // recorded actions chosen by each players
	}
}

