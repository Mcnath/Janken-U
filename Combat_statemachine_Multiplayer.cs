using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat_statemachine : MonoBehaviour {

	//Initialized players(may not need)
	//public Player_base player;


	//variable for timers
	private int seconds_current = 0;
	private int seconds_max = 60;

	//battle turn state
	public enum turnState{
		START,
		PLAYERCHOICE,
		ENEMYCHOICE,
		ACTION
	}
	public turnState currentState;

	//List for storing existing players
	public List<HandleTurn> PerformList = new List<HandleTurn> ();
	public List<GameObject> PlayerInBattle = new List<GameObject>();

	//initialize player input
	public enum PlayerGUI{ ACTIVATE, INPUT, DONE}
	public PlayerGUI playerInput;
	public HandleTurn playerChoice;
	//public List<GameObject> PlayerToManage = new List<GameObject>;
	public GameObject AttackPanel;
	public GameObject EnemySelect;

	//initialization of state
	void Start () {
		currentState = turnState.START;
		playerInput = PlayerGUI.ACTIVATE;
		PlayerInBattle.AddRange (GameObject.FindGameObjectsWithTag("Player"));
		PlayerInBattle.AddRange (GameObject.FindGameObjectsWithTag("AI"));
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (currentState);
		//how the battle is progressed each turn
		switch(currentState){
		case(turnState.START):
				//DelayedAttribute (100);// Replace with transition animation
				currentState = turnState.PLAYERCHOICE;
			break;
		case(turnState.PLAYERCHOICE):
			if (playerInput == PlayerGUI.DONE) {currentState = turnState.ENEMYCHOICE;}
			break;
		case(turnState.ENEMYCHOICE):
			currentState = turnState.ACTION;
			break;
		case(turnState.ACTION):
			//put in the logic here
			if (playerChoice.AttackType == HandleTurn.janken.ROCK) {
				for (int i = 1; i < 3; i++) {
					if (PerformList [i].AttackType == HandleTurn.janken.SCISSORS) {
						//win
					} else if (PerformList [i].AttackType == HandleTurn.janken.PAPER) {
						//lose
					} else { 
						//draw
					}	
				}
			}
			if (playerChoice.AttackType == HandleTurn.janken.PAPER) {
				for (int i = 1; i < 3; i++) {
					if (PerformList [i].AttackType == HandleTurn.janken.ROCK) {
						//win
					} else if (PerformList [i].AttackType == HandleTurn.janken.SCISSORS) {
						//lose
					} else { 
						//draw
					}	
				}
			}
			if (playerChoice.AttackType == HandleTurn.janken.SCISSORS) {
				for (int i = 1; i < 3; i++) {
					if (PerformList [i].AttackType == HandleTurn.janken.PAPER) {
						//win
					} else if (PerformList [i].AttackType == HandleTurn.janken.ROCK) {
						//lose
					} else { 
						//draw
					}	
				}
			}
			//DelayedAttribute (100);// R					eplace with transition animation
			currentState = turnState.START;
			break;
		}

		//how the player turn is progressed
		switch (playerInput){
		case(PlayerGUI.ACTIVATE):
			//DelayedAttribute (100);// Replace with transition animation
			playerInput = PlayerGUI.INPUT;
			break;
		case(PlayerGUI.INPUT):
			break;
		case(PlayerGUI.DONE):
			if(currentState == turnState.PLAYERCHOICE){playerInput = PlayerGUI.ACTIVATE;}
			break;
		}
	}

	void timerPlayer(){
		//counting down the time and force skip player's turn if no action taken after time limit
	}

	public void CollectActions(HandleTurn input){
		PerformList.Add (input); // recorded actions chosen by each players
	}

	void enemyButton(){
		foreach (GameObject AI in PlayerInBattle) {
			//GameObject newButton = Instantiate (enemyButton) as GameObject;
			//EnemySelectButton button = newButton.GetComponent<EnemySelectButton>();

			//Emeny_AIstatemachine cur_enemy = AI.GetComponents<Emeny_AIstatemachine>();
			//Text buttonText = newButton.transform.Findchild ("Text").gameObject.GetComponents<Text> ();
			//buttonText = cur_enemy.enemy.name;
			//button.EnemyPrefab = enemy;
		}
	}


}

