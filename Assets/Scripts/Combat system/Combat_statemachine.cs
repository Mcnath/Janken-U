using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
	//intialize barrier to limit the action perturn to 4
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
	public Queue<string> LeftAttackQueue = new Queue<string> ();
	public Queue<string> RightAttackQueue = new Queue<string> ();
	public List<GameObject> PlayerInBattle = new List<GameObject>();
	//public List<GameObject> EnemyInBattle = new List<GameObject>();

	//initialize player input
	public enum PlayerGUI{ ACTIVATE, INPUT, TARGET, DONE}
	public PlayerGUI playerInput;
	public HandleTurn playerChoice;
	//public List<GameObject> PlayerToManage = new List<GameObject>;
	public GameObject AttackPanel;
	//public GameObject EnemySelect;

	//initialize attack choice here


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
		//Debug.Log (currentState);
		//how the battle is progressed each turn
		switch(currentState){
		case(turnState.START):
			//DelayedAttribute (100);// Replace with transition animation
			Globals.executeAction = new Semaphore (0, PlayerInBattle.Count);
			PerformList = new List<HandleTurn>();	
			currentState = turnState.PLAYERCHOICE;
			break;
		case(turnState.PLAYERCHOICE):
			Thread p = new Thread (new ThreadStart (PSM.readyToBattle));
			p.Start ();
			p.Join ();
			if (playerInput == PlayerGUI.DONE) {currentState = turnState.ENEMYCHOICE;}
			break;
		case(turnState.ENEMYCHOICE):
			for (int i = 0; i < PlayerInBattle.Count - 1; i++) {
				Thread e = new Thread (new ThreadStart (ESM.chooseAction));
				e.Start ();
			}
			if (PerformList.Count == PlayerInBattle.Count) {currentState = turnState.ACTION;}
			break;
		case(turnState.ACTION):
			//put in the logic here
			battleLogic ();
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
		//EnemySelect.SetActive (true);
	}

	public void chooseRockLeft(){
		Debug.Log ("Chosen Rock on the left");
		LeftAttackQueue.Enqueue(playerChoice.LeftAttackType = playerChoice.janken[0]);
	}

	public void chooseScissorsLeft(){
		Debug.Log ("Chosen Scissors on the left");
		LeftAttackQueue.Enqueue(playerChoice.LeftAttackType = playerChoice.janken[2]);
	}

	public void choosePaperLeft(){
		Debug.Log ("Chosen Paper on the left");
		LeftAttackQueue.Enqueue(playerChoice.LeftAttackType = playerChoice.janken[1]);
	}

	public void chooseRockRight(){
		Debug.Log ("Chosen Rock on the right");
		RightAttackQueue.Enqueue(playerChoice.RightAttackType = playerChoice.janken[0]);
	}

	public void chooseScissorsRight(){
		Debug.Log ("Chosen Scissors on the right");
		RightAttackQueue.Enqueue(playerChoice.RightAttackType = playerChoice.janken[2]);
	}

	public void choosePaperRight(){
		Debug.Log ("Chosen Paper on the right");
		RightAttackQueue.Enqueue(playerChoice.RightAttackType = playerChoice.janken[1]);
	}

	public void enemySelected(){
		// update player choice of Attack target
		playerChoice.AttackTarget = this.gameObject;
	}
	public void battleLogic(){
		for (int i = 0; i < PlayerInBattle.Count; i++) {
			// logic for the player 
			if(PSM.pLHS != Player_statemachine.leftHand_state.INACTIVE && PSM.pRHS != Player_statemachine.rightHand_state.INACTIVE){
				//Left Hand
				if (playerChoice.LeftAttackType == playerChoice.janken[0]) {
					if (PSM.rightIncoming.Peek() == playerChoice.janken[2]) {
						//win
						ESM.eRHS = Emeny_AIstatemachine.rightHand_state.INACTIVE;
						PSM.rightIncoming.Dequeue ();
					} else if (PSM.rightIncoming.Peek() == playerChoice.janken[1] || PSM.rightIncoming.Peek() == playerChoice.janken[0]) {
						//lose
						PSM.pLHS = Player_statemachine.leftHand_state.INACTIVE;
						PSM.rightIncoming.Dequeue ();
					}
				} else if (playerChoice.LeftAttackType == playerChoice.janken[1]) {
					if (PSM.rightIncoming.Peek() == playerChoice.janken[0]) {
						//win
						ESM.eRHS = Emeny_AIstatemachine.rightHand_state.INACTIVE;
						PSM.rightIncoming.Dequeue ();
					} else if (PSM.rightIncoming.Peek() == playerChoice.janken[2] || PSM.rightIncoming.Peek() == playerChoice.janken[1]) {
						//lose
						PSM.pLHS = Player_statemachine.leftHand_state.INACTIVE;
						PSM.rightIncoming.Dequeue ();
					}	
				} else if (playerChoice.LeftAttackType == playerChoice.janken[2]) {
					if (PSM.rightIncoming.Peek() == playerChoice.janken[1]) {
						//win
						ESM.eRHS = Emeny_AIstatemachine.rightHand_state.INACTIVE;
						PSM.rightIncoming.Dequeue ();
					} else if (PSM.rightIncoming.Peek() ==playerChoice.janken[0]||PSM.rightIncoming.Peek() == playerChoice.janken[2]) {
						//lose
						PSM.pLHS = Player_statemachine.leftHand_state.INACTIVE;
						PSM.rightIncoming.Dequeue ();
					}
				}
					//right hand
				if (playerChoice.RightAttackType == playerChoice.janken[0]) {
					if (PSM.rightIncoming.Peek() == playerChoice.janken[2]) {
							//win
						ESM.eLHS = Emeny_AIstatemachine.leftHand_state.INACTIVE;
						PSM.rightIncoming.Dequeue ();
					} else if (PSM.rightIncoming.Peek() == playerChoice.janken[1]||PSM.rightIncoming.Peek() == playerChoice.janken[0]) {
							//lose
						PSM.pRHS = Player_statemachine.rightHand_state.INACTIVE;
						PSM.rightIncoming.Dequeue ();
						}
					}
				else if (playerChoice.RightAttackType == playerChoice.janken[1]) {
					if (PSM.rightIncoming.Peek() == playerChoice.janken[0]) {
							//win
						ESM.eLHS = Emeny_AIstatemachine.leftHand_state.INACTIVE;
						PSM.rightIncoming.Dequeue ();
					} else if (PSM.rightIncoming.Peek() == playerChoice.janken[2]||PSM.rightIncoming.Peek() == playerChoice.janken[1]) {
							//lose
						PSM.pRHS = Player_statemachine.rightHand_state.INACTIVE;
						PSM.rightIncoming.Dequeue ();
						}	
					}
				else if (playerChoice.RightAttackType == playerChoice.janken[2]) {
					if (PSM.rightIncoming.Peek() == playerChoice.janken[1]) {
							//win
						ESM.eLHS = Emeny_AIstatemachine.leftHand_state.INACTIVE;
						PSM.rightIncoming.Dequeue ();
					} else if (PSM.rightIncoming.Peek() == playerChoice.janken[0]||PSM.rightIncoming.Peek() == playerChoice.janken[2]) {
							//lose
						PSM.pRHS = Player_statemachine.rightHand_state.INACTIVE;
						PSM.rightIncoming.Dequeue ();
						}	
				}
			}
			else{//player lose
			}

			// for enemy target enemy
		}
	}
 }


