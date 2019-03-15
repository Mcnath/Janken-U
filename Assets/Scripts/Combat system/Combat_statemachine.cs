using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat_statemachine : MonoBehaviour {

	//Initialized players(may not need)
	public Player_base player;
	private Player_statemachine PSM;
	private Emeny_AIstatemachine ESM; 
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
	//public List<GameObject> EnemyInBattle = new List<GameObject>();

	//initialize player input
	public enum PlayerGUI{ ACTIVATE, INPUT, TARGET, DONE}
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
		//AttackPanel.SetActive (false);
		//EnemySelect.SetActive (false);
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
			battleLogic();
			//DelayedAttribute (100);// Replace with transition animation
			currentState = turnState.START;
			break;
		}

		//how the player turn is progressed
		switch (playerInput){
		case(PlayerGUI.ACTIVATE):
			//DelayedAttribute (100);// Replace with transition animation
			//playerChoice = new HandleTurn();
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
		PerformList.Add (input); // recorded actions chosen by enemy
	}

	public void actionChosen(){ // update the chosen type of action
		playerChoice.Attacker = PlayerInBattle[0].name;
		playerChoice.AttackGameObject = PlayerInBattle [0];


		AttackPanel.SetActive (true);
		EnemySelect.SetActive (true);
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
		playerChoice.AttackTarget = 
	}
	public void battleLogic(){
		for (int i = 0; i < PlayerInBattle.Count; i++) {
			// logic for the player 
			if(PSM.pLHS != Player_statemachine.leftHand_state.INACTIVE && PSM.pRHS != Player_statemachine.rightHand_state.INACTIVE){
				//Left Hand
				if (playerChoice.LeftAttackType == HandleTurn.janken.ROCK) {
					if (PerformList [i].RightAttackType == HandleTurn.janken.SCISSORS) {
						//win
						ESM.eRHS = Emeny_AIstatemachine.rightHand_state.INACTIVE;
					} else if (PerformList [i].RightAttackType == HandleTurn.janken.PAPER) {
						//lose
						PSM.pLHS = Player_statemachine.leftHand_state.INACTIVE;
					} else { 
						//draw
					}
				}
				else if (playerChoice.LeftAttackType == HandleTurn.janken.PAPER) {
						if (PerformList [i].RightAttackType == HandleTurn.janken.ROCK) {
							//win
							ESM.eRHS = Emeny_AIstatemachine.rightHand_state.INACTIVE;
						} else if (PerformList [i].RightAttackType == HandleTurn.janken.SCISSORS) {
							//lose
							PSM.pLHS = Player_statemachine.leftHand_state.INACTIVE;
						} else { 
							//draw
						}	
				}
				else if (playerChoice.LeftAttackType == HandleTurn.janken.SCISSORS) {
						if (PerformList [i].RightAttackType == HandleTurn.janken.PAPER) {
							//win
							ESM.eRHS = Emeny_AIstatemachine.rightHand_state.INACTIVE;
						} else if (PerformList [i].RightAttackType == HandleTurn.janken.ROCK) {
							//lose
							PSM.pLHS = Player_statemachine.leftHand_state.INACTIVE;
						} else { 
							//draw
						}	
					//right hand
					if (playerChoice.RightAttackType == HandleTurn.janken.ROCK) {
						if (PerformList [i].LeftAttackType == HandleTurn.janken.SCISSORS) {
							//win
							ESM.eLHS = Emeny_AIstatemachine.leftHand_state.INACTIVE;
						} else if (PerformList [i].LeftAttackType == HandleTurn.janken.PAPER) {
							//lose
							PSM.pRHS = Player_statemachine.rightHand_state.INACTIVE;
						} else { 
							//draw
						}
					}
					else if (playerChoice.RightAttackType == HandleTurn.janken.PAPER) {
						if (PerformList [i].LeftAttackType == HandleTurn.janken.ROCK) {
							//win
							ESM.eLHS = Emeny_AIstatemachine.leftHand_state.INACTIVE;
						} else if (PerformList [i].LeftAttackType == HandleTurn.janken.SCISSORS) {
							//lose
							PSM.pRHS = Player_statemachine.rightHand_state.INACTIVE;
						} else { 
							//draw
						}	
					}
					else if (playerChoice.RightAttackType == HandleTurn.janken.SCISSORS) {
						if (PerformList [i].LeftAttackType == HandleTurn.janken.PAPER) {
							//win
							ESM.eLHS = Emeny_AIstatemachine.leftHand_state.INACTIVE;
						} else if (PerformList [i].LeftAttackType == HandleTurn.janken.ROCK) {
							//lose
							PSM.pRHS = Player_statemachine.rightHand_state.INACTIVE;
						} else { 
							//draw
						}	
				}
			}
			else{//player lose
			}

			// for enemy target enemy
		}
	}
 }
}

