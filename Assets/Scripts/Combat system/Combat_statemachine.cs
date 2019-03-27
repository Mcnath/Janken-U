using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Combat_statemachine : MonoBehaviour {

	//Initialized players(may not need)
	public Player_statemachine PSM;
	public Emeny_AIstatemachine ESM1, ESM2, ESM3;
	private HandleTurn HT;
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

	//List for storing existing player's information
	public List<HandleTurn> PerformList = new List<HandleTurn> ();// collect all the actions via HandleTurn Class
	public List<GameObject> PlayerInBattle = new List<GameObject>();// collect all the player existed in the field


	//initialize player input
	public enum PlayerGUI{ ACTIVATE, INPUT, DONE}
	public PlayerGUI playerInput;
	public HandleTurn playerChoice;
	//public List<GameObject> PlayerToManage = new List<GameObject>

	//initialize attack choice here


	//initialization of state
	void Start () {
		currentState = turnState.START;
		playerInput = PlayerGUI.ACTIVATE;
		PlayerInBattle.AddRange (GameObject.FindGameObjectsWithTag("Player"));
		PlayerInBattle.AddRange (GameObject.FindGameObjectsWithTag("AI"));
	}
	
	// Update is called once per frame
	void Update () {
		//how the battle is progressed each turn
		//Debug.Log ("currentState: "+ currentState);
		switch(currentState){
		case(turnState.START):
			//PerformList = new List<HandleTurn> ();
			playerChoice = new HandleTurn ();
			currentState = turnState.PLAYERCHOICE;
			break;
		case(turnState.PLAYERCHOICE):
			if (playerInput == PlayerGUI.DONE) {currentState = turnState.ENEMYCHOICE;}
			break;
		case(turnState.ENEMYCHOICE):
			if (PerformList.Count == PlayerInBattle.Count) {currentState = turnState.ACTION;}
			break;
		case(turnState.ACTION):
			//put in the logic here
			battleLogic ();
			// Replace with transition animation
			currentState = turnState.START;
			break;
		}

		//how the player turn is progressed
		//Debug.Log ("playerInput: "+ playerInput);
		switch (playerInput){
		case(PlayerGUI.ACTIVATE):
			playerInput = PlayerGUI.INPUT;
			break;
		case(PlayerGUI.INPUT):
			if (playerChoice.AttackTarget != null) {
				playerInput = PlayerGUI.DONE;
			}
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
			PerformList.Add (input); // recorded actions chosen by enemy
	}

	public void chooseRockLeft(){
		Debug.Log ("Chosen Rock on the left");
		playerChoice.LeftAttackType = HandleTurn.janken.ROCK;
	}

	public void chooseScissorsLeft(){
		Debug.Log ("Chosen Scissors on the left");
		playerChoice.LeftAttackType = HandleTurn.janken.SCISSORS;
	}

	public void choosePaperLeft(){
		Debug.Log ("Chosen Paper on the left");
		playerChoice.LeftAttackType = HandleTurn.janken.PAPER;
	}

	public void chooseRockRight(){
		Debug.Log ("Chosen Rock on the right");
		playerChoice.RightAttackType = HandleTurn.janken.ROCK;
	}

	public void chooseScissorsRight(){
		Debug.Log ("Chosen Scissors on the right");
		playerChoice.RightAttackType = HandleTurn.janken.SCISSORS;
	}

	public void choosePaperRight(){
		Debug.Log ("Chosen Paper on the right");
		playerChoice.RightAttackType = HandleTurn.janken.PAPER;
	}

	public void enemySelected(){
		// update player choice of Attack target
		playerChoice.AttackTarget = this.gameObject;
	}
	public void battleLogic(){
		Debug.Log ("Battle Start");
		for (int i = 0; i < PerformList.Count; i++){
			for(int j = 0; j < 2; j++){
				if(PerformList[j].AttackTarget == PerformList[i].AttackGameObject){
					int resultLeft =  howToWin(PerformList[j].LeftAttackType, PerformList[i].RightAttackType);
					int resultRight =  howToWin(PerformList[j].RightAttackType, PerformList[i].LeftAttackType);
					if (resultLeft == 1) {
						Debug.Log ( PerformList[j].Attacker + " win, " + PerformList[i].Attacker + " lose");
						if(PerformList[i].AttackGameObject == GameObject.FindWithTag("Player")){
							PerformList [i].AttackGameObject.GetComponent<Player_statemachine> ().player.RightHand_state = false;
						}
						else
						{
							PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state = false;
						}
					} else {
						Debug.Log (PerformList[i].Attacker + " win, " + PerformList[j].Attacker + " lose");
						if (PerformList[j].AttackGameObject == GameObject.FindWithTag("Player"))
						{
							PerformList[j].AttackGameObject.GetComponent<Player_statemachine>().player.LeftHand_state = false;
						}
						else
						{
							PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.LeftHand_state = false;
						}
					}
					if (resultRight == 1) {
						Debug.Log (PerformList[j].Attacker + " win, " + PerformList[i].Attacker + " lose");
						if (PerformList[i].AttackGameObject == GameObject.FindWithTag("Player"))
						{
							PerformList[i].AttackGameObject.GetComponent<Player_statemachine>().player.LeftHand_state = false;
						}
						else
						{
							PerformList[i].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.LeftHand_state = false;
						}
					} else {
						Debug.Log (PerformList[i].Attacker + " win, " + PerformList[j].Attacker + " lose");
						if (PerformList[j].AttackGameObject == GameObject.FindWithTag("Player"))
						{
							PerformList[j].AttackGameObject.GetComponent<Player_statemachine>().player.RightHand_state = false;
						}
						else
						{
							PerformList[j].AttackGameObject.GetComponent<Emeny_AIstatemachine>().enemy.RightHand_state = false;
						}
					}
				}
			}
		}
		Debug.Log ("Battle End");
		}

	public static int howToWin(HandleTurn.janken source, HandleTurn.janken target){
		if (source == HandleTurn.janken.PAPER && target == HandleTurn.janken.ROCK) {
			return 1;
		}
		else if (source == HandleTurn.janken.ROCK && target == HandleTurn.janken.SCISSORS) {
			return 1;
		}
		else if (source == HandleTurn.janken.SCISSORS && target == HandleTurn.janken.PAPER) {
			return 1;
		}
		return 0;
	}
	}